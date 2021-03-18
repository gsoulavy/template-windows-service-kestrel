namespace GSoulavy.Template.WindowsService.Kestrel.Tests.Features
{
    using AutoFixture.Xunit2;

    using Fixtures;

    using Xbehave;

    public class WindowsServiceControllerFeature
    {
        [Scenario]
        [AutoData]
        public void WindowsServiceControllerAcceptsResults(WscPostFixture fixture)
        {
            var name = "John";
            var expected = $"{name} accepted";
            "GIVEN the controller is running".x(fixture.GivenTheControllerIsRunning);
            $"WHEN the endpoint is called with {name}".x(() => fixture.WhenTheEndpointIsCalledWith(name));
            "THEN returns accepted".x(fixture.ThenReturnsAccepted);
            "AND the content is ResponseRoot".x(fixture.AndTheContentIsResponseRoot);
            $"AND the result contains {expected}".x(() => fixture.AndTheResultContains(expected));
        }
    }
}
