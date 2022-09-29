using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
  public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

    internal List<Keep> GetAll()
    {
      // TODO return the 'count' for how many times keeps have been kept
      // return a count of how many times each keep appears in the vault keep table
      // refer to the restaurants repository in helpReviews for how to achieve this
       string sql = @"
      SELECT
      k.*,
      a.*
       FROM keeps k
       JOIN accounts a ON a.id = k.creatorId;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=>
      {
        keep.Creator = profile;
        return keep;
      }).ToList();
    }

    internal Keep GetOne(int id)
    {
      // TODO return the 'count' for how many times keeps have been kept
      // return a count of how many times each keep appears in the vault keep table
      // refer to the restaurants repository in helpReviews for how to achieve this
      string sql = @"
       SELECT
        k.*,
        a.*
    FROM keeps k
    JOIN accounts a ON k.creatorId = a.id
    WHERE k.id = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=>
      {
        keep.Creator = profile;
        return keep;
      }, new {id}).FirstOrDefault();
    }

    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
        (name, description, img, creatorId)
        VALUES
      (@name, @description, @img, @creatorId);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    internal List<Keep> GetProfileKeeps(string creatorId)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @creatorId;
      
    ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p)=>
      {
        k.Creator = p;
        return k;
      }, new {creatorId}).ToList();
     
    }

   

    internal Keep Update(Keep update)
    {
      string sql = @"
       UPDATE keeps SET
        name = @name,
        img = @img,
        description = @description,
        views = @views,
        kept = @kept
        Where id = @id;
      ";
      _db.Execute(sql, update);
      return update;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}