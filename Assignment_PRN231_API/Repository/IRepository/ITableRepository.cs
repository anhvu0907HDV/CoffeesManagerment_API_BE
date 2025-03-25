using Assignment_PRN231_API.DTOs.Table;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface ITableRepository
    {
        Task<IEnumerable<TableDto>> GetAllTablesAsync(int shopId);
        Task<TableDto?> GetTableByIdAsync(int tableId);
        Task<bool> CreateTableAsync(TableDto table);
        Task<bool> UpdateTableAsync(int tableId, TableDto table);
        Task<bool> DeleteTableAsync(int tableId);
    }
}
