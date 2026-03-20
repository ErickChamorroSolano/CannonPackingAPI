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
        private readonly BoxService _bservice;
        private readonly PackingService _pservice;

        public PackingController(AppDbContext context, BoxService bservice, PackingService pservice)
        {
            _context = context;
            _bservice = bservice;
            _pservice = pservice;
        }

        [HttpPost("{boxId}/add-unit/{unitId}")]
        public async Task<IActionResult> AddUnit(int boxId, int unitId)
        {
            await _bservice.AddTowelToBox(boxId, unitId);
            return Ok();
        }

        [HttpPost("{boxId}/pack-unit/{unitId}")]
        public async Task<IActionResult> Pack(int boxId, int unitId)
        {
            await _pservice.Pack(boxId, unitId);
            return Ok();
        }
    }
}
