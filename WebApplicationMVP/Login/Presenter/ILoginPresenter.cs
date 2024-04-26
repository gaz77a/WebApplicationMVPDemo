using System;
using WebApplicationMVP.Login.Model;
using WebApplicationMVP.Login.View;

namespace WebApplicationMVP.Login.Presenter;

public interface ILoginPresenter
{
    public LoginModel Model { get; set; }
    public ILoginView? GetView();
    public void UnloadView();
    public void Login();
    public void UserLoggedIn();
    public bool Validate(string username, string password);
    public void RedirectToWelcomePage();
}
