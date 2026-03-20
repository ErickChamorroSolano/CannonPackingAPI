using CannonPackingAPI.Data;
using CannonPackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Services
{
    public class BoxService
    {
        private readonly AppDbContext _context;

        public BoxService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTowelToBox(int boxId, int towelId)
        {
            var box = await _context.Boxes.FindAsync(boxId);
            if (box == null || !box.IsActive)
                throw new Exception("La caja no existe.");

            if (box.Status != "OPEN")
                throw new Exception("La caja está cerrada.");

            var currentCount = await _context.BoxTowels
                .CountAsync(x => x.BoxId == boxId && x.IsActive);

            if (currentCount >= box.Capacity)
                throw new Exception("La caja está al limite de capacidad.");

            var unitInAnotherBox = await _context.BoxTowels
                .AnyAsync(x => x.TowelId == towelId && x.IsActive);

            if (unitInAnotherBox)
                throw new Exception("La unidad ya está en otra caja.");

            var boxItem = new BoxTowel
            {
                BoxId = boxId,
                TowelId = towelId
            };

            _context.BoxTowels.Add(boxItem);
            await _context.SaveChangesAsync();
        }
    }
}
