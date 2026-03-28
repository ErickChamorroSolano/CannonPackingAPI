using CannonPackingAPI.Common.Enums;
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

        // Empacar
        public async Task Pack(int boxId, int towelId)
        {
            try
            {
                var box = await _context.Box.FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);

                if (box == null)
                    throw new Exception("La caja no existe.");

                if (box.Status == BoxStatus.CLOSED.ToString())
                    throw new Exception("La caja está cerrada.");

                var towel = await _context.Towel.FirstOrDefaultAsync(t => t.Id == towelId && t.IsActive);

                if (towel == null)
                    throw new Exception("El item no existe.");

                if (towel.Status != TowelStatus.LOOSE.ToString())
                    throw new Exception("El item ya está empacado.");

                if (towel.ProductCode != box.ProductCode)
                    throw new Exception("El item no coincide con la caja.");

                var currentCount = await _context.Towel.CountAsync(t => t.BoxId == boxId && t.IsActive && t.Status == TowelStatus.PACKED.ToString());

                if (currentCount >= box.Capacity)
                    throw new Exception("La caja está llena.");

                // Validar que no esté en otra caja
                var alreadyPacked = await _context.Towel.AnyAsync(t => t.Id == towelId && t.BoxId != boxId && t.IsActive && t.Status == TowelStatus.PACKED.ToString());

                if (alreadyPacked)
                    throw new Exception("El item está en otra caja.");

                towel.BoxId = boxId;
                towel.Box = box;
                towel.Status = TowelStatus.PACKED.ToString();

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        // Desempacar
        public async Task Unpack(int boxId, int towelId)
        {
            try
            {
                var box = await _context.Box.FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);

                if (box == null)
                    throw new Exception("La caja no existe.");

                if (box.Status != BoxStatus.OPEN.ToString())
                    throw new Exception("La caja está cerrada.");

                var towel = await _context.Towel.FirstOrDefaultAsync(t => t.Id == towelId && t.IsActive);

                if (towel == null)
                    throw new Exception("El item no existe.");

                if (towel.BoxId != boxId)
                    throw new Exception("El item no pertenece a esta caja.");

                if (towel.Status != TowelStatus.PACKED.ToString())
                    throw new Exception("El item no está empacado.");

                towel.BoxId = null;
                towel.Box = null;
                towel.Status = TowelStatus.LOOSE.ToString();
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
