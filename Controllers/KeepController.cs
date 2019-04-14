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
  public class KeepController : ControllerBase
  {
    private readonly KeepRepository _kr;
    public KeepController(KeepRepository kr)
    {
      _kr = kr;
    }

    //GETALL for home
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      IEnumerable<Keep> results = _kr.GetALL();
      if (results == null) { return BadRequest(); }
      return Ok(results);
    }

    //GETBYUSERID for dashboard
    [HttpGet("{userId}")]
    public ActionResult<Keep> Get(string userId)
    {
      IEnumerable<Keep> found = _kr.GetById(userId);
      if (found == null) { return BadRequest(); }
      return Ok(found);
    }


    //CREATE
    [HttpPost]
    public ActionResult<Keep> Create([FromBody] Keep keep)
    {
      Keep newKeep = _kr.CreateKeep(keep);
      if (newKeep == null) { return BadRequest("Something went wrong."); }
      return Ok(newKeep);
    }

    //DELETE
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _kr.Delete(id);
      if (!successful) { return BadRequest(); }
      return Ok();
    }


  }
}
