using CannonPackingAPI.Data;
using CannonPackingAPI.DTOs;
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
        public async Task<IActionResult> Create(TowelDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ItemCode))
                return BadRequest("El código del item es requerido.");

            if (string.IsNullOrWhiteSpace(dto.ProductCode))
                return BadRequest("El código del producto es requerido.");

            var exists = await _context.Towel
                .AnyAsync(t => t.ItemCode == dto.ItemCode);

            if (exists)
                return BadRequest("El código del item ya existe.");

            var towel = new Towel
            {
                ItemCode = dto.ItemCode,
                ProductCode = dto.ProductCode,
                TowelStatus = "LOOSE",
                IsActive = true
            };

            _context.Towel.Add(towel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                towel.Id,
                towel.ItemCode,
                towel.ProductCode,
                towel.TowelStatus,
                towel.IsActive
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var units = await _context.Towel
                .Where(x => x.IsActive)
                .ToListAsync();

            return Ok(units);
        }
    }
}
