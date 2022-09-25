using System;
using System.Collections.Generic;
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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService)
        {
            _vaultsService = vaultsService;
            _vaultKeepsService = vaultKeepsService;
        }
        

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultsController>> Create ([FromBody] Vault newVault)
        {
            try 
            {
              Account user = await HttpContext.
              GetUserInfoAsync<Account>();
              newVault.CreatorId = user.Id;
              Vault vault = _vaultsService.Create(newVault);
              vault.Creator = user;
              return Ok(vault);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetOne(int id)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              Vault vault = _vaultsService.GetOne(id, user?.Id);
              return Ok(vault);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<VaultKeepViewModel>> GetKeeps(int id)
        {
          try 
          {
            List<VaultKeepViewModel> keeps = _vaultKeepsService.GetKeepsByVaultId(id);
            return Ok(keeps);
          }
          catch (Exception e)
          {
            return BadRequest(e.Message);
          }
        }



        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault vaultData)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              vaultData.Id = id;
              Vault vault = _vaultsService.Update(vaultData, user);
              return Ok(vault);
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
              string message = _vaultsService.Delete(id, user);
              return Ok(message);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }
    }
}