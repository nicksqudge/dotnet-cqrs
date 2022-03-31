using DotnetCQRS;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ResultToStatusCode(Result result)
        {
            if (result.IsSuccess)
                return NoContent();

            if (result.ErrorCode == ErrorCodes.NotFound)
                return NotFound();

            return BadRequest(result.ErrorCode);
        }

        protected IActionResult ResultToStatusCode<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            if (result.ErrorCode == ErrorCodes.NotFound)
                return NotFound();

            return BadRequest(result.ErrorCode);
        }
    }
}
