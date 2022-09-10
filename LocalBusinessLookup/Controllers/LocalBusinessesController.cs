using System.Collections.Generic;
using System.Threading.Tasks;
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



    [HttpPost]
    public async Task<ActionResult<LocalBusiness>> Post(LocalBusiness business)
    {
      _db.LocalBusinesses.Add(business);
      await _db.SaveChangesAsync();

      return CreatedAtAction("Post", new { id = business.LocalBusinessId }, business);
      //return CreatedAtAction(nameof(GetLocalBusiness), new {id = business.LocalBusinessId, business});
    }
  }
}