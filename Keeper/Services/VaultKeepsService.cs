using System;
using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;
        private readonly VaultsRepository _vaultsRepo;
        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService, VaultsRepository vaultsRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepository;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
            _vaultsRepo = vaultsRepo;
        }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
    //  Vault vault = _vaultsRepo.GetOne(id);
    // if (vault.CreatorId != newVaultKeep.Creator.Id)
    // {
    //   throw new Exception("Not your vault to keep in");
    // }
      return _vaultKeepsRepo.Create(newVaultKeep);
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id, string userId)
    {
    Vault vault = _vaultsRepo.GetOne(id);
      // List <VaultKeepViewModel> keeps =_vaultKeepsRepo.GetKeepsByVaultId(id);
      
      if (vault.IsPrivate == true && vault.CreatorId != userId)
      {
        throw new Exception("Vault is Private");
      }

   return _vaultKeepsRepo.GetKeepsByVaultId(id);
    }

    internal string Delete(int id, Account user)
    {
      VaultKeep original = GetOne(id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception("Can't Delete, Not Yours");
      }
      _vaultKeepsRepo.Delete(id);
      return "It's Been Deleted";
    }

    private VaultKeep GetOne(int id)
    {
      VaultKeep vaultKeep = _vaultKeepsRepo.GetOne(id);
      if (vaultKeep == null)
      {
        throw new Exception("NOTHING THERE");
      }
      return vaultKeep;
    }

  
  }
}