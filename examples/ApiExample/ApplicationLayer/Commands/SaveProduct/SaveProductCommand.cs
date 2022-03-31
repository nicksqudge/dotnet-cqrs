using DotnetCQRS.Commands;

namespace ApiExample.ApplicationLayer.Commands.SaveProduct;

public class SaveProductCommand : ICommand
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    public bool HasProductId()
    {
        return ProductId != 0;
    }
}