using CannonPackingAPI.Common.Enums;
using CannonPackingAPI.Data;
using CannonPackingAPI.Models;
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
                var box = await _context.Box
                    .FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);

                if (box == null)
                    throw new Exception("La caja no existe.");

                if (box.BoxStatus != BoxStatus.OPEN.ToString())
                    throw new Exception("La caja está cerrada.");

                var towel = await _context.Towel.FirstOrDefaultAsync(t => t.Id == towelId && t.IsActive);

                if (towel == null)
                    throw new Exception("El item no existe.");

                if (towel.TowelStatus != TowelStatus.LOOSE.ToString())
                    throw new Exception("El item ya está empacado.");

                if (towel.ProductCode != box.ProductCode)
                    throw new Exception("El item no coincide con la caja.");

                var currentCount = await _context.BoxTowel.CountAsync(bt => bt.BoxId == boxId && bt.IsActive);

                if (currentCount >= box.Capacity)
                    throw new Exception("La caja está llena.");

                // Validar que no esté en otra caja
                var alreadyPacked = await _context.BoxTowel.AnyAsync(bt => bt.TowelId == towelId && bt.BoxId != boxId && bt.IsActive);

                if (alreadyPacked)
                    throw new Exception("El item está en otra caja.");

                // Agregar
                var boxTowel = new BoxTowel
                {
                    BoxId = boxId,
                    TowelId = towelId,
                    IsActive = true
                };

                _context.BoxTowel.Add(boxTowel);

                towel.TowelStatus = TowelStatus.PACKED.ToString();

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

                if (box.BoxStatus != BoxStatus.OPEN.ToString())
                    throw new Exception("La caja está cerrada.");

                var relation = await _context.BoxTowel
                    .FirstOrDefaultAsync(bt =>
                        bt.BoxId == boxId &&
                        bt.TowelId == towelId &&
                        bt.IsActive);

                if (relation == null)
                    throw new Exception("El item no pertenece a esta caja.");

                var towel = await _context.Towel.FirstOrDefaultAsync(t => t.Id == towelId && t.IsActive);

                if (towel == null)
                    throw new Exception("El item no existe.");

                if (towel.TowelStatus != TowelStatus.PACKED.ToString())
                    throw new Exception("El item no está empacado.");

                relation.IsActive = false;
                towel.TowelStatus = TowelStatus.LOOSE.ToString();
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
