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
  [Route("keep")]
  [ApiController]
  public class KeepController : ControllerBase
  {
    private readonly KeepRepository _kr;
    public KeepController(KeepRepository kr)
    {
      _kr = kr;
    }

    //CREATE
    [HttpPost]
    public ActionResult<Keep> Create([FromBody] Keep keep)
    {
      Keep newKeep = _kr.CreateKeep(keep);
      if (newKeep == null) { return BadRequest("Something went wrong."); }
      return Ok(newKeep);
    }


  }
}
