using ApiExample.DataAccessLayer;
using DotnetCQRS;
using DotnetCQRS.Queries;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.ApplicationLayer.Queries.ViewCart
{
    public class ViewCartHandler : IQueryHandler<ViewCartQuery, ViewCartResult>
    {
        private readonly ExampleDbContext _context;

        public ViewCartHandler(ExampleDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ViewCartResult>> HandleAsync(ViewCartQuery query, CancellationToken cancellationToken)
        {
            var items = await _context.CartItems
                .Where(ci => ci.UserId == query.UserId)
                .ToListAsync(cancellationToken);

            var productIds = items.Select(ci => ci.ProductId).ToHashSet();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            var resultItems = new List<ViewCartResult.CartItem>();
            double total = 0;

            foreach(var item in items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null || product.IsActive == false || item.Quantity < 1)
                {
                    _context.CartItems.Remove(item);
                    continue;
                }

                if (product.Price != item.Price)
                {
                    item.Price = product.Price;
                    item.UpdateSubPrice();
                }

                total += item.SubPrice;
                resultItems.Add(new ViewCartResult.CartItem()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ItemPrice = item.Price.ToString("c"),
                    ProductName = product.Name,
                    SubTotal = item.SubPrice.ToString("c")
                });
            }

            var result = new ViewCartResult()
            {
                Items = resultItems,
                Total = total.ToString("c"),
            };
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(result);
        }
    }
}
