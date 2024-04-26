using System;
using System.Diagnostics.CodeAnalysis;

using WebApplicationMVP.Login.Model;
using WebApplicationMVP.Login.View;

namespace WebApplicationMVP.Login.Presenter;

public class LoginPresenter: ILoginPresenter
{
    public const string WelcomeViewLocation = "../../Welcome/View/WelcomeView.aspx";
    public const string UserIdSessionKey = "UserId";
    private const string AdminUserName = "admin";
    private const string AdminPassword = "password";

    public LoginModel Model { get; set; }
    private ILoginView? View { get; set; }

    public ILoginView? GetView() => View;

    public void UnloadView() => View = null;

    public LoginPresenter(ILoginView view, LoginModel model)
    {
        View = view;
        Model = model;

        WireUpEvents();
    }

    [ExcludeFromCodeCoverage]
    private void OnLoginClicked(object sender, EventArgs e)
    {
        Login();
    }

    [ExcludeFromCodeCoverage]
    private void OnUserLoggedIn(object sender, EventArgs e)
    {
        UserLoggedIn();
    }

    [ExcludeFromCodeCoverage]
    private void WireUpEvents()
    {
        View!.LoginClicked += OnLoginClicked;
        View.UserLoggedIn += OnUserLoggedIn;
    }

    public void RedirectToWelcomePage() => View?.Redirect(WelcomeViewLocation);

    public void UserLoggedIn()
    {
        if (View?.GetSessionValue(UserIdSessionKey) is not null)
        {
            RedirectToWelcomePage();
        }
    }

    public void Login()
    {
        var username = View?.GetUserName();
        var password = View?.GetPassword();

        if (!Validate(username, password)) return;

        View?.SetSessionValue(UserIdSessionKey, username!);
        Model.Token = Guid.NewGuid().ToString();

        // Redirect to welcome page upon successful login
        RedirectToWelcomePage();
    }

    private static bool IsValidAdmin(string username, string password) =>
        username == AdminUserName && password == AdminPassword;

    public bool Validate(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            View?.ShowMessage("Please enter username and password.");
            return false;
        }

        if (IsValidAdmin(username!, password!)) return true;

        View?.ShowMessage("Invalid username or password.");
        return false;
    }
}
