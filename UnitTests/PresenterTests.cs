using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AppResources;
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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            
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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            modelMock.Setup(mm => mm.GetAll()).Returns(configurations).Verifiable();
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.Error, Language.NoConfigurationFoundError_Tittle,
                        Language.NoConfigurationFoundError_Text)).Verifiable();

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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            modelMock.Setup(mm => mm.GetAll()).Throws(new IOException("Acceso Denegado")).Verifiable();
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.Error, Language.UnexpectedError_Tittle,
                        string.Format(Language.UnexpectedError_Text, "Acceso Denegado"))).Verifiable();

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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            modelMock.Setup(mm => mm.LoadConfig(configToLoad)).Verifiable();
            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToLoad);
            // Act
            mainPresenter.SetConfig();

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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            modelMock.Setup(mm => mm.LoadConfig(configToLoad)).Verifiable();
            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToLoad);
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.Info, Language.Success_Tittle,
                       string.Format(Language.SuccessSetConfiguration_Text, configToLoad.Name)))
                       .Verifiable();
            // Act
            mainPresenter.SetConfig();

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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToLoad);
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    Language.InvalidConfigurationError_Tittle,
                    Language.InvalidConfigurationError_Text)).Verifiable();
            // Act
            mainPresenter.SetConfig();

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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToLoad);
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.Error, Language.InvalidConfigurationError_Tittle,
                    Language.InvalidConfigurationError_Text)).Verifiable();
            // Act
            mainPresenter.SetConfig();

            // assert
            mainViewMock.Verify();
        }

        [Test]
        public void SetConfig_WhenUnexpectedExceptionIsTrhown_ShouldThrowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var exceptionToThrow = new ArgumentNullException("configuration", "La configuración no puede ser nula");
            var configToLoad = new EConfiguration() { Name = "Test", Content = "test" };
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);

            modelMock.Setup(mm => mm.LoadConfig(configToLoad))
               .Throws(exceptionToThrow);
            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToLoad);
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    Language.UnexpectedError_Tittle,
                    string.Format(Language.UnexpectedError_Text, exceptionToThrow.Message )))
                    .Verifiable();
           
            // Act
            mainPresenter.SetConfig();

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

            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            factoryMock.Setup(fm => fm.Create("ImportView")).Returns(importViewDialog.Object).Verifiable();
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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);

            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToDelete);
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.YesNo,
                    Language.Warning_Tittle,
                    Language.DeleteConfirmation_Text))
                    .Verifiable();

            // act
            mainPresenter.Delete();

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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);

            modelMock.Setup(mm => mm.DeleteConfig(configToDelete)).Verifiable();
            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToDelete);

            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.YesNo,
                    Language.Warning_Tittle,
                    Language.DeleteConfirmation_Text))
                    .Returns(DialogResult.Yes)
                    .Verifiable();

            // act
            mainPresenter.Delete();

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
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);

            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configToDelete);
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.YesNo,
                    Language.Warning_Tittle,
                    Language.DeleteConfirmation_Text))
                    .Returns(DialogResult.No)
                    .Verifiable();

            // act
            mainPresenter.Delete();

            // assert
            modelMock.Verify(mm => mm.DeleteConfig(configToDelete), Times.Never);
            mainViewMock.Verify();
        }

        [Test]
        public void DeleteConfiguration_WhenNullConfiguration_ShouldShowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();

            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            
            mainViewMock.Setup(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    Language.InvalidConfigurationError_Tittle,
                    Language.InvalidConfigurationError_Text)).Verifiable();

            // act
            mainPresenter.Delete();

            // assert
            mainViewMock.Verify();
        }

        [Test]
        public void DeleteConfiguration_WhenUnexpectedExceptionIsThrown_ShouldShowErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var exceptionToThrow = new Exception("Error Desconocido");

            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            modelMock.Setup(mm => mm.DeleteConfig(It.IsAny<EConfiguration>())).Throws(exceptionToThrow);

            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(new EConfiguration(0,"nombre","contenido"));
            mainViewMock.Setup(mv => mv.ShowMessage(
                MessageType.YesNo,
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(DialogResult.Yes);
            // act
            mainPresenter.Delete();

            // assert
            mainViewMock.Verify(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    Language.UnexpectedError_Tittle,
                    string.Format(Language.UnexpectedError_Text, exceptionToThrow.Message)),
                Times.Once);

        }

        [Test]
        public void EditConfig_ShouldDisplayEditDialog()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var editViewMock = new Mock<IEditView>();
            var configurationToEdit = new EConfiguration(0, "Test", "Test");
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);

            mainViewMock.Setup(mv => mv.SelectedConfiguration).Returns(configurationToEdit);
            factoryMock.Setup(fm => fm.Create("EditView")).Returns(editViewMock.Object).Verifiable();
            
            //act
            mainPresenter.Edit();

            //assert
            editViewMock.Verify(ev => ev.ShowDialog(), Times.Once);
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

            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            
            factoryMock.Setup(fm => fm.Create("EditView")).Returns(editViewMock.Object).Verifiable();
            //act
            mainPresenter.Edit();

            //assert
            mainViewMock.Verify(
                mv => mv.ShowMessage(
                    MessageType.Error,
                    Language.InvalidConfigurationError_Tittle,
                    Language.InvalidConfigurationError_Text),
                Times.Once());}

        [Test]
        public void NewConfig_ShouldDisplayEditDialog()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var editViewMock = new Mock<IEditView>();
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            factoryMock.Setup(fm => fm.Create("EditView")).Returns(editViewMock.Object).Verifiable();

            //act
            mainPresenter.New();

            //assert
            editViewMock.Verify(ev => ev.ShowDialog(), Times.Once);
            editViewMock.VerifySet(ev => ev.EditMode = EditMode.New, Times.Once);
        }

        [Test]
        public void NewConfig_WhenResultIsTrue_ShouldRefreshView()
        {
            var configurations = new List<EConfiguration>()
            {
                new EConfiguration() {Name = "file", Content = "TestContent"},
                new EConfiguration() {Name = "file2", Content = "TestContent"},
                new EConfiguration() {Name = "file3", Content = "TestContent"}
            };
            var mainViewMock = new Mock<IMainView>();
            var editViewMock = new Mock<IEditView>();
            var modelMock = new Mock<IHostManager>();
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            factoryMock.Setup(fm => fm.Create("EditView")).Returns(editViewMock.Object).Verifiable();
            modelMock.Setup(mm => mm.GetAll()).Returns(configurations).Verifiable();
            mainViewMock.SetupSet(mv => mv.Configurations = configurations).Verifiable();
            editViewMock.Setup(ev => ev.ShowDialog()).Returns(DialogResult.OK);
            // act
            mainPresenter.New();

            // assert
            mainViewMock.Verify();
            modelMock.Verify();
        }

        [Test]
        public void BackupConfig_ShouldBackupCurrentSystemConfig()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            var configurationMock = new Mock<EConfiguration>();
            var systemConfigPath = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts");
            modelMock.Setup(mm => mm.ReadExternalConfig(systemConfigPath)).Returns(configurationMock.Object).Verifiable();
            modelMock.Setup(mm => mm.HostsFilePath).Returns(systemConfigPath);
            configurationMock.Setup(cm => cm.Name).Returns("test");
            configurationMock.Setup(cm => cm.Content).Returns("test");
            configurationMock.Setup(cm => cm.Id).Returns(0);

            // act
            mainPresenter.BackupConfig(false);

            // assert
            configurationMock.VerifySet(cm => cm.Name = "Current", Times.Once);
            modelMock.Setup(mm => mm.AddConfig(configurationMock.Object));

        }

        [Test]
        public void BackupConfig_WhenShowSuccessIsTrue_ShouldShowSuccessMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            var configurationMock = new Mock<EConfiguration>();
            var systemConfigPath = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts");
            modelMock.Setup(mm => mm.ReadExternalConfig(systemConfigPath)).Returns(configurationMock.Object).Verifiable();
            modelMock.Setup(mm => mm.HostsFilePath).Returns(systemConfigPath);
            configurationMock.Setup(cm => cm.Name).Returns("test");
            configurationMock.Setup(cm => cm.Content).Returns("test");
            configurationMock.Setup(cm => cm.Id).Returns(0);

            // act
            mainPresenter.BackupConfig(true);

            // assert
            configurationMock.VerifySet(cm => cm.Name = "Current", Times.Once);
            modelMock.Verify(mm => mm.AddConfig(configurationMock.Object),Times.Once);
            mainViewMock.Verify(
                mv => mv.ShowMessage(
                    MessageType.Info, 
                    Language.Success_Tittle,
                    Language.BackupSuccess_Text),
                Times.Once);

        }

        [Test]
        public void BackupConfig_WhenUnexpectedExceptionThrown_ShouldDisplayErrorMessage()
        {
            // arrange
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var factoryMock = new Mock<IViewFactory>();
            var mainPresenter = new MainPresenter(mainViewMock.Object, factoryMock.Object, modelMock.Object);
            var exception = new Exception("Unknown error");
            modelMock.Setup(mm => mm.ReadExternalConfig(It.IsAny<string>())).Throws(exception);


            // act
            mainPresenter.BackupConfig(false);

            // assert
            mainViewMock.Verify(
                mv => mv.ShowMessage(
                    MessageType.Error, 
                    Language.UnexpectedError_Text,
                    string.Format(Language.BackupError_Text, exception.Message)),
                Times.Once);

        }
        // TODO: Implementar importación de la configuración inicial del equipo
        // TODO: Implementar firstRun
    }

}