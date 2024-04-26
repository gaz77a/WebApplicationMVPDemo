using System.Diagnostics.CodeAnalysis;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WebApplicationMVP;

[ExcludeFromCodeCoverage]
public static class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        var settings = new FriendlyUrlSettings
        {
            AutoRedirectMode = RedirectMode.Permanent
        };
        routes.EnableFriendlyUrls(settings);
    }
}