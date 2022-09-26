using System;
using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
  public class KeepsService
    {
        private readonly KeepsRepository _keepsRepo;
        public KeepsService(KeepsRepository keepsRepo)
        {
            _keepsRepo = keepsRepo;
        }

    internal List<Keep> GetAll()
    {
      return _keepsRepo.GetAll();
    }

    internal Keep Create(Keep newKeep)
    {
      return _keepsRepo.Create(newKeep);
    }

    internal Keep GetOne(int id, string userId)
    {
      Keep keep = _keepsRepo.GetOne(id);
      if (keep == null)
      {
        throw new Exception("no keep with that id");
      }
      // if(keep.CreatorId != userId)
      // {
      //   throw new Exception("Not your Keep");
      // }
      return keep;
    }

    internal Keep Update(Keep update, Account user)
    {
      Keep original = GetOne(update.Id, user.Id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception("You Are Not The Creator");
      }
      original.Name = update.Name ?? original.Name;
      original.Img = update.Img ?? original.Name;
      original.Description = update.Description ?? original.Description;

      return _keepsRepo.Update(original);
    }

    internal string Delete(int id, Account user)
    {
      Keep original = GetOne(id, user.Id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception("Not yours to delete");
      }
      _keepsRepo.Delete(id);
      return "Keep Deleted";
    }
  }
}