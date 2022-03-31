using ApiExample.DataAccessLayer;
using DotnetCQRS;
using DotnetCQRS.Commands;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.ApplicationLayer.Commands.ShowProduct
{
    public class ShowProductHandler : ICommandHandler<ShowProductCommand>
    {
        private readonly ExampleDbContext _context;

        public ShowProductHandler(ExampleDbContext context)
        {
            _context = context;
        }

        public async Task<Result> HandleAsync(ShowProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);
            if (product == null)
                return Result.Failure(ErrorCodes.NotFound);

            product.IsActive = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
