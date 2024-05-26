using Supermarket.Challenge.Domain.Entities;

namespace Supermarket.Challenge.Services.Strategies.Interfaces
{
    public interface IPricingStrategy
    {
        decimal CalculateTotalPrice(Product product);
    }
}