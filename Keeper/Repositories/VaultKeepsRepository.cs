using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT
      vk.*,
      k.*,
      a.*
      FROM vaultKeeps vk
      JOIN keeps k ON vk.keepId = k.id
      JOIN accounts a ON k.creatorId = a.id
      WHERE vk.vaultId = @vaultId
      ;
      ";
      List<VaultKeepViewModel> keeps = _db.Query<VaultKeep, VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (vk, k, a)=>
      {
        k.Creator = a;
        k.VaultKeepId = vk.Id;
        return k;
      }, new {vaultId}).ToList();
      return keeps;
    }
  internal VaultKeep GetOne(int id)
    {
      string sql = @"
      SELECT
      vk.*,
      a.*
      FROM vaultKeeps vk
      JOIN accounts a ON vk.creatorId = a.id
      WHERE vk.Id = @id;
      ";
      return _db.Query<VaultKeep, Account, VaultKeep>(sql, (vk, account) =>
      {
        return vk;
      }, new {id}).FirstOrDefault();
    }



    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultKeeps WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

  
  }
}