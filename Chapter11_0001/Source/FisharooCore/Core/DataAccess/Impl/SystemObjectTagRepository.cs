using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class SystemObjectTagRepository : ISystemObjectTagRepository
    {
        private Connection conn;
        public SystemObjectTagRepository()
        {
            conn = new Connection();
        }

        public List<SystemObjectTagWithObject> GetSystemObjectsByTagID(int TagID)
        {
            List<SystemObjectTagWithObject> result = new List<SystemObjectTagWithObject>(); 
            List<SystemObjectTag> tags = new List<SystemObjectTag>();

            List<Account> accounts = new List<Account>();
            List<Profile> profiles = new List<Profile>();
            List<Blog> blogs = new List<Blog>();
            List<BoardPost> posts = new List<BoardPost>();
            List<File> files = new List<File>();
            List<FileType> fileTypes = new List<FileType>();
            List<Folder> folders = new List<Folder>();
            List<Group> groups = new List<Group>();

            using(FisharooDataContext dc = conn.GetContext())
            {
                tags =
                    dc.SystemObjectTags.Where(sot => sot.TagID == TagID).
                    OrderByDescending(sot => sot.CreateDate).ToList();
                accounts =
                    dc.Accounts.Where(
                        a =>
                        tags.Where(t => t.SystemObjectID == 1).Select(t => t.SystemObjectRecordID).Contains(a.AccountID))
                        .ToList();
                profiles =
                    dc.Profiles.Where(
                        p =>
                        tags.Where(t => t.SystemObjectID == 2).Select(t => t.SystemObjectRecordID).Contains(p.ProfileID))
                        .ToList();
                blogs =
                    dc.Blogs.Where(
                        b =>
                        tags.Where(t => t.SystemObjectID == 3).Select(t => t.SystemObjectRecordID).Contains(b.BlogID))
                        .ToList();
                posts =
                    dc.BoardPosts.Where(
                        bp =>
                        tags.Where(t => t.SystemObjectID == 4).Select(t => t.SystemObjectRecordID).Contains(bp.PostID))
                        .ToList();
                files =
                    dc.Files.Where(
                        f =>
                        tags.Where(t => t.SystemObjectID == 5).Select(t => t.SystemObjectRecordID).Contains(f.FileID))
                        .ToList();
                fileTypes = dc.FileTypes.ToList();
                for (int i = 0; i < files.Count();i++)
                {
                    files[i].Extension =
                        fileTypes.Where(ft => ft.FileTypeID == files[i].FileTypeID).Select(ft => ft.Name).FirstOrDefault();
                }
                folders =
                        dc.Folders.Where(folder => files.Select(f => f.DefaultFolderID).Contains(folder.FolderID)).ToList();
                groups =
                    dc.Groups.Where(
                        g =>
                        tags.Where(t => t.SystemObjectID == 6).Select(t => t.SystemObjectRecordID).Contains(g.GroupID))
                        .ToList();
            }

            foreach (SystemObjectTag tag in tags)
            {
                switch(tag.SystemObjectID)
                {
                    case 1:
                        result.Add(new SystemObjectTagWithObject(){SystemObjectTag = tag,Account = accounts.Where(a=>a.AccountID == tag.SystemObjectRecordID).FirstOrDefault()});
                        break;

                    case 2:
                        result.Add(new SystemObjectTagWithObject(){SystemObjectTag = tag, Profile = profiles.Where(p=>p.ProfileID == tag.SystemObjectRecordID).FirstOrDefault()});
                        break;

                    case 3:
                        result.Add(new SystemObjectTagWithObject() { SystemObjectTag = tag, Blog = blogs.Where(b => b.BlogID == tag.SystemObjectRecordID).FirstOrDefault() });
                        break;

                    case 4:
                        result.Add(new SystemObjectTagWithObject() { SystemObjectTag = tag, BoardPost = posts.Where(p => p.PostID == tag.SystemObjectRecordID).FirstOrDefault() });
                        break;

                    case 5:
                        //need to get the file for use in getting the folder as well
                        File file = files.Where(f => f.FileID == tag.SystemObjectRecordID).FirstOrDefault();
                        result.Add(new SystemObjectTagWithObject() { SystemObjectTag = tag, File = file , Folder =
                        folders.Where(f=>f.FolderID == file.DefaultFolderID).FirstOrDefault()});
                        break;

                    case 6:
                        result.Add(new SystemObjectTagWithObject() { SystemObjectTag = tag, Group = groups.Where(g => g.GroupID == tag.SystemObjectRecordID).FirstOrDefault() });
                        break;
                }
            }


            return result;
        }

        public long SaveSystemObjectTag(SystemObjectTag tag)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(tag.SystemObjectTagID > 0)
                {
                    dc.SystemObjectTags.Attach(tag, true);
                }
                else if (dc.SystemObjectTags.Where(sot => sot.CreatedByAccountID == tag.CreatedByAccountID &&
                    sot.SystemObjectID == tag.SystemObjectID &&
                    sot.SystemObjectRecordID == tag.SystemObjectRecordID &&
                    sot.TagID == tag.TagID).FirstOrDefault() == null)
                {
                    dc.SystemObjectTags.InsertOnSubmit(tag);
                }
                dc.SubmitChanges();
            }
            return tag.SystemObjectTagID;
        }

        public void DeleteSystemObjectTag(SystemObjectTag tag)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.SystemObjectTags.Attach(tag, true);
                dc.SystemObjectTags.DeleteOnSubmit(tag);
                dc.SubmitChanges();
            }
        }
    }
}
