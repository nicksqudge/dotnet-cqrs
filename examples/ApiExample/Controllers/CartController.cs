using ApiExample.ApplicationLayer.Commands.AddToCart;
using ApiExample.ApplicationLayer.Queries.ViewCart;
using DotnetCQRS.Commands;
using DotnetCQRS.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CartController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            command.UserId = GetUserId();
            var result = await _commandDispatcher.RunAsync(command, HttpContext.RequestAborted);
            return ResultToStatusCode(result);
        }

        [HttpGet]
        public async Task<IActionResult> View()
        {
            var result = await _queryDispatcher.RunAsync<ViewCartQuery, ViewCartResult>(new ViewCartQuery()
            {
                UserId = GetUserId(),
            }, HttpContext.RequestAborted);
            return ResultToStatusCode(result);
        }
    }
}
