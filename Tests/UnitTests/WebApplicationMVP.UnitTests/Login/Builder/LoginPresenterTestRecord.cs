using Moq.AutoMock;
using WebApplicationMVP.Login.Presenter;

namespace WebApplicationMVP.UnitTests.Login.Builder;

public record LoginPresenterTestRecord(
    AutoMocker Mocker,
    ILoginPresenter ArchiveDocumentPresenter
);
