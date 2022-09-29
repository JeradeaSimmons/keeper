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
        private readonly KeepsRepository _keepsRepo;
        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService, VaultsRepository vaultsRepo, KeepsRepository keepsRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepository;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
            _vaultsRepo = vaultsRepo;
            _keepsRepo = keepsRepo;
        }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
     Vault vault = _vaultsRepo.GetOne(newVaultKeep.vaultId);
    if (vault.CreatorId != newVaultKeep.CreatorId)
    {
      throw new Exception("Not your vault to keep in");
    }
      
      // TODO whenever I create a vaultkeep....I should increment the keep count for THAT keep
      // find the keep by the vaultKeep.keepId...use your repo function NOT the keeps service
      // 'update' the foundkeep and increase it's kept count
      Keep keep = _keepsRepo.GetOne(newVaultKeep.keepId);
      keep.Kept++;
      _keepsRepo.Update(keep);
      return _vaultKeepsRepo.Create(newVaultKeep);
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id, string userId)
    {
    Vault vault = _vaultsRepo.GetOne(id);
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