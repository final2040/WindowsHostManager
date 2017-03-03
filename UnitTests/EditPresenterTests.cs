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
        private readonly Mock<IEditView> _viewMock = new Mock<IEditView>();
        private readonly Mock<IHostManager> _modelMock = new Mock<IHostManager>();

        [Test]
        public void Save_WhenNameIsEmpty_ShoulDisplayErrorMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0,"", "Test");
            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre de la configuración es requerido\r\n"), 
                Times.Once);
        }

        [Test]
        public void Save_WhenContentIsEmpty_ShoulDisplayErrorMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "");
            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El contenido de la configuración no puede estar vacio\r\n"),
                Times.Once);
        }
        
        [Test]
        public void Save_WhenNameContainsIlegalCharacters_ShoulDisplayErrorMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, ">?te<st", "test");
            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre contiene caracteres inválidos, el nombre solo puede contener carácteres alfanumericos, guión medio y gión bajo\r\n"),
                Times.Once);
        }

        [Test]
        public void Save_WhenContentIsEmptyAndNameIsEmpty_ShoulDisplayCombinedErrorMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "", "");
            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre de la configuración es requerido\r\n" +
                    "El contenido de la configuración no puede estar vacio\r\n"),
                Times.Once);
        }

        [Test]
        public void Save_WhenConfigurationIsCorrect_ShouldCloseViewAndSetDialogResultToOk()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();


            // act
            presenter.Save();

            // assert
            _viewMock.VerifySet(vm => vm.DialogResult = DialogResult.OK);
            _viewMock.Verify(vm => vm.Close());
        }

        [Test]
        public void EditConfig_WhenUserHitOk_ShouldSaveConfig()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            

            // act
            presenter.Save();

            // assert
            _viewMock.VerifySet(vm => vm.DialogResult = DialogResult.OK);
            _viewMock.Verify(vm => vm.Close());
            _modelMock.Verify(
                mm => mm.AddConfig(editedConfiguration),
                Times.Once
                );
        }

        // TODO: crear mostrar alerta al usuario cuando se cancela
        // TODO: crear mecanismo que detecte si ha habido cambios
        // TODO: si no ha habido cambios no guardar archivo
        // TODO: implementar sintaxis highligth

    }
}