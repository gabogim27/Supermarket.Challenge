using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Strategies.Interfaces;

namespace Supermarket.Challenge.Services.Strategies.Implementations
{
    public class BulkPurchasesStrategy : IPricingStrategy
    {
        public decimal CalculateTotalPrice(Product product)
        {
            if (product.Quantity >= 3)
            {
                return product.Quantity * 4.50m;
            }

            return product.Quantity * product.Price;
        }
    }
}
