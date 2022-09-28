using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

        public ProfilesController(AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
        {
            _accountService = accountService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetOne(string id)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              Profile profile = _accountService.GetOne(id);
              return Ok(profile);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<Keep>>> GetProfileKeeps( string id)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              List<Keep> keeps = _keepsService.GetProfileKeeps( id);
              return Ok(keeps);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetProfileVaults( string id)
        {
            try 
            {
              Account user = await HttpContext.GetUserInfoAsync<Account>();
              List<Vault> vaults = _vaultsService.GetProfileVaults(id);
              return Ok(vaults);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }
    }
}