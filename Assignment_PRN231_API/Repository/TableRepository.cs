using api_VS.Data;
using Assignment_PRN231_API.DTOs.Table;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationDBContext _context;

        public TableRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TableDto>> GetAllTablesAsync(int shopId)
        {
            return await _context.Tables
                .Where(t => t.ShopId == shopId)
                .Select(t => new TableDto
                {
                    Status = t.Status,
                    ShopId = t.ShopId,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<TableDto?> GetTableByIdAsync(int tableId)
        {
            return await _context.Tables
                .Where(t => t.TableId == tableId)
                .Select(t => new TableDto
                {
                    Status = t.Status,
                    ShopId = t.ShopId,
                    Name = t.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateTableAsync(TableDto tableDto)
        {
            var table = new Table
            {
                Status = tableDto.Status,
                ShopId = tableDto.ShopId,
                Name = tableDto.Name
            };

            _context.Tables.Add(table);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTableAsync(int tableId, TableDto tableDto)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null) return false;

            table.Status = tableDto.Status;
            table.Name = tableDto.Name;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null) return false;

            _context.Tables.Remove(table);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
