using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace WebApplication;

[ExcludeFromCodeCoverage]
public partial class LoginView : Page
{
    public const string WelcomeViewLocation = "../../Welcome/View/WelcomeView.aspx";
    public const string UserIdSessionKey = "UserId";
    private const string AdminUserName = "admin";
    private const string AdminPassword = "password";

    public string GetUserName() => Username.Text.Trim();

    public string GetPassword() => Password.Text.Trim();

    public void ShowMessage(string message) => Message.Text = message;

    public void Redirect(string url) => Response.Redirect(url);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        UserLoggedIn();
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        Login();
    }

    public void UserLoggedIn()
    {
        if (GetSessionValue(UserIdSessionKey) is not null)
        {
            RedirectToWelcomePage();
        }
    }

    public string? GetSessionValue(string sessionKey) => Session[sessionKey]?.ToString();
    public void SetSessionValue(string sessionKey, string value) => Session[sessionKey] = value;

    public void RedirectToWelcomePage() => Redirect(WelcomeViewLocation);

    public void Login()
    {
        var username = GetUserName();
        var password = GetPassword();

        if (!Validate(username, password)) return;

        SetSessionValue(UserIdSessionKey, username!);
        // Token = Guid.NewGuid().ToString();

        // Redirect to welcome page upon successful login
        RedirectToWelcomePage();
    }

    private static bool IsValidAdmin(string username, string password) =>
        username == AdminUserName && password == AdminPassword;

    public bool Validate(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Please enter username and password.");
            return false;
        }

        if (IsValidAdmin(username!, password!)) return true;

        ShowMessage("Invalid username or password.");
        return false;
    }
}
