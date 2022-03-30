using ApiExample.ApplicationLayer.Commands.SaveProduct;
using DotnetCQRS;
using DotnetCQRS.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        public ProductController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProduct([FromBody] SaveProductCommand command)
        {
            var result = await commandDispatcher.Run(command, HttpContext.RequestAborted);
            return ResultToStatusCode(result);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] SaveProductCommand command)
        {
            command.ProductId = productId;
            var result = await commandDispatcher.Run(command, HttpContext.RequestAborted);
            return ResultToStatusCode(result);
        }

        private IActionResult ResultToStatusCode(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            
            if(result.ErrorCode == ErrorCodes.NotFound)
                return NotFound();
            
            return BadRequest(result.ErrorCode);
        }
    }
}
