using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class FolderService : IFolderService
    {
        private IFriendRepository _friendRepository;
        private IFolderRepository _folderRepository;
        public FolderService()
        {
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _folderRepository = ObjectFactory.GetInstance<IFolderRepository>();
        }

        public List<Folder> GetFriendsFolders(Int32 AccountID)
        {
            List<Friend> friends = _friendRepository.GetFriendsByAccountID(AccountID);
            List<Folder> folders = _folderRepository.GetFriendsFolders(friends);
            folders.OrderBy(f => f.CreateDate).Reverse();
            return folders;
        }
    }
}
