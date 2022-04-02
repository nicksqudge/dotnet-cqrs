using DotnetCQRS;
using DotnetCQRS.Queries;

namespace ApiExample.ApplicationLayer.Queries.ViewCart
{


    public class ViewCartQuery : IQuery<ViewCartResult>
    {
    }

    public class ViewCartResult
    {

    }

    public class ViewCartHandler : IQueryHandler<ViewCartQuery, ViewCartResult>
    {
        public Task<Result<ViewCartResult>> HandleAsync(ViewCartQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
