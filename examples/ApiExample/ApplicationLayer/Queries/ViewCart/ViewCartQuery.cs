using DotnetCQRS.Queries;

namespace ApiExample.ApplicationLayer.Queries.ViewCart
{
    public class ViewCartQuery : IQuery<ViewCartResult>
    {
        public int UserId { get; set; }
    }
}
