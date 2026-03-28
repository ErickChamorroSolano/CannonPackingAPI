using CannonPackingAPI.Common.Enums;
using CannonPackingAPI.Data;
using CannonPackingAPI.DTOs;
using CannonPackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CannonPackingAPI.Services
{
    public class TowelService
    {
        private readonly AppDbContext _context;

        public TowelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTowel(TowelDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.ItemCode))
                    throw new Exception("El código del item es requerido.");

                if (string.IsNullOrWhiteSpace(dto.ProductCode))
                    throw new Exception("El código del producto es requerido.");

                var exists = await _context.Towel.AnyAsync(t => t.ItemCode == dto.ItemCode);

                if (exists)
                    throw new Exception("El código del item ya existe.");

                var towel = new Towel
                {
                    ItemCode = dto.ItemCode,
                    ProductCode = dto.ProductCode,
                    TowelStatus = TowelStatus.LOOSE.ToString(),
                    IsActive = true
                };

                _context.Towel.Add(towel);
                await _context.SaveChangesAsync();

                //return Ok(new
                //{
                //    towel.Id,
                //    towel.ItemCode,
                //    towel.ProductCode,
                //    towel.TowelStatus,
                //    towel.IsActive
                //});
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Towel>> GetActiveTowels()
        {
            try
            {
                return await _context.Towel
                    .Where(t => t.IsActive)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        //Eliminar item (solo si no está empacado)
        public async Task DisableTowel(int id)
        {
            try
            {
                var towel = await _context.Towel.FindAsync(id);
                if (towel == null)
                    throw new Exception("El item no existe.");
                if (towel.TowelStatus == TowelStatus.PACKED.ToString())
                    throw new Exception("No se puede deshabilitar un item empacado.");
                towel.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
