using Supermarket.Challenge.Domain.Entities;
using Supermarket.Challenge.Services.Services.Interfaces;
using Supermarket.Challenge.Services.Strategies.Interfaces;

namespace Supermarket.Challenge.Services.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly Dictionary<string, IPricingStrategy> _strategies;

        public TicketService(Dictionary<string, IPricingStrategy> strategies)
        {
            _strategies = strategies;
        }

        public Ticket CalculateTicket(List<Product> products)
        {
            var ticket = new Ticket();
            foreach (var product in products)
            {
                var ticketDetail = new TicketDetail();
                ticketDetail.Product = product;

                if (_strategies.TryGetValue(product.Code, out var strategy))
                {
                    ticketDetail.Subtotal = strategy.CalculateTotalPrice(product);
                }
                else
                {
                    ticketDetail.Subtotal = product.Quantity * product.Price;
                }

                ticket.Products.Add(ticketDetail);
                ticket.Total += ticketDetail.Subtotal;
            }

            return ticket;
        }
    }
}
