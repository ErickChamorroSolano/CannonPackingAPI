using CannonPackingAPI.DTOs;
using CannonPackingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CannonPackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxController : ControllerBase
    {
        private readonly BoxService _service;

        public BoxController(BoxService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BoxDto dto)
        {
            try
            {
                await _service.CreateBox(dto);
                return Ok("Caja creada correctamente.");
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
                var boxes = await _service.GetActiveBoxes();
                return Ok(boxes);
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
                await _service.DisableBox(id);
                return Ok("Caja eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> Close(int id)
        {
            try
            {
                await _service.CloseBox(id);
                return Ok("Caja cerrada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/open")]
        public async Task<IActionResult> Open(int id)
        {
            try
            {
                await _service.OpenBox(id);
                return Ok("Caja abierta correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}