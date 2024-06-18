using DemoApplication1.Models;

namespace DemoApplication1.Services
{
    public class ShoppingItemService
    {
        public double GetPricePerKg(ShoppingItem item)
        {
            return item.Price / item.Weight;
        }
    }
}
