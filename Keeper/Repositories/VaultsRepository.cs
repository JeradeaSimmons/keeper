using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
  public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

    internal Vault Create(Vault newVault)
    {
       string sql = @"
      INSERT INTO vaults
        (name, description, img, creatorId, isPrivate)
        VALUES
      (@name, @description, @img, @creatorId, @isPrivate);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }

    internal Vault GetOne(int id)
    {
      string sql = @"
       SELECT
        v.*,
        a.*
    FROM vaults v
    JOIN accounts a ON v.creatorId = a.id
    WHERE v.id = @id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile)=>
      {
        vault.Creator = profile;
        return vault;
      }, new {id}).FirstOrDefault();
    }

    internal Vault Update(Vault update)
    {
      string sql = @"
       UPDATE vaults SET
        name = @name,
        img = @img,
        description = @description
        Where id = @id;
      ";
      _db.Execute(sql, update);
      return update;
    }

    internal void Delete(int id)
    {
     string sql = @"
      DELETE FROM vaults WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}