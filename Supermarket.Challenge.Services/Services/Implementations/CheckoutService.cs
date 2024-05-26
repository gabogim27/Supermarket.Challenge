using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Data;
using Supermarket.Challenge.Services.Services.Interfaces;

namespace Supermarket.Challenge.Services.Services.Implementations
{
    public class CheckoutService : ICheckoutService
    {
        private List<Product> Products = new List<Product>();

        private readonly IDataSource _proDataSource;

        public CheckoutService(IDataSource proDataSource)
        {
            _proDataSource = proDataSource;
        }

        public Product? Scan(string code)
        {
            if (Products?.Count == 0)
            {
                Products = _proDataSource.GetProducts();
            }

            return Products?.FirstOrDefault(x => x.Code == code);
        }
    }
}
