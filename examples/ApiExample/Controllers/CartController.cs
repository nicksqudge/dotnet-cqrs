using ApiExample.ApplicationLayer.Commands.AddToCart;
using DotnetCQRS.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CartController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            var result = await _commandDispatcher.RunAsync(command, HttpContext.RequestAborted);
            return ResultToStatusCode(result);
        }
    }
}
