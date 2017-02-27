using System.Security.Policy;
using System.Windows.Forms;
using Moq;
using NUnit.Framework;
using Entities;
using Presenter;

namespace UnitTests
{
    [TestFixture]
    public class EditPresenterTests
    {
        [Test]
        public void Submit_WhenNameIsEmpty_ShoulDisplayErrorMessage()
        {
            // arrange
            var viewMock = new Mock<IEditView>();
            PEdit presenter = new PEdit(viewMock.Object);
            var editedConfiguration = new EConfiguration(0,"", "Test");
            viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre de la configuración es requerido\r\n"), 
                Times.Once);
        }

        [Test]
        public void Submit_WhenContentIsEmpty_ShoulDisplayErrorMessage()
        {
            // arrange
            var viewMock = new Mock<IEditView>();
            PEdit presenter = new PEdit(viewMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "");
            viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El contenido de la configuración no puede estar vacio\r\n"),
                Times.Once);
        }
        
        [Test]
        public void Submit_WhenNameContainsIlegalCharacters_ShoulDisplayErrorMessage()
        {
            // arrange
            var viewMock = new Mock<IEditView>();
            PEdit presenter = new PEdit(viewMock.Object);
            var editedConfiguration = new EConfiguration(0, ">?te<st", "test");
            viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre contiene caracteres inválidos, el nombre solo puede contener carácteres alfanumericos, guión medio y gión bajo\r\n"),
                Times.Once);
        }

        [Test]
        public void Submit_WhenContentIsEmptyAndNameIsEmpty_ShoulDisplayCombinedErrorMessage()
        {
            // arrange
            var viewMock = new Mock<IEditView>();
            PEdit presenter = new PEdit(viewMock.Object);
            var editedConfiguration = new EConfiguration(0, "", "");
            viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Submit();

            // assert
            viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre de la configuración es requerido\r\n" +
                    "El contenido de la configuración no puede estar vacio\r\n"),
                Times.Once);
        }

        [Test]
        public void Submit_WhenConfigurationIsCorrect_ShouldCloseViewAndSetDialogResultToOk()
        {
            // arrange
            var viewMock = new Mock<IEditView>();
            var importPresenter = new PEdit(viewMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();


            // act
            importPresenter.Submit();

            // assert
            viewMock.VerifySet(vm => vm.DialogResult = DialogResult.OK);
            viewMock.Verify(vm => vm.Close());
        }

        // TODO: crear mostrar alerta al usuario cuando se cancela
        // TODO: crear mecanismo que detecte si ha habido cambios
        // TODO: si no ha habido cambios no guardar archivo

    }
}