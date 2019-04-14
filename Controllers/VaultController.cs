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
  public class VaultController : ControllerBase
  {
    private readonly VaultRepository _vr;
    public VaultController(VaultRepository vr)
    {
      _vr = vr;
    }

    // //GETALL for home
    // [HttpGet]
    // public ActionResult<IEnumerable<Vault>> Get()
    // {
    //   IEnumerable<Vault> results = _kr.GetALL();
    //   if (results == null) { return BadRequest(); }
    //   return Ok(results);
    // }

    //GETBYUSERID for dashboard
    [HttpGet("{userId}")]
    public ActionResult<Vault> Get(string userId)
    {
      IEnumerable<Vault> found = _vr.GetById(userId);
      if (found == null) { return BadRequest(); }
      return Ok(found);
    }


    //CREATE
    [HttpPost]
    public ActionResult<Vault> Create([FromBody] Vault vault)
    {
      Vault newVault = _vr.CreateVault(vault);
      if (newVault == null) { return BadRequest("Something went wrong."); }
      return Ok(newVault);
    }

    //DELETE
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _vr.Delete(id);
      if (!successful) { return BadRequest(); }
      return Ok();
    }


  }
}
