using api_VS.Data;
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

        public async Task<bool> CreateTableAsync(Table table)
        {
            _context.Tables.Add(table);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) return false;

            _context.Tables.Remove(table);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Table> GetTableByIdAsync(int id)
        {
            return await _context.Tables.Include(t => t.Shop).FirstOrDefaultAsync(t => t.TableId == id);
        }

        public async Task<List<Table>> GetAllTablesAsync()
        {
            return await _context.Tables.Include(t => t.Shop).ToListAsync();
        }

        public async Task<bool> UpdateTableStatusAsync(int id, bool status)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) return false;

            table.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
