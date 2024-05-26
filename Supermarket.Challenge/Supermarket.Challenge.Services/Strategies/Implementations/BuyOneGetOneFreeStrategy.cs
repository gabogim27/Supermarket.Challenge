using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Strategies.Interfaces;

namespace Supermarket.Challenge.Services.Strategies.Implementations
{
    public class BuyOneGetOneFreeStrategy : IPricingStrategy
    {
        public decimal CalculateTotalPrice(Product product)
        {
            product.Quantity = product.Quantity %2 != 0 ? product.Quantity++ : product.Quantity;
            
            return product.Quantity / 2 * product.Price;
        }
    }
}
