namespace Supermarket.Challenge.Domain.Entities
{
    public class Ticket
    {
        public List<TicketDetail> Products { get; set; } = new List<TicketDetail>();

        public decimal Total { get; set; }
    }
}
