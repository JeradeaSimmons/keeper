using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
       private readonly VaultKeepsService _vaultKeepsService;

       public VaultKeepsController(VaultKeepsService vaultKeepsService)
       {
        _vaultKeepsService = vaultKeepsService;
       }

       [HttpPost]
       [Authorize]
       public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
       {
        try 
        {
          Account user = await HttpContext.GetUserInfoAsync<Account>();
          newVaultKeep.CreatorId = user.Id;
          VaultKeep vaultKeep = _vaultKeepsService.Create(newVaultKeep);
          
          return Ok(vaultKeep);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
       }


       [HttpDelete("{id}")]
       [Authorize]
       public async Task<ActionResult<string>> Delete(int id)
       {
        try 
        {
          Account user = await HttpContext.GetUserInfoAsync<Account>();
          string message = _vaultKeepsService.Delete(id, user.Id);
          return Ok(message);
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
       }
    }
}