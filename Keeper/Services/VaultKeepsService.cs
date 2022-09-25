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
        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService)
        {
            _vaultKeepsRepo = vaultKeepsRepository;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _vaultKeepsRepo.Create(newVaultKeep);
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      return _vaultKeepsRepo.GetKeepsByVaultId(vaultId);
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