using Moq;
using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Services.Implementations;
using Supermarket.Challenge.Services.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Challenge.Tests
{
    [TestFixture]
    public class TicketServiceTests
    {
        private TicketService _ticketService;
        private Dictionary<string, IPricingStrategy> _strategies;

        [SetUp]
        public void Setup()
        {
            _strategies = new Dictionary<string, IPricingStrategy>();
            _ticketService = new TicketService(_strategies);
        }

        [Test]
        public void CalculateTicket_WithNoPricingStrategy_ReturnsCorrectTotal()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Code = "GR1", Name = "Green tea", Price = 3.11m, Quantity = 1 },
            new Product { Code = "SR1", Name = "Strawberries", Price = 5.00m, Quantity = 2 }
        };

            // Act
            var ticket = _ticketService.CalculateTicket(products);

            // Assert
            Assert.That(ticket.Total, Is.EqualTo(13.11m));
            Assert.That(ticket.Products.Count, Is.EqualTo(2));
            Assert.That(ticket.Products[0].Subtotal, Is.EqualTo(3.11m));
            Assert.That(ticket.Products[1].Subtotal, Is.EqualTo(10.00m));
        }

        [Test]
        public void CalculateTicket_WithPricingStrategy_ReturnsCorrectTotal()
        {
            // Arrange
            var product = new Product { Code = "GR1", Name = "Green tea", Price = 3.11m, Quantity = 2 };
            var mockStrategy = new Mock<IPricingStrategy>();
            mockStrategy.Setup(s => s.CalculateTotalPrice(product)).Returns(3.11m);

            _strategies[product.Code] = mockStrategy.Object;

            var products = new List<Product> { product };

            // Act
            var ticket = _ticketService.CalculateTicket(products);

            // Assert
            Assert.That(ticket.Total, Is.EqualTo(3.11m));
            Assert.That(ticket.Products.Count, Is.EqualTo(1));
            Assert.That(ticket.Products[0].Subtotal, Is.EqualTo(3.11m));
            mockStrategy.Verify(s => s.CalculateTotalPrice(product), Times.Once);
        }

        [Test]
        public void CalculateTicket_WithMultipleProductsAndStrategies_ReturnsCorrectTotal()
        {
            // Arrange
            var product1 = new Product { Code = "GR1", Name = "Green tea", Price = 3.11m, Quantity = 2 };
            var product2 = new Product { Code = "SR1", Name = "Strawberries", Price = 5.00m, Quantity = 3 };
            var mockStrategy1 = new Mock<IPricingStrategy>();
            var mockStrategy2 = new Mock<IPricingStrategy>();

            mockStrategy1.Setup(s => s.CalculateTotalPrice(product1)).Returns(3.11m);
            mockStrategy2.Setup(s => s.CalculateTotalPrice(product2)).Returns(13.50m);

            _strategies[product1.Code] = mockStrategy1.Object;
            _strategies[product2.Code] = mockStrategy2.Object;

            var products = new List<Product> { product1, product2 };

            // Act
            var ticket = _ticketService.CalculateTicket(products);

            // Assert
            Assert.That(ticket.Total, Is.EqualTo(16.61m));
            Assert.That(ticket.Products.Count, Is.EqualTo(2));
            Assert.That(ticket.Products[0].Subtotal, Is.EqualTo(3.11m));
            Assert.That(ticket.Products[1].Subtotal, Is.EqualTo(13.50m));
            mockStrategy1.Verify(s => s.CalculateTotalPrice(product1), Times.Once);
            mockStrategy2.Verify(s => s.CalculateTotalPrice(product2), Times.Once);
        }
    }
}
