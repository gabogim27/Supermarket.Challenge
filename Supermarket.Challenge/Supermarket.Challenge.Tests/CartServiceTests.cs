using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Services.Implementations;

namespace Supermarket.Challenge.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        private CartService _cartService;

        [SetUp]
        public void Setup()
        {
            _cartService = new CartService();
        }

        [Test]
        public void InsertOnCart_AddNewProduct_ProductAddedToCart()
        {
            // Arrange
            var product = new Product { Code = "GR1", Price = 3.11m };
            var quantity = 2;

            // Act
            _cartService.InsertOnCart(product, quantity);
            var productsInCart = _cartService.GetProductsOnCart();

            // Assert
            Assert.That(productsInCart.Count, Is.EqualTo(1));
            Assert.That(productsInCart[0].Code, Is.EqualTo("GR1"));
            Assert.That(productsInCart[0].Quantity, Is.EqualTo(2));
        }

        [Test]
        public void InsertOnCart_AddExistingProduct_QuantityUpdated()
        {
            // Arrange
            var product = new Product { Code = "GR1", Price = 3.11m };
            _cartService.InsertOnCart(product, 2);

            // Act
            _cartService.InsertOnCart(product, 3);
            var productsInCart = _cartService.GetProductsOnCart();

            // Assert
            Assert.That(productsInCart.Count, Is.EqualTo(1));
            Assert.That(productsInCart[0].Code, Is.EqualTo("GR1"));
            Assert.That(productsInCart[0].Quantity, Is.EqualTo(5));
        }

        [Test]
        public void GetProductsOnCart_WhenCalled_ReturnsAllProductsInCart()
        {
            // Arrange
            var product1 = new Product { Code = "GR1", Price = 3.11m };
            var product2 = new Product { Code = "SR1", Price = 5.00m };
            _cartService.InsertOnCart(product1, 2);
            _cartService.InsertOnCart(product2, 1);

            // Act
            var productsInCart = _cartService.GetProductsOnCart();

            // Assert
            Assert.That(productsInCart.Count, Is.EqualTo(2));
            Assert.IsTrue(productsInCart.Any(p => p.Code == "GR1" && p.Quantity == 2));
            Assert.IsTrue(productsInCart.Any(p => p.Code == "SR1" && p.Quantity == 1));
        }

        [Test]
        public void ClearCart_WhenCalled_RemovesAllProductsFromCart()
        {
            // Arrange
            var product1 = new Product { Code = "GR1", Price = 3.11m };
            var product2 = new Product { Code = "SR1", Price = 5.00m };
            _cartService.InsertOnCart(product1, 2);
            _cartService.InsertOnCart(product2, 1);

            // Act
            _cartService.ClearCart();
            var productsInCart = _cartService.GetProductsOnCart();

            // Assert
            Assert.That(productsInCart.Count, Is.EqualTo(0));
        }
    }
}