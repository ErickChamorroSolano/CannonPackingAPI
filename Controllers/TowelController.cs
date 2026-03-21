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
    public class TowelController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TowelService _service;

        public TowelController(AppDbContext context, TowelService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TowelDto dto)
        {
            try
            {
                await _service.CreateTowel(dto);
                return Ok("Item creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var units = await _context.Towel
                    .Where(x => x.IsActive)
                    .ToListAsync();

                return Ok(units);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Eliminar (inhabilitar)
        [HttpPut("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            try
            {
                await _service.DisableTowel(id);
                return Ok("Item eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
