using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Services.Interfaces;

namespace Supermarket.Challenge.Services.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly List<Product> SelectedProducts = new List<Product>();

        public void InsertOnCart(Product product, int quantity)
        {
            var prodObj = SelectedProducts.FirstOrDefault(x => x.Code == product.Code);
            if (prodObj != null)
            {
                prodObj.Quantity += quantity;
            }
            else
            {
                product.Quantity = quantity;
                SelectedProducts.Add(product);
            }
        }

        public List<Product> GetProductsOnCart()
        {
            return SelectedProducts;
        }

        public void ClearCart()
        {
            SelectedProducts.Clear();
        }
    }
}
