using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface ITableRepository
    {
        Task<bool> CreateTableAsync(Table table);
        Task<bool> DeleteTableAsync(int id);
        Task<Table> GetTableByIdAsync(int id);
        Task<List<Table>> GetAllTablesAsync();
        Task<bool> UpdateTableStatusAsync(int id, bool status);
        Task<List<Table>> GetTablesByShopIdAsync(int shopId);
    }
}
