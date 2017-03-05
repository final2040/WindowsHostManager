using System;
using System.IO;
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
        private readonly Mock<IHostManager> _modelMock = new Mock<IHostManager>();
        private readonly Mock<IImportFileView> _viewMock = new Mock<IImportFileView>();

        [Test]
        public void Save_WhenConfigurationNameIsEmpty_ShouldThrowErrorMessage()
        {
            // arrange
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);

            _viewMock.Setup(vm => vm.ConfigName).Returns("").Verifiable();
            _viewMock.Setup(vm => vm.Path).Returns("Test");
            
            // act
            importPresenter.Import();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre de la configuración es requerido"), Times.Once);
        }

        [Test]
        public void Save_WhenConfigurationPathIsEmpty_ShouldThrowErrorMessage()
        {
            // arrange
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);

            _viewMock.Setup(vm => vm.ConfigName).Returns("Test").Verifiable();
            _viewMock.Setup(vm => vm.Path).Returns("").Verifiable();

            // act
            importPresenter.Import();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error, 
                    "Datos Inválidos", 
                    "La ruta del archivo es requerida"), Times.Once);
        }

        [Test]
        public void Save_WhenConfigurationNameAndPathIsEmpty_ShouldThrowBothErrorMessages()
        {
            // arrange
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);

            _viewMock.Setup(vm => vm.ConfigName).Returns("").Verifiable();
            _viewMock.Setup(vm => vm.Path).Returns("");

            // act
            importPresenter.Import();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error, 
                    "Datos Inválidos", 
                    "El nombre de la configuración es requerido\r\n" +
                    "La ruta del archivo es requerida"),
                Times.Once);
        }

        [Test]
        public void Save_WhenConfigurationContainsIlegalCharacteres_ShouldThrowErrorMessage()
        {
            // arrange
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);

            _viewMock.Setup(vm => vm.ConfigName).Returns("this\\is*a<bad>name").Verifiable();
            _viewMock.Setup(vm => vm.Path).Returns("test");

            // act
            importPresenter.Import();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Datos Inválidos",
                    "El nombre contiene caracteres inválidos, el nombre solo puede contener carácteres alfanumericos, guión medio y gión bajo"),
                Times.Once);
        }
        
        [Test]
        public void Save_WhenConfigurationIsCorrect_ShouldSaveConfiguration()
        {
            // arrange
            var filePath = "C:\\test.host";
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);
            var expectedConfiguration = new EConfiguration(0, "Test", "Test");

            _viewMock.Setup(vm => vm.ConfigName).Returns("Test");
            _viewMock.Setup(vm => vm.Path).Returns(filePath);
            _modelMock.Setup(mm => mm.ReadExternalConfig(filePath)).Returns(expectedConfiguration);

            // act
            importPresenter.Import();

            // assert
            _modelMock.Verify(
                mm => mm.AddConfig(
                    It.Is<EConfiguration>(c => c.Equals(expectedConfiguration))
                    )
                );
            _modelMock.Verify(
                mm => mm.ReadExternalConfig(filePath)
                );
        }

        [Test]
        public void Save_WhenUnexpectedExceptionIsThrown_ShouldDisplayMessage()
        {
            // arrange
            var filePath = "C:\\test.host";
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);

            _viewMock.Setup(vm => vm.ConfigName).Returns("Test");
            _viewMock.Setup(vm => vm.Path).Returns(filePath);
            _modelMock.Setup(mm => mm.ReadExternalConfig(filePath)).Throws(new Exception("Error desconocido"));

            // act
            importPresenter.Import();

            // assert
            _modelMock.Verify(
                mm => mm.ReadExternalConfig(filePath)
                );
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    "Error Inesperado", 
                    "Ocurrio un error inesperado: Error desconocido")
                );
        }

        [Test]
        public void Save_ShouldShowSuccessMessage()
        {
            // arrange
            var filePath = "C:\\test.host";
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);
            var expectedConfiguration = new EConfiguration(0, "Test", "Test");

            _viewMock.Setup(vm => vm.ConfigName).Returns("Test");
            _viewMock.Setup(vm => vm.Path).Returns(filePath);
            _modelMock.Setup(mm => mm.ReadExternalConfig(filePath)).Returns(expectedConfiguration);

            // act
            importPresenter.Import();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Info,
                    "Exito",
                    "Se ha importado la configuración satisfactoriamente"
                    ));
        }

        [Test]
        public void Save_WhenConfigurationExists_ShouldShowConfirmationMessage()
        {
            // arrange
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);
            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };

            _viewMock.Setup(vm => vm.ConfigName).Returns("test");
            _viewMock.Setup(vm => vm.Path).Returns("C:\\test.host");

            _modelMock.Setup(mm => mm.Exists(configToImport)).Returns(true);
            _modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.host")).Returns(configToImport);
            
            // Act
            importPresenter.Import();

            // assert
            _viewMock.Verify(
                mv =>
                    mv.ShowMessage(MessageType.YesNo, "Confirmación",
                        "Ya existe una configuración con ese nombre ¿Desea sobreescribirla?")
                );
        }


        [Test]
        public void Save_OnExistingConfigWhenUserSelectYes_ShouldOverwriteConfiguration()
        {
            // arrange
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);
            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };

            _viewMock.Setup(vm => vm.ConfigName).Returns("test");
            _viewMock.Setup(vm => vm.Path).Returns("C:\\test.host");
            _viewMock.Setup(mv =>
                mv.ShowMessage(MessageType.YesNo, "Confirmación",
                    "Ya existe una configuración con ese nombre ¿Desea sobreescribirla?"))
                .Returns(DialogResult.Yes);
            _modelMock.Setup(mm => mm.Exists(configToImport)).Returns(true);
            _modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.host")).Returns(configToImport);

            // Act
            importPresenter.Import();

            // assert
            _modelMock.Verify(
                mm => 
                mm.AddConfig(configToImport),
                Times.Once);
        }

        [Test]
        public void Save_OnExistingConfigWhenUserSelectNo_ShouldDoNothing()
        {
            // arrange
            var importPresenter = new PImport(_viewMock.Object, _modelMock.Object);
            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };

            _viewMock.Setup(vm => vm.ConfigName).Returns("test");
            _viewMock.Setup(vm => vm.Path).Returns("C:\\test.host");
            _viewMock.Setup(mv =>
                mv.ShowMessage(MessageType.YesNo, "Confirmación",
                    "Ya existe una configuración con ese nombre ¿Desea sobreescribirla?"))
                .Returns(DialogResult.No);
            _modelMock.Setup(mm => mm.Exists(configToImport)).Returns(true);
            _modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.host")).Returns(configToImport);

            // Act
            importPresenter.Import();

            // assert
            _modelMock.Verify(
                mm =>
                mm.AddConfig(configToImport),
                Times.Never);
        }

    }
}