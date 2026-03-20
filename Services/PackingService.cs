using CannonPackingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Services
{
    public class PackingService
    {
        private readonly AppDbContext _context;

        public PackingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Pack(int boxId, int towelId)
        {
            var box = await _context.Boxes
                .Include(b => b.Towels)
                .FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);

            if (box == null)
                throw new Exception("la caja no existe.");

            if (box.Status != "OPEN")
                throw new Exception("La caja está cerrada.");

            var towel = await _context.Towels
                .FirstOrDefaultAsync(t => t.Id == towelId && t.IsActive);

            if (towel == null)
                throw new Exception("El item no existe.");

            if (towel.Status != "LOOSE")
                throw new Exception("El item ya está empacado.");

            if (towel.ProductCode != box.ProductCode)
                throw new Exception("El producto no coincide con la caja.");

            var currentCount = box.Towels.Count(t => t.IsActive && t.Status == "PACKED");

            if (currentCount >= box.Capacity)
                throw new Exception("La caja está llena.");

            // APPLY
            towel.Status = "PACKED";
            towel.BoxId = boxId;

            await _context.SaveChangesAsync();
        }

        // UNPACK
        public async Task Unpack(int boxId, int towelId)
        {
            var box = await _context.Boxes
                .FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);

            if (box == null)
                throw new Exception("La caja no existe.");

            if (box.Status != "OPEN")
                throw new Exception("La caja está cerrada.");

            var towel = await _context.Towels
                .FirstOrDefaultAsync(t => t.Id == towelId && t.IsActive);

            if (towel == null)
                throw new Exception("El item no existe.");

            if (towel.Status != "PACKED" || towel.BoxId != boxId)
                throw new Exception("El item no pertenece a ésta caja.");

            towel.Status = "LOOSE";
            towel.BoxId = null;

            await _context.SaveChangesAsync();
        }

        // Cerrar caja
        public async Task CloseBox(int boxId)
        {
            var box = await _context.Boxes
                .FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);

            if (box == null)
                throw new Exception("La caja no existe.");

            if (box.Status != "OPEN")
                throw new Exception("La caja ya está cerrada.");

            box.Status = "CLOSED";

            await _context.SaveChangesAsync();
        }
    }
}
