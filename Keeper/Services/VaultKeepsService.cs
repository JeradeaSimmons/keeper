using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
        {
            _vaultKeepsRepo = vaultKeepsRepository;
        }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _vaultKeepsRepo.Create(newVaultKeep);
    }
  }
}