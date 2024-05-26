using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Strategies.Interfaces;

namespace Supermarket.Challenge.Services.Strategies.Implementations
{
    public class CoffeeStrategy : IPricingStrategy
    {
        public decimal CalculateTotalPrice(Product product)
        {
            var subTotal = product.Quantity * product.Price;

            if (product.Quantity >= 3)
            {
                var discountedPrice = (2m / 3) * product.Price; // Calculate the discounted price per unit
                subTotal = Math.Round(product.Quantity * discountedPrice, 2); // Calculate the subtotal with the discounted price
            }

            return subTotal;
        }
    }
}
