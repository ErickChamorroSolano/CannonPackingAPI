using CannonPackingAPI.Common.Enums;
using CannonPackingAPI.Data;
using CannonPackingAPI.DTOs;
using CannonPackingAPI.Models;
using CannonPackingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly BoxService _service;

        public BoxController(AppDbContext context, BoxService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BoxDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.BoxCode))
                return BadRequest("El código de caja es requerido.");

            if (string.IsNullOrWhiteSpace(dto.ProductCode))
                return BadRequest("El código de producto es requerido.");

            if (dto.Capacity <= 0)
                return BadRequest("La capacidad debe ser mayor a 0.");

            var exists = await _context.Box.AnyAsync(b => b.BoxCode == dto.BoxCode);

            if (exists)
                return BadRequest("El codigo de caja ya existe.");

            var box = new Box
            {
                BoxCode = dto.BoxCode,
                ProductCode = dto.ProductCode,
                Capacity = dto.Capacity,
                BoxStatus = BoxStatus.OPEN.ToString(),
                IsActive = true
            };

            _context.Box.Add(box);
            await _context.SaveChangesAsync();

            //return Ok(new
            //{
            //    box.Id,
            //    box.BoxCode,
            //    box.ProductCode,
            //    box.Capacity,
            //    box.BoxStatus,
            //    box.IsActive
            //});
            return Ok("Caja creada correctamente.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var boxes = await _context.Box
                .Where(x => x.IsActive)
                .ToListAsync();

            return Ok(boxes);
        }

        // Eliminar (inhabilitar)
        [HttpPut("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var box = await _context.Box.FirstOrDefaultAsync(b => b.Id == id);

            if (box == null)
                return NotFound();

            var count = _context.BoxTowel.Count(bt => bt.BoxId == id);

            if (count > 0)
                return BadRequest("La caja tiene items empacados");

            box.IsActive = false;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}