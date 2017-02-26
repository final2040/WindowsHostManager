using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Entities;
using Model;
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
            var mainPresenter = new PMain(mainViewMock.Object, null, modelMock.Object);
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
            var mainPresenter = new PMain(mainViewMock.Object,null, modelMock.Object);
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
            var mainPresenter = new PMain(mainViewMock.Object, null, modelMock.Object);
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
            var mainPresenter = new PMain(mainViewMock.Object,null, modelMock.Object);
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
            var mainPresenter = new PMain(mainViewMock.Object, null, modelMock.Object);
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
            var mainPresenter = new PMain(mainViewMock.Object,null, modelMock.Object);
            mainViewMock.Setup(
                mv => mv.ShowMessage(MessageType.Error, "Valor inválido", "La configuración seleccionada es inválida")).Verifiable();
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
            var mainPresenter = new PMain(mainViewMock.Object,null, modelMock.Object);
            mainViewMock.Setup(
                mv => mv.ShowMessage(MessageType.Error, "Valor inválido", "La configuración seleccionada es inválida")).Verifiable();
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
            var mainPresenter = new PMain(mainViewMock.Object,null, modelMock.Object);
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
        public void ImportConfiguration_ShouldImportConfiguration()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var importViewDialog = new Mock<IImportFileView>();

            EConfiguration configToImport = new EConfiguration() {Id = 0,Name = "Test", Content = "test" };
            var mainPresenter = new PMain(mainViewMock.Object, importViewDialog.Object, modelMock.Object);

            modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.txt")).Returns(configToImport).Verifiable();
            modelMock.Setup(mm => mm.AddConfig(It.Is<EConfiguration>(c => c.Equals(configToImport)))).Verifiable();

            importViewDialog.Setup(iv => iv.Path).Returns("C:\\test.txt").Verifiable();
            importViewDialog.Setup(iv => iv.ConfigName).Returns("Test").Verifiable();
            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            // Act
            mainPresenter.ImportConfig();

            // assert
            mainViewMock.Verify();
            modelMock.Verify();
            importViewDialog.Verify();
        }

        [Test]
        public void ImportConfiguration_WhenConfigurationExists_ShouldShowConfirmationMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var importViewDialog = new Mock<IImportFileView>();

            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };
            var mainPresenter = new PMain(mainViewMock.Object,importViewDialog.Object, modelMock.Object);
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.YesNo, "Confirmación",
                        "Ya existe una configuración con ese nombre ¿Desea sobreescribirla?"))
                        .Verifiable();
            modelMock.Setup(mm => mm.Exists(configToImport)).Returns(true).Verifiable();
            modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.txt")).Returns(configToImport).Verifiable();

            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            importViewDialog.Setup(iv => iv.Path).Returns("C:\\test.txt").Verifiable();
            importViewDialog.Setup(iv => iv.ConfigName).Returns("Test").Verifiable();
            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            
            // Act
            mainPresenter.ImportConfig();

            // assert
            mainViewMock.Verify();
        }

        [Test]
        public void ImportConfiguration_OnExistingConfigWhenUserSelectYes_ShouldOverwriteConfiguration()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var importViewDialog = new Mock<IImportFileView>();

            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };
            var mainPresenter = new PMain(mainViewMock.Object, importViewDialog.Object, modelMock.Object);

            modelMock.Setup(mm => mm.Exists(configToImport)).Returns(true);
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.YesNo, "Confirmación",
                        "Ya existe una configuración con ese nombre ¿Desea sobreescribirla?"))
                        .Returns(DialogResult.Yes)
                        .Verifiable();
            modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.txt")).Returns(configToImport).Verifiable();

            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            importViewDialog.Setup(iv => iv.Path).Returns("C:\\test.txt").Verifiable();
            importViewDialog.Setup(iv => iv.ConfigName).Returns("Test").Verifiable();
            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            // Act
            mainPresenter.ImportConfig();

            // assert
            mainViewMock.Verify();
            modelMock.Verify(mm => mm.AddConfig(configToImport), Times.Exactly(1));
        }

        [Test]
        public void ImportConfiguration_OnExistingConfigWhenUserSelectNo_ShouldDoNothing()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var importViewDialog = new Mock<IImportFileView>();
            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };
            var mainPresenter = new PMain(mainViewMock.Object, importViewDialog.Object, modelMock.Object);

            modelMock.Setup(mm => mm.Exists(configToImport)).Returns(true);
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.YesNo, "Confirmación",
                        "Ya existe una configuración con ese nombre ¿Desea sobreescribirla?"))
                        .Returns(DialogResult.No)
                        .Verifiable();

            modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.txt")).Returns(configToImport).Verifiable();

            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            importViewDialog.Setup(iv => iv.Path).Returns("C:\\test.txt").Verifiable();
            importViewDialog.Setup(iv => iv.ConfigName).Returns("Test").Verifiable();
            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();

            // Act
            mainPresenter.ImportConfig();

            // assert
            mainViewMock.Verify();
            modelMock.Verify(mm => mm.AddConfig(configToImport), Times.Never);
        }

        [Test]
        public void ImportConfiguration_ShouldShowImportDialog()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var importViewDialog = new Mock<IImportFileView>();
            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };

            var mainPresenter = new PMain(mainViewMock.Object, importViewDialog.Object, modelMock.Object);

            modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.txt")).Returns(configToImport).Verifiable();

            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            importViewDialog.Setup(iv => iv.Path).Returns("C:\\test.txt").Verifiable();
            importViewDialog.Setup(iv => iv.ConfigName).Returns("Test").Verifiable();
            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            // Act
            mainPresenter.ImportConfig();

            // assert
           importViewDialog.Verify();
        }

        [Test]
        public void ImportConfiguration_WhenUserAcceptDialog_ShouldSaveConfig()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var importViewDialog = new Mock<IImportFileView>();
            EConfiguration configToImport = new EConfiguration() { Name = "Test", Content = "test" };

            var mainPresenter = new PMain(mainViewMock.Object, importViewDialog.Object, modelMock.Object);
            modelMock.Setup(mm => mm.ReadExternalConfig("C:\\test.txt")).Returns(configToImport).Verifiable();
            importViewDialog.Setup(iv => iv.Path).Returns("C:\\test.txt").Verifiable();
            importViewDialog.Setup(iv => iv.ConfigName).Returns("test").Verifiable();
            importViewDialog.Setup(iv => iv.ShowDialog()).Returns(DialogResult.OK).Verifiable();
            // Act
            mainPresenter.ImportConfig();

            // assert
            importViewDialog.Verify();
        }
    }
}