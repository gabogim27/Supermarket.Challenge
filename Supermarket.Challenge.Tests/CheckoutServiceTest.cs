using Moq;
using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Data;
using Supermarket.Challenge.Services.Services.Implementations;

namespace Supermarket.Challenge.Tests
{
    [TestFixture]
    public class CheckoutServiceTests
    {
        private Mock<IDataSource> _mockDataSource;
        private CheckoutService _checkoutService;

        [SetUp]
        public void Setup()
        {
            _mockDataSource = new Mock<IDataSource>();
            _checkoutService = new CheckoutService(_mockDataSource.Object);
        }

        [Test]
        public void Scan_ProductExists_ReturnsProduct()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Code = "GR1", Name = "Green tea", Price = 3.11m },
            new Product { Code = "SR1", Name = "Strawberries", Price = 5.00m }
        };
            _mockDataSource.Setup(ds => ds.GetProducts()).Returns(products);

            // Act
            var product = _checkoutService.Scan("GR1");

            // Assert
            Assert.IsNotNull(product);
            Assert.That(product.Code, Is.EqualTo("GR1"));
            Assert.That(product.Name, Is.EqualTo("Green tea"));
            Assert.That(product.Price, Is.EqualTo(3.11m));
        }

        [Test]
        public void Scan_ProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Code = "GR1", Name = "Green tea", Price = 3.11m },
            new Product { Code = "SR1", Name = "Strawberries", Price = 5.00m }
        };
            _mockDataSource.Setup(ds => ds.GetProducts()).Returns(products);

            // Act
            var product = _checkoutService.Scan("CF1");

            // Assert
            Assert.IsNull(product);
        }

        [Test]
        public void Scan_ProductsListInitiallyEmpty_LoadsProductsFromDataSource()
        {
            // Arrange
            _mockDataSource.Setup(ds => ds.GetProducts()).Returns(new List<Product>
        {
            new Product { Code = "GR1", Name = "Green tea", Price = 3.11m }
        });

            // Act
            var product = _checkoutService.Scan("GR1");

            // Assert
            Assert.IsNotNull(product);
            Assert.That(product.Code, Is.EqualTo("GR1"));
            _mockDataSource.Verify(ds => ds.GetProducts(), Times.Once);
        }

        [Test]
        public void Scan_CalledMultipleTimes_LoadsProductsOnlyOnce()
        {
            // Arrange
            _mockDataSource.Setup(ds => ds.GetProducts()).Returns(new List<Product>
        {
            new Product { Code = "GR1", Name = "Green tea", Price = 3.11m },
            new Product { Code = "SR1", Name = "Strawberries", Price = 5.00m }
        });

            // Act
            var product1 = _checkoutService.Scan("GR1");
            var product2 = _checkoutService.Scan("SR1");

            // Assert
            Assert.IsNotNull(product1);
            Assert.IsNotNull(product2);
            _mockDataSource.Verify(ds => ds.GetProducts(), Times.Once);
        }
    }
}
