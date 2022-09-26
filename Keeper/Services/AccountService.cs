using System;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class AccountService
    {
        private readonly AccountsRepository _repo;
        private readonly KeepsRepository _keepsRepo;
        public AccountService(AccountsRepository repo, KeepsRepository keepsRepo)
        {
            _repo = repo;
            _keepsRepo = keepsRepo;
        }

        internal string GetProfileEmailById(string id)
        {
            return _repo.GetById(id).Email;
        }
        internal Account GetProfileByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
        internal Account GetOrCreateProfile(Account userInfo)
        {
            Account profile = _repo.GetById(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }

    internal Profile GetOne(string id)
    {
      Profile profile = _repo.GetOne(id);
      if(profile == null)
      {
        throw   new Exception("NO PROFILE");
      }
      return profile;
    }

    

    internal Account Edit(Account editData, string userEmail)
        {
            Account original = GetProfileByEmail(userEmail);
            original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
            original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
            return _repo.Edit(original);
        }
    }
}