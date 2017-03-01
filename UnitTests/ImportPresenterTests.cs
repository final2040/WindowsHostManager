using System.Windows.Forms;
using Entities;
using Moq;
using NUnit.Framework;
using Presenter;

namespace UnitTests
{
    [TestFixture]
    public class ImportPresenterTests
    {
        [Test]
        public void Submit_WhenConfigurationNameIsEmpty_ShouldThrowErrorMessage()
        {
            // arrange
            var viewMock = new Mock<IImportFileView>();
            var importPresenter = new PImport(viewMock.Object);

            viewMock.Setup(vm => vm.ConfigName).Returns("").Verifiable();
            viewMock.Setup(vm => vm.Path).Returns("Test");
            
            // act
            importPresenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre de la configuración es requerido\r\n"), Times.Once);
        }

        [Test]
        public void Submit_WhenConfigurationPathIsEmpty_ShouldThrowErrorMessage()
        {
            // arrange
            var viewMock = new Mock<IImportFileView>();
            var importPresenter = new PImport(viewMock.Object);

            viewMock.Setup(vm => vm.ConfigName).Returns("Test").Verifiable();
            viewMock.Setup(vm => vm.Path).Returns("");

            // act
            importPresenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error, 
                    "Datos Inválidos", 
                    "La ruta del archivo es requerida\r\n"), Times.Once);
        }

        [Test]
        public void Submit_WhenConfigurationNameAndPathIsEmpty_ShouldThrowBothErrorMessages()
        {
            // arrange
            var viewMock = new Mock<IImportFileView>();
            var importPresenter = new PImport(viewMock.Object);

            viewMock.Setup(vm => vm.ConfigName).Returns("").Verifiable();
            viewMock.Setup(vm => vm.Path).Returns("");

            // act
            importPresenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error, 
                    "Datos Inválidos", 
                    "El nombre de la configuración es requerido\r\n" +
                    "La ruta del archivo es requerida\r\n"),
                Times.Once);
        }

        [Test]
        public void Submit_WhenConfigurationContainsIlegalCharacteres_ShouldThrowErrorMessage()
        {
            // arrange
            var viewMock = new Mock<IImportFileView>();
            var importPresenter = new PImport(viewMock.Object);

            viewMock.Setup(vm => vm.ConfigName).Returns("this\\is*a<bad>name").Verifiable();
            viewMock.Setup(vm => vm.Path).Returns("test");

            // act
            importPresenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre contiene caracteres inválidos, el nombre solo puede contener carácteres alfanumericos, guión medio y gión bajo\r\n"),
                Times.Once);
        }

        [Test]
        public void Submit_WhenConfigurationIsCorrect_ShouldCloseViewAndSetDialogResultToOk()
        {
            // arrange
            var viewMock = new Mock<IImportFileView>();
            var importPresenter = new PImport(viewMock.Object);

            viewMock.Setup(vm => vm.ConfigName).Returns("okName").Verifiable();
            viewMock.Setup(vm => vm.Path).Returns("test");
            
            // act
            importPresenter.Submit();

            // assert
            viewMock.VerifySet(vm => vm.DialogResult = DialogResult.OK);
            viewMock.Verify(vm => vm.Close());
        }
    }
}