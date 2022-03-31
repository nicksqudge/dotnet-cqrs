using DotnetCQRS.Commands;

namespace ApiExample.ApplicationLayer.Commands.HideProduct
{
    public class HideProductCommand : ICommand
    {
        public int ProductId { get; set; }
    }
}
