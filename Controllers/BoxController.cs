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
                return BadRequest("El codigo de producto es requerido.");

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
                BoxStatus = "OPEN",
                IsActive = true
            };

            _context.Box.Add(box);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                box.Id,
                box.BoxCode,
                box.ProductCode,
                box.Capacity,
                box.BoxStatus
            });
        }

        [HttpPost("{boxId}/add-unit/{unitId}")]
        public async Task<IActionResult> AddUnit(int boxId, int unitId)
        {
            await _service.AddTowelToBox(boxId, unitId);
            return Ok();
        }
    }
}