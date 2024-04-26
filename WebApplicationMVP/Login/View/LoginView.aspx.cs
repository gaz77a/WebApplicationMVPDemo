using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using WebApplicationMVP.Login.Model;
using WebApplicationMVP.Login.Presenter;

namespace WebApplicationMVP.Login.View;

[ExcludeFromCodeCoverage]
public partial class LoginView : Page, ILoginView
{
    private const string LoginModelSessionKey = "LoginModel";

    public event EventHandler? LoginClicked;
    public event EventHandler? UserLoggedIn;

    private ILoginPresenter? Presenter { get; set; }
     
    public LoginModel? Model
    {
        get => Session[LoginModelSessionKey] as LoginModel;
        set => Session[LoginModelSessionKey] = value;
    }

    public string GetUserName() => Username.Text.Trim();

    public string GetPassword() => Password.Text.Trim();
    
    public void ShowMessage(string message) => Message.Text = message;

    public void Redirect(string url) => Response.Redirect(url);

    protected void Page_Load(object sender, EventArgs e)
    {
        Model ??= new LoginModel();
        Presenter = new LoginPresenter(this, Model);

        if (IsPostBack) return;

        UserLoggedIn?.Invoke(this, EventArgs.Empty);
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        LoginClicked?.Invoke(this, EventArgs.Empty);
    }

    public override void Dispose()
    {
        Presenter?.UnloadView();
        Presenter = null;
        base.Dispose();
    }

    public string? GetSessionValue(string sessionKey) => Session[sessionKey]?.ToString();
    public void SetSessionValue(string sessionKey, string value) => Session[sessionKey] = value;
}
