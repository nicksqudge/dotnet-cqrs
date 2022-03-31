using ApiExample.DataAccessLayer;
using DotnetCQRS;
using DotnetCQRS.Queries;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.ApplicationLayer.Queries.ProductList;

public class ProductListHandler : IQueryHandler<ProductListQuery, ProductListResult>
{
    private readonly ExampleDbContext _context;

    public ProductListHandler(ExampleDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductListResult>> HandleAsync(ProductListQuery query, CancellationToken cancellationToken)
    {
        var productQuery = _context.Products.AsQueryable();

        if (query.ShowOnlyActive)
            productQuery = productQuery.Where(q => q.IsActive == true);

        var products = await productQuery
            .Select(p => new ProductListResult.Product()
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price.ToString("c")
            })
            .ToListAsync(cancellationToken);

        return Result.Success(new ProductListResult()
        {
            Products = products
        });
    }
}