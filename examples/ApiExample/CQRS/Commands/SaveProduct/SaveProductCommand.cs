using DotnetCQRS.Commands;

namespace ApiExample.CQRS.Commands.SaveProduct
{
    public class SaveProductCommand : ICommand
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
    }
}
