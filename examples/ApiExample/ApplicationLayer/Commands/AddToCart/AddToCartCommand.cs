using DotnetCQRS.Commands;

namespace ApiExample.ApplicationLayer.Commands.AddToCart
{
    public class AddToCartCommand : ICommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public int UserId { get; set; }
    }
}
