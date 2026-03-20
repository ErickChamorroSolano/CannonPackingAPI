using CannonPackingAPI.Data;
using CannonPackingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TowelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TowelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Towel towel)
        {
            _context.Towels.Add(towel);
            await _context.SaveChangesAsync();
            return Ok(towel);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var units = await _context.Towels
                .Where(x => x.IsActive)
                .ToListAsync();

            return Ok(units);
        }
    }
}
