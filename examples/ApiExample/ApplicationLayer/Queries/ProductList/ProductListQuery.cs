using DotnetCQRS.Queries;

namespace ApiExample.ApplicationLayer.Queries.ProductList;

public class ProductListQuery : IQuery<ProductListResult>
{
    public bool ShowOnlyActive { get; set; } = true;
}

public class ProductListResult
{
    public IReadOnlyList<Product> Products { get; set; }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}