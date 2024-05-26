using Supermarket.Challenge.Domain.Entities;

namespace Supermarket.Challenge.Services.Data
{
    public class ProductsDataSource : IDataSource
    {
        public List<Product> GetProducts()
        {
            return JsonReader.DataFromJson!.Products!;
        }
    }
}
