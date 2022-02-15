namespace GSoulavy.Template.WindowsService.Kestrel.Controllers;

using Microsoft.AspNetCore.Mvc;

using Models;

[ApiController]
[Route("[controller]")]
public class WindowsServiceController : ControllerBase
{
    private readonly ILogger<WindowsServiceController> _logger;

    public WindowsServiceController(ILogger<WindowsServiceController> logger) => _logger = logger;

    /// <summary>
    ///     Sends the print to the printer
    /// </summary>
    /// <param name="request"></param>
    /// <returns>A message contains the success of the dispatch</returns>
    /// <response code="202"></response>
    /// <response code="400"></response>
    /// <response code="500"></response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseRoot), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ResponseRoot), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseRoot), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Request request)
    {
        _logger.LogInformation("Post is called with {Name}", request.Name);
        await Task.Delay(1000);
        return Accepted(new ResponseRoot { Message = $"{request.Name} accepted" });
    }
}
