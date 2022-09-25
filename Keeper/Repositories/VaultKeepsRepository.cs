using System.Data;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
  public class VaultKeepsRepository
    {
      private readonly IDbConnection _db;
      public VaultKeepsRepository(IDbConnection db)
      {
        _db = db;
      }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
        (vaultId, keepId, creatorId)
        VALUES
      (@vaultId, @keepId, @creatorId);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }
  }
}