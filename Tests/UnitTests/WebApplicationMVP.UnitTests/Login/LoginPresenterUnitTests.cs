using FluentAssertions;
using Moq;
using WebApplicationMVP.Login.Presenter;
using WebApplicationMVP.Login.View;
using WebApplicationMVP.UnitTests.Login.Builder;

namespace WebApplicationMVP.UnitTests.Login;

public class LoginPresenterUnitTests
{
    public class ValidateUnitTests
    {
        [Fact]
        public void GIVEN_AdminUserNameAndPassword_WHEN_Validate_THEN_ReturnTrue()
        {
            // Arrange
            var (mocker, presenter) = new LoginPresenterBuilder()
                .Build();

            // Act
            var result = presenter.Validate("admin", "password");

            // Assert
            var viewMock = mocker.GetMock<ILoginView>();
            result.Should().BeTrue();
            viewMock.Verify(v => v.ShowMessage(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GIVEN_NonAdminUserNameAndPassword_WHEN_Validate_THEN_ReturnFalseANdDisplayMessage()
        {
            // Arrange
            var (mocker, presenter) = new LoginPresenterBuilder()
                .Build();

            // Act
            var result = presenter.Validate("some user name", "some password");

            // Assert
            var viewMock = mocker.GetMock<ILoginView>();
            result.Should().BeFalse();
            viewMock.Verify(v => v.ShowMessage("Invalid username or password."), Times.Once);
        }

        [Fact]
        public void GIVEN_NoUserNameAndPassword_WHEN_Validate_THEN_ReturnFalseANdDisplayMessage()
        {
            // Arrange
            var (mocker, presenter) = new LoginPresenterBuilder()
                .Build();

            // Act
            var result = presenter.Validate(string.Empty, string.Empty);

            // Assert
            var viewMock = mocker.GetMock<ILoginView>();
            result.Should().BeFalse();
            viewMock.Verify(v => v.ShowMessage("Please enter username and password."), Times.Once);
        }
    }

    public class LoginUnitTests
    {
        [Fact]
        public void GIVEN_AdminUserNameAndPassword_WHEN_Login_THEN_RedirectedToWelcomePage()
        {
            // Arrange
            var (mocker, presenter) = new LoginPresenterBuilder()
                .WithUserName("admin")
                .WithPassword("password")
                .Build();

            // Act
            presenter.Login();

            // Assert
            var viewMock = mocker.GetMock<ILoginView>();
            viewMock.Verify(v => v.Redirect("../../Welcome/View/WelcomeView.aspx"), Times.Once);
            viewMock.Verify(h => h.SetSessionValue("UserId", "admin"), Times.Once);

            presenter.Model.Token.Should().NotBeEmpty();
        }

        [Fact]
        public void GIVEN_NonAdminUserNameAndPassword_WHEN_Login_THEN_NoRedirect()
        {
            // Arrange
            var (mocker, presenter) = new LoginPresenterBuilder()
                .WithUserName(string.Empty)
                .WithPassword(string.Empty)
                .Build();

            // Act
            presenter.Login();

            // Assert
            var viewMock = mocker.GetMock<ILoginView>();
            viewMock.Verify(v => v.Redirect(It.IsAny<string>()), Times.Never);
            viewMock.Verify(h => h.SetSessionValue("UserId", It.IsAny<string>()), Times.Never);

            presenter.Model.Token.Should().BeNull();
        }
    }
    public class UserLoggedInUnitTests
    {
        [Fact]
        public void GIVEN_LoggedInUser_WHEN_UserLoggedIn_THEN_RedirectedToWelcomePage()
        {
            // Arrange
            var (mocker, presenter) = new LoginPresenterBuilder()
                .WithUserId("some user name")
                .Build();

            // Act
            presenter.UserLoggedIn();

            // Assert
            var viewMock = mocker.GetMock<ILoginView>();
            viewMock.Verify(h => h.GetSessionValue("UserId"), Times.Once);
            viewMock.Verify(v => v.Redirect(LoginPresenter.WelcomeViewLocation), Times.Once);
        }

        [Fact]
        public void GIVEN_NoLoggedInUser_WHEN_UserLoggedIn_THEN_DoNotRedirectedToWelcomePage()
        {
            // Arrange
            var (mocker, presenter) = new LoginPresenterBuilder()
                .Build();

            // Act
            presenter.UserLoggedIn();

            // Assert
            var viewMock = mocker.GetMock<ILoginView>();
            viewMock.Verify(h => h.GetSessionValue("UserId"), Times.Once);
            viewMock.Verify(v => v.Redirect(LoginPresenter.WelcomeViewLocation), Times.Never);
        }
    }
}
