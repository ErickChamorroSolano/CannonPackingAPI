using CannonPackingAPI.Common.Enums;
using CannonPackingAPI.Data;
using CannonPackingAPI.DTOs;
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

        public async Task CreateBox(BoxDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.BoxCode))
                    throw new Exception("El código de caja es requerido.");

                if (string.IsNullOrWhiteSpace(dto.ProductCode))
                    throw new Exception("El código de producto es requerido.");

                if (dto.Capacity <= 0)
                    throw new Exception("La capacidad debe ser mayor a 0.");

                var exists = await _context.Box.AnyAsync(b => b.BoxCode == dto.BoxCode);

                if (exists)
                    throw new Exception("Ya existe un código de caja registrado.");

                var box = new Box
                {
                    BoxCode = dto.BoxCode,
                    ProductCode = dto.ProductCode,
                    Capacity = dto.Capacity,
                    BoxStatus = BoxStatus.OPEN.ToString(),
                    IsActive = true
                };

                _context.Box.Add(box);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Box>> GetActiveBoxes()
        {
            try
            {
                return await _context.Box
                    .Where(x => x.IsActive)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task OpenBox(int boxId)
        {
            try
            {
                var box = await _context.Box.FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);
                if (box == null)
                    throw new Exception("La caja no existe.");
                if (box.BoxStatus != BoxStatus.CLOSED.ToString())
                    throw new Exception("La caja no está cerrada.");
                box.BoxStatus = BoxStatus.OPEN.ToString();
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task CloseBox(int boxId)
        {
            try
            {
                var box = await _context.Box.FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);
                if (box == null)
                    throw new Exception("La caja no existe.");
                var hasItems = await _context.BoxTowel
                    .AnyAsync(x => x.BoxId == boxId && x.IsActive);
                if (!hasItems)
                    throw new Exception("La caja no tiene items empacados.");
                box.BoxStatus = BoxStatus.CLOSED.ToString();
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DisableBox(int boxId)
        {
            try
            {
                var box = await _context.Box.FirstOrDefaultAsync(b => b.Id == boxId && b.IsActive);
                if (box == null)
                    throw new Exception("La caja no existe.");
                var hasItems = await _context.BoxTowel
                    .AnyAsync(x => x.BoxId == boxId && x.IsActive);
                if (hasItems)
                    throw new Exception("La caja tiene items empacados.");
                box.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
