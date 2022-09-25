using System;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepo;

        public VaultsService(VaultsRepository vaultsRepo)
        {
            _vaultsRepo = vaultsRepo;
        }

    internal Vault Create(Vault newVault)
    {
      return _vaultsRepo.Create(newVault);
    }

    internal Vault GetOne(int id, string userId)
    {
      Vault vault = _vaultsRepo.GetOne(id);
      if (vault == null)
      {
        throw new Exception("no vault with that id");

      }
      if (vault.CreatorId != userId)
      {
        throw new Exception("Not Your Vault");
      }
      return vault;
    }

    internal Vault Update(Vault update, Account user)
    {
      Vault original = GetOne(update.Id, user.Id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception("You Are Not The Creator");
      }
      original.Name = update.Name ?? original.Name;
      original.Img = update.Img ?? original.Name;
      original.Description = update.Description ?? original.Description;

      return _vaultsRepo.Update(original);

    }

    internal string Delete(int id, Account user)
    {
      Vault original = GetOne(id, user.Id);
      if(original.CreatorId != user.Id)
      {
        throw new Exception("Not yours to delete");
      }
      _vaultsRepo.Delete(id);
      return "Vault Deleted";
    }
  }
}