using System.Diagnostics.CodeAnalysis;
using WebApplicationMVP.Welcome.Model;
using WebApplicationMVP.Welcome.View;

namespace WebApplicationMVP.Welcome.Presenter;

public class WelcomePresenter : IWelcomePresenter
{
    public WelcomeModel Model { get; set; }
    public IWelcomeView View { get; set; }

    public WelcomePresenter(IWelcomeView view, WelcomeModel model)
    {
        View = view;
        Model = model;

        WireUpEvents();
    }

    [ExcludeFromCodeCoverage]
    private static void WireUpEvents()
    {
    }
}
