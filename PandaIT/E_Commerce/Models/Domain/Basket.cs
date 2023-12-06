using E_Commerce.Interface;

using System.Collections.Generic;
using System.Linq;

namespace E_Commerce.Models.Domain
{
    public class Basket : IUserEntity<object>
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public ICollection<BasketItem> BasketItem { get; set; } = new List<BasketItem>();

        // Other properties and methods...

        public void AddItem(Product product, int quantity)
        {
            // If the product is not available it will add the product to the cart
            if (BasketItem.All(item => item.ProductId != product.Id))
            {
                BasketItem.Add(new BasketItem { Product = product, Quantity = quantity });
                return;
            }
            var isExist = BasketItem.FirstOrDefault(i => i.ProductId == product.Id);
            if (isExist != null)
                isExist.Quantity += quantity;
        }

        public void RemoveItem(int productId, int quantity = 1)
        {
            // if the product is there
            var item = BasketItem.FirstOrDefault(basketItem => basketItem.ProductId == productId);
            if (item == null) return;
            // decrease the quantity
            item.Quantity -= quantity;
            // When the quantity is 0, the product will be removed from the basket
            if (item.Quantity <= 0) BasketItem.Remove(item);
        }
    }
}
