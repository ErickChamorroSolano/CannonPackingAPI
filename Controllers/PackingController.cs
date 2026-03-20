using CannonPackingAPI.Data;
using CannonPackingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CannonPackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PackingService _service;

        public PackingController(AppDbContext context, PackingService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("{boxId}/pack")]
        public async Task<IActionResult> Pack(int boxId, int towelId)
        {
            try
            {
                await _service.Pack(boxId, towelId);
                return Ok("Item empacado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{boxId}/unpack")]
        public async Task<IActionResult> Unpack(int boxId, int towelId)
        {
            try
            {
                await _service.Unpack(boxId, towelId);
                return Ok("Item removido de la caja");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{boxId}/close")]
        public async Task<IActionResult> Close(int boxId)
        {
            try
            {
                await _service.CloseBox(boxId);
                return Ok("Caja cerrada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
