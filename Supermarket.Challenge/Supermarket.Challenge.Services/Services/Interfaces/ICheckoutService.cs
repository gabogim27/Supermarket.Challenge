using Supermarket.Challenge.Domain.Entities;

namespace Supermarket.Challenge.Services.Services.Interfaces
{
    public interface ICheckoutService
    {
        Product? Scan(string code);
    }
}
