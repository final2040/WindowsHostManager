using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Entities;
using Moq;
using NUnit.Framework;
using Presenter;

namespace UnitTests
{
    [TestFixture()]
    public class PresenterTests
    {
        [Test]
        public void UpdateView_ShouldRetrieveAndPopulateConfiguationList()
        {
            // arrange
            var configurations = new List<EConfiguration>()
            {
                new EConfiguration() {Name = "file", Content = "TestContent"},
                new EConfiguration() {Name = "file2", Content = "TestContent"},
                new EConfiguration() {Name = "file3", Content = "TestContent"}
            };
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var mainPresenter = new PMain(mainViewMock.Object, null, null, modelMock.Object);
            modelMock.Setup(mm => mm.GetAll()).Returns(configurations).Verifiable();
            mainViewMock.SetupSet(mv => mv.Configurations = configurations).Verifiable();

            // act
            mainPresenter.UpdateView();

            // assert
            mainViewMock.Verify();
            modelMock.Verify();
        }

        [Test]
        public void UpdateView_WhenThereIsNotConfigToLoad_ShouldThrowErrorMessage()
        {
            // arrange
            var configurations = new List<EConfiguration>();
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var mainPresenter = new PMain(mainViewMock.Object,null,null, modelMock.Object);
            modelMock.Setup(mm => mm.GetAll()).Returns(configurations).Verifiable();
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.Error, "Sin Resultados",
                        "No hay configuraciones para cargar, por favor cree o importe configuraciones de Arhivo Host")).Verifiable();

            // act
            mainPresenter.UpdateView();

