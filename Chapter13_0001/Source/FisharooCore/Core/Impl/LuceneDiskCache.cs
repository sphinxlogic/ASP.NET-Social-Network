using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using StructureMap;
using Directory=Lucene.Net.Store.Directory;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Lucene")]
    public class LuceneDiskCache
    {
        private const string INDEX_FIELD_NAME = "index";
        private const string VALUE_FIELD_NAME = "object";
        private Analyzer _analyzer;
        private Directory _directory;
        private string _cachePath;
        private object _locker = new object();

        public LuceneDiskCache()
        {
            _analyzer = new StandardAnalyzer();
            _cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");
        }

        public void Set<T>(string key, T value)
        {
            if(value == null)
            {
                return;
            }

            lock (_locker)
            {
                removeObjectIfExists<T>(key);

                Directory directory = getCacheDirectory();
                IndexWriter indexWriter;
                if (IndexReader.IndexExists(directory))
                {
                    indexWriter = new IndexWriter(directory, _analyzer, false);
                }
                else
                {
                    indexWriter = new IndexWriter(directory, _analyzer, true);
                }

                byte[] objectBytes = getObjectBytes(value);

                Document document = new Document();
                string indexKey = getIndexKey<T>(key);
                document.Add(new Field(INDEX_FIELD_NAME, indexKey, Field.Store.YES, Field.Index.UN_TOKENIZED));
                document.Add(new Field(VALUE_FIELD_NAME, objectBytes, Field.Store.YES));
                indexWriter.AddDocument(document);

                indexWriter.Close();
            }
        }

        public T Get<T>(string key)
        {
            if(!IndexReader.IndexExists(getCacheDirectory()))
            {
                return default(T);
            }

            IndexSearcher searcher = new IndexSearcher(getCacheDirectory());
            Query query = new TermQuery(new Term(INDEX_FIELD_NAME, getIndexKey<T>(key)));
            Hits results = searcher.Search(query);

            if (results.Length() > 1)
            {
                searcher.Close();
                throw new ApplicationException("Found more than 1 hit in the cache.  Cache should not store duplicate objects");
            }
            else if (results.Length() == 0)
            {
                searcher.Close();
                return default(T);
            }
            else
            {
                Document foundDocument = results.Doc(0);
                byte[] objectBytes = foundDocument.GetBinaryValue(VALUE_FIELD_NAME);
                T foundObject = getObjectFromBytes<T>(objectBytes);
                searcher.Close();
                return foundObject;
            }
        }

        private void removeObjectIfExists<T>(string key)
        {
            Directory directory = getCacheDirectory();
            IndexModifier indexModifier;
            if (IndexReader.IndexExists(directory))
            {
                indexModifier = new IndexModifier(directory, _analyzer, false);
            }
            else
            {
                indexModifier = new IndexModifier(directory, _analyzer, true);
            }

            indexModifier.DeleteDocuments(new Term(INDEX_FIELD_NAME, getIndexKey<T>(key)));
            indexModifier.Close();
        }

        private static byte[] getObjectBytes<T>(T value)
        {
            byte[] objectBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, value);

                memoryStream.Position = 0;
                objectBytes = new byte[memoryStream.Length];
                memoryStream.Read(objectBytes, 0, objectBytes.Length);
            }
            return objectBytes;
        }

        private static T getObjectFromBytes<T>(byte[] objectBytes)
        {
            T rehydratedObject;
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(objectBytes, 0, objectBytes.Length);
                stream.Position = 0;

                BinaryFormatter formatter = new BinaryFormatter();
                rehydratedObject = (T) formatter.Deserialize(stream);
            }

            return rehydratedObject;
        }

        private string getIndexKey<T>(string key)
        {
            return typeof (T).FullName + key;
        }

        private Directory getCacheDirectory()
        {
            if (_directory != null)
            {
                return _directory;
            }

            if (!System.IO.Directory.Exists(_cachePath))
            {
                System.IO.Directory.CreateDirectory(_cachePath);
            }

            _directory = FSDirectory.GetDirectory(_cachePath, false);
            return _directory;
        }
    }
}