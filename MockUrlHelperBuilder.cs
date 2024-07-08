using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

namespace AW.Test.Utilities.Builders;

public class MockUrlHelperBuilder
{
    private readonly Mock<IUrlHelper> _urlHelper = new();
    private ActionContext? _actionContext;

    public MockUrlHelperBuilder WithActionContext(ActionContext actioncontext)
    {
        _actionContext = actioncontext;

        return this;
    }

    public MockUrlHelperBuilder WithActionContext(string page)
    {
        _actionContext = CreateActionContext(page);

        return this;
    }

    /// <summary>
    /// Set the callbackUrl which is returned by the IUrlHelper.Page extension method
    /// </summary>
    /// <param name="callbackUrl"></param>
    /// <returns>MockUrlHelperBuilder</returns>
    /// <remarks>Not possible to mock the Page method as this a static extension method.
    /// Page calls the underlying routeurl, so that mocked instead and returned through
    /// any page call</remarks>
    public MockUrlHelperBuilder SetPageCallbackUrl(string callbackUrl)
    {
        _urlHelper
            .Setup(h => h.RouteUrl(
                It.IsAny<UrlRouteContext>()))
            .Returns(callbackUrl);

        return this;
    }

    public Mock<IUrlHelper> Build()
    {
        // need to have an action context else the helper fails
        _actionContext ??= CreateActionContext("/home");

        _urlHelper
            .SetupGet(h => h.ActionContext)
            .Returns(_actionContext);

        return _urlHelper;
    }

    private ActionContext CreateActionContext(string page)
    {
        return new()
        {
            ActionDescriptor = new()
            {
                RouteValues = new Dictionary<string, string?> { { "page", page } }
            },
            RouteData = new()
            {
                Values = { ["page"] = page }
            }
        };
    }
}