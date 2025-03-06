using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using Assignment_PRN231_API.Service.IService;

namespace Assignment_PRN231_API.Service
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<bool> CreateTableAsync(Table table)
        {
            return await _tableRepository.CreateTableAsync(table);
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            return await _tableRepository.DeleteTableAsync(id);
        }

        public async Task<Table> GetTableByIdAsync(int id)
        {
            return await _tableRepository.GetTableByIdAsync(id);
        }

        public async Task<List<Table>> GetAllTablesAsync()
        {
            return await _tableRepository.GetAllTablesAsync();
        }

        public async Task<bool> UpdateTableStatusAsync(int id, bool status)
        {
            return await _tableRepository.UpdateTableStatusAsync(id, status);
        }
    }
}
