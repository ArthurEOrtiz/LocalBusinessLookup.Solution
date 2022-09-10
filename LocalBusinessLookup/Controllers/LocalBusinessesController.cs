using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalBusinessLookup.Models;

namespace LocalBusinessLookup.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocalBusinessesController : ControllerBase
  {
    private readonly LocalBusinessLookupContext _db;

    public LocalBusinessesController(LocalBusinessLookupContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocalBusiness>>> Get()
    {
      return await _db.LocalBusinesses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocalBusiness>> GetLocalBusiness(int id)
    {
      var localbusiness = await _db.LocalBusinesses.FindAsync(id);

      if (localbusiness == null)
      {
        return NotFound();
      }

      return localbusiness;
    }

    [HttpPost]
    public async Task<ActionResult<LocalBusiness>> Post(LocalBusiness business)
    {
      _db.LocalBusinesses.Add(business);
      await _db.SaveChangesAsync();

      //return CreatedAtAction("Post", new { id = business.LocalBusinessId }, business);
      return CreatedAtAction(nameof(GetLocalBusiness), new {id = business.LocalBusinessId, business});
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, LocalBusiness business)
    {
      if (id != business.LocalBusinessId)
      {
        return BadRequest();
      }

      _db.Entry(business).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LocalBusinessExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocalBusiness(int id)
    {
      var localbusiness = await _db.LocalBusinesses.FindAsync(id);
      if (localbusiness == null)
      {
        return NotFound();
      }

      _db.LocalBusinesses.Remove(localbusiness);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool LocalBusinessExists(int id)
    {
      return _db.LocalBusinesses.Any(e => e.LocalBusinessId == id);
    }
  }
}