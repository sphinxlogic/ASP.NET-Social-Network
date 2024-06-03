using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using StructureMap;
using Directory=Lucene.Net.Store.Directory;
using Term=Lucene.Net.Index.Term;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class LuceneSearchService : ILuceneSearchService
    {
        private object _searchLocker = new object();
        private object _indexBuildLocker = new object();
        private Directory _directory;
        private string _indexPath;

        private IProfileRepository _profileRepository;
        private IBlogRepository _blogRepository;
        private IAccountRepository _accountRepository;

        public event EventHandler RecordAddedEvent;
        public LuceneSearchService()
        {
            _indexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache_");

            _profileRepository = ObjectFactory.GetInstance<IProfileRepository>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _blogRepository = ObjectFactory.GetInstance<IBlogRepository>();
        }

        public List<SearchResult> Search(string InputText)
        {
            List<SearchResult> results = new List<SearchResult>();

            if (string.IsNullOrEmpty(InputText))
                return null;

            lock (_searchLocker)
            {
                results = SearchIndexes(InputText.ToLower());
            }

            return results;
        }

        private List<SearchResult> SearchIndexes(string InputText)
        {
            List<SearchResult> result = new List<SearchResult>();
            string[] indexNames = {"Profiles", "Blogs"};

            foreach (string indexName in indexNames)
            {
                IndexReader reader = IndexReader.Open(getCacheDirectory(indexName));
                IndexSearcher searcher = new IndexSearcher(reader);

                Hits hits = null;

                //are there any wild cards in use?
                if (InputText.Contains("*"))
                {
                    WildcardQuery query = new WildcardQuery(new Term("Content", InputText));
                    hits = searcher.Search(query);
                }
                    //is this a multi term query?
                else if (InputText.Contains(" "))
                {                   
                    MultiPhraseQuery query = new MultiPhraseQuery();
                    foreach (string s in InputText.Split(' '))
                    {
                        query.Add(new Term("Content", s));
                    }
                    hits = searcher.Search(query);
                }
                    //single term query
                else
                {
                    PhraseQuery query = new PhraseQuery();
                    query.Add(new Term("Content", InputText));
                    hits = searcher.Search(query);
                }

                for (int i = 0; i < hits.Length(); i++)
                {
                    Document doc = hits.Doc(i);
                    SearchResult sr = new SearchResult();

                    sr.AccountID = Convert.ToInt32(doc.GetField("AccountID").StringValue());
                    sr.DisplayText = doc.GetField("DisplayText").StringValue();
                    sr.Content = doc.GetField("Content").StringValue();
                    sr.Order = Convert.ToInt32(doc.GetField("Order").StringValue());
                    sr.SystemObjectID = Convert.ToInt32(doc.GetField("SystemObjectID").StringValue());
                    sr.SystemObjectRecordID = Convert.ToInt64(doc.GetField("SystemObjectRecordID").StringValue());
                    sr.URL = doc.GetField("URL").StringValue();

                    result.Add(sr);
                }
            }
            return result;
        }
        
        private Directory getCacheDirectory(string SubFolder)
        {        
            if (!System.IO.Directory.Exists(_indexPath + "\\" + SubFolder))
            {
                System.IO.Directory.CreateDirectory(_indexPath + "\\" + SubFolder);
            }

            _directory = FSDirectory.GetDirectory(_indexPath + "\\" + SubFolder, false);
            return _directory;
        }

        public void BuildIndexesThread()
        {
            ThreadStart start = new ThreadStart(BuildIndexes);
            Thread t = new Thread(start);
            t.Priority = ThreadPriority.Lowest;
            t.Start();
            Thread.Sleep(0);
        }

        private void BuildIndexes()
        {
            lock (_indexBuildLocker)
            {
                BuildBlogIndex(); 
                BuildProfileIndex();
            }
        }

        private void BuildProfileIndex()
        {
            int currentProfilePage = 1;
            bool moreRecords = true;

            //open up a new indexWriter
            IndexWriter indexWriter = new IndexWriter(getCacheDirectory("Profiles"), new StandardAnalyzer(), true);

            //keep track of how many records we have in the index
            int counter = 0;

            try
            {
                //as long as we have more records iterate through them
                while (moreRecords)
                {
                    //get an updated list of profiles to add to the index
                    List<Profile> profiles = _profileRepository.GetProfilesForIndexing(currentProfilePage);

                    //get out of the loop once we run out of records
                    if (profiles.Count() == 0)
                        moreRecords = false;

                    //with each profile we need to create a new record
                    foreach (Profile profile in profiles)
                    {
                        Document doc = new Document();

                        doc.Add(new Field("SystemObjectID", "2", Field.Store.YES, Field.Index.NO,
                                          Field.TermVector.NO));
                        doc.Add(new Field("SystemObjectRecordID", profile.ProfileID.ToString(), Field.Store.YES,
                                          Field.Index.NO, Field.TermVector.NO));
                        doc.Add(new Field("DisplayText",
                                          profile.Signature != "" ? profile.Signature : profile.ProfileName,
                                          Field.Store.YES, Field.Index.NO, Field.TermVector.NO));
                        doc.Add(new Field("Content", profile.IMAOL + " " +
                                                         profile.IMGIM + " " +
                                                         profile.IMICQ + " " +
                                                         profile.IMMSN + " " +
                                                         profile.IMSkype + " " +
                                                         profile.IMYIM + " " +
                                                         profile.ProfileName + " " +
                                                         profile.Signature, Field.Store.YES, Field.Index.TOKENIZED,
                                          Field.TermVector.YES));
                        doc.Add(new Field("URL", "~/default.aspx?AccountID=" + profile.AccountID.ToString(),
                                          Field.Store.YES, Field.Index.NO, Field.TermVector.NO));
                        doc.Add(new Field("Order", counter.ToString(), Field.Store.YES, Field.Index.NO,
                                          Field.TermVector.NO));
                        doc.Add(new Field("AccountID", profile.AccountID.ToString(), Field.Store.YES,
                                          Field.Index.NO, Field.TermVector.NO));

                        indexWriter.AddDocument(doc);

                        //RecordAdded!
                        EventHandler handler = RecordAddedEvent;
                        if(handler != null)
                        {
                            handler(this,new EventArgs());
                            Thread.Sleep(0);
                        }

                        //increment the counter
                        counter++;
                    }
                    currentProfilePage++;
                }

                //make sure we optimize the index after building it
                indexWriter.Optimize();
            }
            catch (Exception e)
            {
                //oops
                Log.Error(this, e.Message);
            }
            finally
            {
                //we need to make sure that we close this!
                if(indexWriter != null)
                {
                    //close the index
                    indexWriter.Close();
                }
            }
        }

        private void BuildBlogIndex()
        {
            int currentBlogPage = 1;
            bool moreRecords = true;

            //open up a new indexWriter
            IndexWriter indexWriter = new IndexWriter(getCacheDirectory("Blogs"), new StandardAnalyzer(), true);

            //keep track of how many records we have in the index
            int counter = 0;

            try
            {
                //as long as we have more records iterate through them
                while (moreRecords)
                {
                    //get an updated list of profiles to add to the index
                    List<Blog> blogs = _blogRepository.GetBlogsForIndexing(currentBlogPage);

                    //get out of the loop once we run out of records
                    if (blogs.Count() == 0)
                        moreRecords = false;

                    //with each profile we need to create a new record
                    foreach (Blog blog in blogs)
                    {
                        Document doc = new Document();

                        doc.Add(new Field("SystemObjectID", "3", Field.Store.YES, Field.Index.NO,
                                          Field.TermVector.NO));
                        doc.Add(new Field("SystemObjectRecordID", blog.BlogID.ToString(), Field.Store.YES,
                                          Field.Index.NO, Field.TermVector.NO));
                        doc.Add(new Field("DisplayText",
                                          blog.Title != "" ? blog.Title : blog.Subject,
                                          Field.Store.YES, Field.Index.NO, Field.TermVector.NO));
                        doc.Add(new Field("Content", blog.Title + " " + blog.Subject + " " + blog.Post + " " + blog.PageName, Field.Store.YES, Field.Index.TOKENIZED,
                                          Field.TermVector.YES));
                        doc.Add(new Field("URL", "~/Blogs/default.aspx?BlogID=" + blog.BlogID.ToString(),
                                          Field.Store.YES, Field.Index.NO, Field.TermVector.NO));
                        doc.Add(new Field("Order", counter.ToString(), Field.Store.YES, Field.Index.NO,
                                          Field.TermVector.NO));
                        doc.Add(new Field("AccountID", blog.AccountID.ToString(), Field.Store.YES,
                                          Field.Index.NO, Field.TermVector.NO));

                        indexWriter.AddDocument(doc);

                        //RecordAdded!
                        EventHandler handler = RecordAddedEvent;
                        if (handler != null)
                        {
                            handler(this, new EventArgs());
                            Thread.Sleep(0);
                        }

                        //increment the counter
                        counter++;
                    }
                    currentBlogPage++;
                }

                //make sure we optimize the index after building it
                indexWriter.Optimize();
            }
            catch (Exception e)
            {
                //oops
                Log.Error(this, e.Message);
            }
            finally
            {
                //we need to make sure that we close this!
                if (indexWriter != null)
                {
                    //close the index
                    indexWriter.Close();
                }
            }
        }
    }

    public struct SearchResult
    {
        public int SystemObjectID { get; set; }
        public long SystemObjectRecordID { get; set; }
        public string DisplayText { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }
        public int AccountID { get; set; }
    }
}
