using System;

namespace WebApplicationMVP.Login.View;

public interface ILoginView
{
    public event EventHandler? LoginClicked;
    public event EventHandler? UserLoggedIn;
    public string GetUserName();
    public string GetPassword();
    public void ShowMessage(string message);
    public void Redirect(string url);
    public string? GetSessionValue(string sessionKey);
    public void SetSessionValue(string sessionKey, string value);
}
