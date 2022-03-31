using ApiExample.DataAccessLayer;
using DotnetCQRS;
using DotnetCQRS.Commands;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.ApplicationLayer.Commands.HideProduct
{
    public class HideProductHandler : ICommandHandler<HideProductCommand>
    {
        private readonly ExampleDbContext _context;

        public HideProductHandler(ExampleDbContext context)
        {
            _context = context;
        }

        public async Task<Result> HandleAsync(HideProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);
            if (product == null)
                return Result.Failure(ErrorCodes.NotFound);

            product.IsActive = false;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
