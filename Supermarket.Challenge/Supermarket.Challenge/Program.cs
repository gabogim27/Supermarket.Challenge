using Supermarket.Challenge.Services.Services.Implementations;
using Supermarket.Challenge.Services.Services.Interfaces;
using Supermarket.Challenge.Services.Strategies.Implementations;
using Supermarket.Challenge.Services.Strategies.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supermarket.Challenge.Services.Data;
using Supermarket.Challenge.Handlers.Interfaces;
using Supermarket.Challenge.Handlers.Implementations;

public class Program
{
    static void Main(string[] args)
    {
        var strategies = new Dictionary<string, IPricingStrategy>
        {
            { "GR1", new BuyOneGetOneFreeStrategy() },
            { "SR1", new BulkPurchasesStrategy() },
            { "CF1", new CoffeeStrategy() }
        };

        var serviceProvider = new ServiceCollection()
            .AddScoped<IJsonReader, JsonReader>()
            .AddScoped<IDataSource, ProductsDataSource>()
            .AddScoped<ICheckoutService, CheckoutService>()
            .AddScoped<ICartService, CartService>()
            .AddScoped<IUserInputHandler, UserInputHandler>()
            .AddScoped<ITicketService>(p => new TicketService(strategies))
            .BuildServiceProvider();

        var config = GetConfiguration();
        var jsonReader = serviceProvider.GetRequiredService<IJsonReader>();

        jsonReader.ReadDataFromJson(config["JsonFilePath"]!);

        var uiHandler = serviceProvider.GetRequiredService<IUserInputHandler>();

        uiHandler.Run();
    }

    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }
}