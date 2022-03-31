using DotnetCQRS.Commands;

namespace ApiExample.ApplicationLayer.Commands.ShowProduct
{
    public class ShowProductCommand : ICommand
    {
        public int ProductId { get; set; }
    }
}
