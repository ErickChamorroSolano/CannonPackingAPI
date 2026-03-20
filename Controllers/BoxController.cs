using CannonPackingAPI.Data;
using CannonPackingAPI.Models;
using CannonPackingAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(Box box)
        {
            _context.Boxes.Add(box);
            await _context.SaveChangesAsync();
            return Ok(box);
        }

        [HttpPost("{boxId}/add-unit/{unitId}")]
        public async Task<IActionResult> AddUnit(int boxId, int unitId)
        {
            await _service.AddTowelToBox(boxId, unitId);
            return Ok();
        }
    }
}