using Supermarket.Challenge.Domain.Entities;

namespace Supermarket.Challenge.Services.Services.Interfaces
{
    public interface ICartService
    {
        void InsertOnCart(Product product, int quantity);

        List<Product> GetProductsOnCart();
    }
}
