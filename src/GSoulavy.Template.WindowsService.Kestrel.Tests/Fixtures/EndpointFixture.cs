namespace GSoulavy.Template.WindowsService.Kestrel.Tests.Fixtures;

using System.Threading.Tasks;

using Controllers;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Models;

using Moq;

public class EndpointFixture
{
    private readonly Mock<ILogger<WindowsServiceController>> _logger;
    private readonly WindowsServiceController _windowsServiceController;
    private ActionResult _result;

    public EndpointFixture()
    {
        _logger = new Mock<ILogger<WindowsServiceController>>();
        _windowsServiceController = new WindowsServiceController(_logger.Object);
    }

    public void GivenTheControllerIsRunning()
    {
        // Set up pre-conditions
    }

    public async Task WhenTheEndpointIsCalledWith(string name) =>
        _result = await _windowsServiceController.Post(new Request { Name = name });

    public void ThenReturnsAccepted() => _result.Should().BeOfType<AcceptedResult>();

    public void AndTheContentIsResponseRoot() => (_result as AcceptedResult)!.Value.Should().BeOfType<ResponseRoot>();

    public void AndTheResultContains(string expected) =>
        ((_result as AcceptedResult)!.Value as ResponseRoot)!.Message.Should().Be(expected);
}
