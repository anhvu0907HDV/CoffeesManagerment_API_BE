using api_VS.Data;
using Assignment_PRN231_API.DTOs.Inventory;
using Assignment_PRN231_API.Models;
using Assignment_PRN231_API.Repository.IRepository;
using AutoMapper;
using Google;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN231_API.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDBContext _context;

        public InventoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoryItems(int shopId)
        {
            return await _context.Inventories.Include(igre => igre.Ingredient).Where(i => i.ShopId == shopId).ToListAsync();
        }

        public async Task<Inventory?> GetInventoryItemById(int ingredientId, int shopId)
        {
            return await _context.Inventories.Include(igre => igre.Ingredient).FirstOrDefaultAsync(i => i.IngredientId == ingredientId && i.ShopId == shopId);
        }

        public async Task<Inventory> CreateInventoryItem(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<Inventory> UpdateInventoryItem(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<bool> DeleteInventoryItem(int ingredientId, int shopId)
        {
            var inventory = await GetInventoryItemById(ingredientId, shopId);
            if (inventory == null)
            {
                return false;
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InventoryItemExists(int ingredientId, int shopId)
        {
            return await _context.Inventories.AnyAsync(i => i.IngredientId == ingredientId && i.ShopId == shopId);
        }
    }




}
