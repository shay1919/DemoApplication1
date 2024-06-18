using DemoApplication1.DbContexts;
using DemoApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication1.Services
{
    public class ShoppingItemRepository
    {
        ShoppingItemContext _context;

        public ShoppingItemRepository(ShoppingItemContext context)
        {
            _context = context;
        }

        public async Task AddItemAsync(ShoppingItem item)
        {
            _context.Items.Add(item);
        }

        public async Task<IEnumerable<ShoppingItem>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<ShoppingItem> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public void DeleteItemByIdAsync(ShoppingItem item)
        {
            _context.Items.Remove(item);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
