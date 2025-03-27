using Assignment_PRN231_API.DTOs.Inventory;
using Assignment_PRN231_API.Models;

namespace Assignment_PRN231_API.Repository.IRepository
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventoryItems(int shopId);
        Task<Inventory?> GetInventoryItemById(int ingredientId, int shopId);
        Task<Inventory> CreateInventoryItem(Inventory inventory);
        Task<Inventory> UpdateInventoryItem(Inventory inventory);
        Task<bool> DeleteInventoryItem(int ingredientId, int shopId);
        Task<bool> InventoryItemExists(int ingredientId, int shopId);
    }

}
