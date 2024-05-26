using Supermarket.Challenge.Handlers.Interfaces;
using Supermarket.Challenge.Services.Services.Interfaces;

namespace Supermarket.Challenge.Handlers.Implementations
{
    public class UserInputHandler : IUserInputHandler
    {
        private readonly ICheckoutService _checkoutService;

        private readonly ICartService _cartService;

        private readonly ITicketService _ticketService;

        public UserInputHandler(ICheckoutService checkoutService, ICartService cartService, ITicketService ticketService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _ticketService = ticketService;
        }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Enter product codes (type 'EXIT' to finish):");
                var input = Console.ReadLine().Trim();

                if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    isRunning = false;
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(input))
                {
                    HandleInput(input);
                }
                else
                {
                    Console.WriteLine("Invalid product code. Please try again.");
                }
            }

            ShowTicket();
        }

        private void HandleInput(string input)
        {
            var prod = _checkoutService.Scan(input.ToUpperInvariant());
            if (!string.IsNullOrWhiteSpace(prod?.Name))
            {
                var quantity = 0;
                do
                {
                    Console.WriteLine($"Product Selected: {prod.Name}, Quantity?");
                } while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0);

                _cartService.InsertOnCart(prod, quantity);
                Console.Clear();
                Console.WriteLine($"Product {prod.Name} and quantity: {quantity} added to Cart");
            }
            else
            {
                Console.WriteLine("Error: Incorrect Product code, please enter a valid Code (GR1, SR1, CF1).");
            }
        }

        public void ShowTicket()
        {
            var productsOnCart = _cartService.GetProductsOnCart();
            var ticket = _ticketService.CalculateTicket(productsOnCart);
            Console.Clear();

            if (ticket?.Products?.Count > 0)
            {
                foreach (var prod in ticket.Products)
                {
                    Console.WriteLine($"Product: {prod.Product.Name}, quantity: {prod.Product.Quantity}, SubTotal: {prod.Subtotal}");
                }

                Console.WriteLine($"Total price: $ {ticket.Total}");
            }
        }
    }
}
