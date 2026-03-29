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
                    Status = BoxStatus.OPEN.ToString(),
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

        public async Task<List<BoxesDto>> GetActiveBoxes()
        {
            try
            {
                return await _context.Box
                    .Where(b => b.IsActive)
                    .Select(bd => new BoxesDto
                    {
                        Id = bd.Id,
                        BoxCode = bd.BoxCode,
                        ProductCode = bd.ProductCode,
                        Capacity = bd.Capacity,
                        Status = bd.Status,
                        CurrentCount = (short)bd.Towels.Count(t => t.IsActive && t.Status == TowelStatus.PACKED.ToString()),
                        IsActive = bd.IsActive,
                        Towels = bd.Towels.Where(t => t.IsActive).Select(td => new TowelsDto { 
                            Id = td.Id,
                            ItemCode = td.ItemCode,
                            ProductCode = td.ProductCode,
                            BoxId = td.BoxId,
                            BoxCode = bd.BoxCode,
                            Status = td.Status,
                            IsActive = td.IsActive
                        }).ToList()
                    })
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
                if (box.Status != BoxStatus.CLOSED.ToString())
                    throw new Exception("La caja no está cerrada.");
                box.Status = BoxStatus.OPEN.ToString();
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
                var hasItems = await _context.Towel
                    .AnyAsync(t => t.BoxId == boxId && t.IsActive && t.Status == TowelStatus.PACKED.ToString());
                if (!hasItems)
                    throw new Exception("La caja no tiene items empacados.");
                box.Status = BoxStatus.CLOSED.ToString();
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
                var hasItems = await _context.Towel
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
