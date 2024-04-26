using Moq.AutoMock;
using WebApplicationMVP.Login.Model;
using WebApplicationMVP.Login.Presenter;
using WebApplicationMVP.Login.View;

namespace WebApplicationMVP.UnitTests.Login.Builder;

public class LoginPresenterBuilder
{
    private readonly AutoMocker _mocker = new();

    public LoginPresenterBuilder()
    {
        var viewMock = _mocker.GetMock<ILoginView>();
        viewMock.Setup(v => v.GetUserName()).Returns("admin");
        viewMock.Setup(v => v.GetPassword()).Returns("password");
    }

    public LoginPresenterBuilder WithUserName(string userName)
    {
        var viewMock = _mocker.GetMock<ILoginView>();
        viewMock.Setup(v => v.GetUserName()).Returns(userName);

        return this;
    }

    public LoginPresenterBuilder WithPassword(string password)
    {
        var viewMock = _mocker.GetMock<ILoginView>();
        viewMock.Setup(v => v.GetPassword()).Returns(password);

        return this;
    }

    public LoginPresenterBuilder WithUserId(string userName)
    {
        var viewMock = _mocker.GetMock<ILoginView>();
        viewMock.Setup(v => v.GetSessionValue(LoginPresenter.UserIdSessionKey))
            .Returns(userName);

        return this;
    }

    public LoginPresenterTestRecord Build()
    {
        var viewMock = _mocker.GetMock<ILoginView>();
        var presenter = new LoginPresenter(viewMock.Object, new LoginModel());
        
        return new LoginPresenterTestRecord(_mocker, presenter);
    }
}
