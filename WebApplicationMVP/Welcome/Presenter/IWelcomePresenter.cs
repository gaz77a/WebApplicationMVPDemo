using WebApplicationMVP.Welcome.Model;
using WebApplicationMVP.Welcome.View;

namespace WebApplicationMVP.Welcome.Presenter;

public interface IWelcomePresenter
{
    public WelcomeModel Model { get; set; }
    public IWelcomeView View { get; set; }
}
