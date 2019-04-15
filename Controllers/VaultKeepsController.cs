using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsRepository _vkr;
    public VaultKeepsController(VaultKeepsRepository vkr)
    {
      _vkr = vkr;
    }

    //GETBYUSERID for dashboard
    [HttpGet("{userId}")]
    public ActionResult<VaultKeeps> Get(string userId)
    {
      IEnumerable<VaultKeeps> found = _vkr.GetById(userId);
      if (found == null) { return BadRequest(); }
      return Ok(found);
    }

    //CREATE
    [HttpPost]
    public ActionResult<VaultKeeps> Create([FromBody] VaultKeeps vaultkeeps)
    {
      VaultKeeps newVaultKeeps = _vkr.CreateVaultKeeps(vaultkeeps);
      if (newVaultKeeps == null) { return BadRequest("Something went wrong."); }
      return Ok(newVaultKeeps);
    }

    //DELETE
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _vkr.Delete(id);
      if (!successful) { return BadRequest(); }
      return Ok();
    }

  }
}