            // assert
            mainViewMock.Verify();
            modelMock.Verify();
        }

        [Test]
        public void UpdateView_WhenExceptionIsThrown_ShouldThrowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var mainPresenter = new PMain(mainViewMock.Object, null,null, modelMock.Object);
            modelMock.Setup(mm => mm.GetAll()).Throws(new IOException("Acceso Denegado")).Verifiable();
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.Error, "Error Inesperado",
                        "Ocurrio un error inesperado: " + "Acceso Denegado")).Verifiable();

            // act
            mainPresenter.UpdateView();

            // assert
            mainViewMock.Verify();
            modelMock.Verify();
        }

        [Test]
        public void SetConfig_ShouldLoadConfigurationFile()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var configToLoad = new EConfiguration()
            {
                Name = "Test",
                Content = "#Example\r\n127.0.0.1\tlocalhost"
            };
            var mainPresenter = new PMain(mainViewMock.Object,null,null, modelMock.Object);
            modelMock.Setup(mm => mm.LoadConfig(configToLoad)).Verifiable();

            // Act
            mainPresenter.SetConfig(configToLoad);

            // assert
            modelMock.Verify();
        }

        [Test]
        public void SetConfig_ShouldDisplaySuccessMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var configToLoad = new EConfiguration()
            {
                Name = "Test",
                Content = "#Example\r\n127.0.0.1\tlocalhost"
            };
            var mainPresenter = new PMain(mainViewMock.Object, null,null, modelMock.Object);
            modelMock.Setup(mm => mm.LoadConfig(configToLoad)).Verifiable();
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.Info, "Exito",
                        "Se ha cargado la configuración Test en el sistema operativo")).Verifiable();
            // Act
            mainPresenter.SetConfig(configToLoad);

            // assert
            modelMock.Verify();
            mainViewMock.Verify();
        }

        [Test]
        public void SetConfig_WhenConfigurationIsNull_ShouldThrowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            EConfiguration configToLoad = null;
            var mainPresenter = new PMain(mainViewMock.Object,null,null, modelMock.Object);
            mainViewMock.Setup(
                mv => mv.ShowMessage(MessageType.Error, "Valor inválido", "Debe de seleccionar una configuración")).Verifiable();
            // Act
            mainPresenter.SetConfig(configToLoad);

            // assert
            mainViewMock.Verify();
        }

        [Test]
        public void SetConfig_WhenConfigurationIsInvalid_ShouldThrowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            EConfiguration configToLoad = new EConfiguration();
            var mainPresenter = new PMain(mainViewMock.Object,null,null, modelMock.Object);
            mainViewMock.Setup(
                mv => mv.ShowMessage(MessageType.Error, "Valor inválido", "Debe de seleccionar una configuración")).Verifiable();
            // Act
            mainPresenter.SetConfig(configToLoad);

            // assert
            mainViewMock.Verify();
        }

        [Test]
        public void SetConfig_WhenUnexpectedExceptionIsTrhown_ShouldThrowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            EConfiguration configToLoad = new EConfiguration() {Name = "Test", Content="test"};
            var mainPresenter = new PMain(mainViewMock.Object,null,null, modelMock.Object);
            mainViewMock.Setup(
                mv => mv.ShowMessage(MessageType.Error, "Error Inesperado", "Ocurrio un error inesperado: La configuración no puede ser nula\r\nNombre del parámetro: configuration")).Verifiable();
            modelMock.Setup(mm => mm.LoadConfig(configToLoad))
                .Throws(new ArgumentNullException("configuration","La configuración no puede ser nula"));
            // Act
            mainPresenter.SetConfig(configToLoad);

            // assert
            mainViewMock.Verify();
        }

        [Test]
        public void ImportConfiguration_ShouldShowImportDialog()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var importViewDialog = new Mock<IImportFileView>();
            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };

            var mainPresenter = new PMain(mainViewMock.Object, importViewDialog.Object,null, modelMock.Object);

            modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.txt")).Returns(configToImport).Verifiable();

            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.Cancel).Verifiable();
            
            // Act
            mainPresenter.ImportConfig();

            // assert
           importViewDialog.Verify();
        }

        [Test]
        public void DeleteConfiguration_ShouldShowConfirmationDialog()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            
            EConfiguration configToDelete = new EConfiguration() { Name = "Test", Content = "test" };
            var mainPresenter = new PMain(mainViewMock.Object, null, null, modelMock.Object);
            
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.YesNo,
                    "Advertencia",
                    "Está a punto de eliminar un archivo de configuración, esta acción no se puede deshacer. ¿Desea continuar?")).Verifiable();

            // act
            mainPresenter.Delete(configToDelete);

            // assert
            mainViewMock.Verify();
        }

        [Test]
        public void DeleteConfiguration_WhenUserSelectYes_ShouldDeleteConfiguration()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();

            EConfiguration configToDelete = new EConfiguration() { Name = "Test", Content = "test" };
            var mainPresenter = new PMain(mainViewMock.Object, null, null, modelMock.Object);

            modelMock.Setup(mm => mm.DeleteConfig(configToDelete)).Verifiable();

            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.YesNo,
                    "Advertencia",
                    "Está a punto de eliminar un archivo de configuración, esta acción no se puede deshacer. ¿Desea continuar?"))
                    .Returns(DialogResult.Yes)
                    .Verifiable();

            // act
            mainPresenter.Delete(configToDelete);

            // assert
            modelMock.Verify();
            mainViewMock.Verify();
        }

        [Test]
        public void DeleteConfiguration_WhenUserSelectNo_ShouldDoNothing()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();

            EConfiguration configToDelete = new EConfiguration() { Name = "Test", Content = "test" };
            var mainPresenter = new PMain(mainViewMock.Object, null, null, modelMock.Object);
            
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.YesNo,
                    "Advertencia",
                    "Está a punto de eliminar un archivo de configuración, esta acción no se puede deshacer. ¿Desea continuar?"))
                    .Returns(DialogResult.No)
                    .Verifiable();

            // act
            mainPresenter.Delete(configToDelete);

            // assert
            modelMock.Verify(mm => mm.DeleteConfig(configToDelete),Times.Never);
            mainViewMock.Verify();
        }

        [Test]
        public void DeleteConfiguration_WhenNullConfiguration_ShouldShowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            
            var mainPresenter = new PMain(mainViewMock.Object, null, null, modelMock.Object);

            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    "Valor inválido",
                    "Debe de seleccionar una configuración")).Verifiable();

            // act
            mainPresenter.Delete(null);

            // assert
            mainViewMock.Verify();
        }
        //TODO: Crear prueba para error inesperado para delete configuration
        [Test]
        public void EditConfig_ShouldDisplayEditDialog()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var editViewMock = new Mock<IEditView>();
            var configurationToEdit = new EConfiguration(0,"Test","Test");
            var mainPresenter = new PMain(mainViewMock.Object, null, editViewMock.Object ,modelMock.Object);
            
            //act
            mainPresenter.Edit(configurationToEdit);

            //assert
            editViewMock.Verify(ev => ev.ShowDialog(),Times.Once);
            editViewMock.VerifySet(ev => ev.Configuration = configurationToEdit, Times.Once);
            editViewMock.VerifySet(ev => ev.EditMode = EditMode.Edit, Times.Once);
        }

        [Test]
        public void EditConfig_WhenConfigurationIsNull_ShouldDisplayErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var editViewMock = new Mock<IEditView>();
            
            var mainPresenter = new PMain(mainViewMock.Object, null, editViewMock.Object, modelMock.Object);

            //act
            mainPresenter.Edit(null);

            //assert
            mainViewMock.Verify(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    "Valor inválido",
                    "Debe de seleccionar una configuración"), 
                Times.Once());
        }

        [Test]
        public void EditConfig_WhenUserHitCancel_ShouldDoNothing()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var editViewMock = new Mock<IEditView>();
            var configToEdit = new EConfiguration(0,"test","test");
            var mainPresenter = new PMain(mainViewMock.Object, null, editViewMock.Object, modelMock.Object);

            editViewMock.Setup(ev => ev.ShowDialog()).Returns(DialogResult.Cancel).Verifiable();
            
            //act
            mainPresenter.Edit(configToEdit);

            //assert
            editViewMock.Verify();
            modelMock.Verify(mm => mm.AddConfig(configToEdit), Times.Never);
        }

        [Test]
        public void EditConfig_WhenUserHitOk_ShouldSaveConfig()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var editViewMock = new Mock<IEditView>();
            var configToEdit = new EConfiguration(0, "test", "test");
            var mainPresenter = new PMain(mainViewMock.Object, null, editViewMock.Object, modelMock.Object);

            editViewMock.Setup(ev => ev.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            editViewMock.Setup(ev => ev.Configuration).Returns(configToEdit).Verifiable();
            //act
            mainPresenter.Edit(configToEdit);

            //assert
            editViewMock.Verify(ev => ev.ShowDialog(), Times.Once);
            editViewMock.VerifySet(ev => ev.Configuration = configToEdit, Times.Once);
            editViewMock.VerifySet(ev => ev.EditMode = EditMode.Edit, Times.Once);
            modelMock.Verify(mm => mm.AddConfig(configToEdit), Times.Once);

        }

        [Test]
        public void EditConfig_WhenUnexpectedExceptionIsThrow_ShouldDisplayError()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var editViewMock = new Mock<IEditView>();
            var configToEdit = new EConfiguration(0, "test", "test");
            var mainPresenter = new PMain(mainViewMock.Object, null, editViewMock.Object, modelMock.Object);

            editViewMock.Setup(ev => ev.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            editViewMock.Setup(ev => ev.Configuration).Returns(configToEdit).Verifiable();
            modelMock.Setup(mm => mm.AddConfig(configToEdit)).Throws(new Exception("Error desconocido"));
            //act
            mainPresenter.Edit(configToEdit);

            //assert
            editViewMock.Verify(ev => ev.ShowDialog(), Times.Once);
            editViewMock.VerifySet(ev => ev.Configuration = configToEdit, Times.Once);
            editViewMock.VerifySet(ev => ev.EditMode = EditMode.Edit, Times.Once);
            mainViewMock.Verify(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    "Error Inesperado",
                    "Ocurrio un error inesperado: Error desconocido"));
            modelMock.Verify(mm => mm.AddConfig(configToEdit), Times.Once);
        }

    }
}