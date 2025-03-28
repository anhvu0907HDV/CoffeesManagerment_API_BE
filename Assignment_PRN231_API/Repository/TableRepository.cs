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

        public async Task<IEnumerable<TableDto>> GetAllTablesByShopIdAsync(int shopId)
        {
            return await _context.Tables
                .Where(t => t.ShopId == shopId)
                .Select(t => new TableDto
                {
                    TableId = t.TableId,
                    Status = t.Status,
                    ShopId = t.ShopId,
                    Name = t.Name
                })
                .ToListAsync();
        }
        public async Task<List<TableDto>> GetAllTablesAsync()
        {
            return await _context.Tables
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
            bool isTableInUse = await _context.TableOrders.AnyAsync(o => o.TableId == tableId);
            if (isTableInUse)
            {
                return false; 
            }
            _context.Tables.Remove(table);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Table>> GetTablesByShopIdAsync(int shopId)
        {
            // Tìm tất cả các bàn (table) có ShopId phù hợp và trả về toàn bộ thông tin của bàn
            var tables = await _context.Tables
                .Where(t => t.ShopId == shopId)
                .Include(t => t.Shop) // Bao gồm thông tin về Shop
                .ToListAsync();

            return tables; // Trả về danh sách bàn
        }

        public Task<bool> CreateTableAsync(Table table)
        {
            throw new NotImplementedException();
        }

        Task<Table> ITableRepository.GetTableByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTableStatusAsync(int id, bool status)
        {
            throw new NotImplementedException();
        }
    }

}
