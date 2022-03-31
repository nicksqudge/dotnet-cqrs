using ApiExample.ApplicationLayer.Commands.HideProduct;
using ApiExample.ApplicationLayer.Commands.SaveProduct;
using ApiExample.ApplicationLayer.Commands.ShowProduct;
using ApiExample.ApplicationLayer.Queries.ProductList;
using DotnetCQRS;
using DotnetCQRS.Commands;
using DotnetCQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ProductController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> ProductList([FromQuery] ProductListQuery query)
    {
        var result = await _queryDispatcher
            .Run<ProductListQuery, ProductListResult>(query, HttpContext.RequestAborted);

        return ResultToStatusCode(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewProduct([FromBody] SaveProductCommand command)
    {
        var result = await _commandDispatcher.Run(command, HttpContext.RequestAborted);
        return ResultToStatusCode(result);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] SaveProductCommand command)
    {
        command.ProductId = productId;
        var result = await _commandDispatcher.Run(command, HttpContext.RequestAborted);
        return ResultToStatusCode(result);
    }

    [HttpPut("{productId}/show")]
    public async Task<IActionResult> ShowProduct(int productId)
    {
        var result = await _commandDispatcher.Run(new ShowProductCommand()
        {
            ProductId = productId
        }, HttpContext.RequestAborted);
        return ResultToStatusCode(result);
    }

    [HttpPut("{productId}/hide")]
    public async Task<IActionResult> HideProduct(int productId)
    {
        var result = await _commandDispatcher.Run(new HideProductCommand()
        {
            ProductId = productId
        }, HttpContext.RequestAborted);
        return ResultToStatusCode(result);
    }

    private IActionResult ResultToStatusCode(Result result)
    {
        if (result.IsSuccess)
            return NoContent();

        if (result.ErrorCode == ErrorCodes.NotFound)
            return NotFound();

        return BadRequest(result.ErrorCode);
    }

    private IActionResult ResultToStatusCode<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);
        
        if (result.ErrorCode == ErrorCodes.NotFound)
            return NotFound();

        return BadRequest(result.ErrorCode);
    }
}