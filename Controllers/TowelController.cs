using CannonPackingAPI.DTOs;
using CannonPackingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CannonPackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TowelController : ControllerBase
    {
        private readonly TowelService _service;

        public TowelController(TowelService service)
        {
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
                var units = await _service.GetActiveTowels();
                return Ok(units);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{productcode}/available")]
        public async Task<IActionResult> GetAvailable(string productcode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productcode))
                    throw new Exception("ProductCode es requerido.");

                var units = await _service.GetAvailableTowelsByProductCode(productcode.ToUpper());
                return Ok(units);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Eliminar (inhabilitar)
        [HttpPut("{id}/disable")]
        public async Task<IActionResult> Delete(int id)
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
