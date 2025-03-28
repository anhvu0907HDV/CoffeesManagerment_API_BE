using Assignment_PRN231_API.DTOs.Table;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface ITableRepository
    {
        Task<bool> CreateTableAsync(Table table);
        Task<bool> DeleteTableAsync(int id);
        Task<Table> GetTableByIdAsync(int id);
        Task<IEnumerable<TableDto>> GetAllTablesByShopIdAsync(int shopId);
        Task<bool> UpdateTableStatusAsync(int id, bool status);
        Task<List<Table>> GetTablesByShopIdAsync(int shopId);
        Task<bool> CreateTableAsync(TableDto tableDto);
        Task<bool> UpdateTableAsync(int tableId, TableDto tableDto);
        Task<List<TableDto>> GetAllTablesAsync();
    }
}
