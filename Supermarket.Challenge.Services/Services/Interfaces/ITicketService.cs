using Supermarket.Challenge.Domain.Entities;

namespace Supermarket.Challenge.Services.Services.Interfaces
{
    public interface ITicketService
    {
        Ticket CalculateTicket(List<Product> products);
    }
}
