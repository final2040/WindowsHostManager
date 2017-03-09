using System;
using System.IO;
using System.Windows.Forms;
using AppResources;
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
                    Language.Error_Data_Tittle,
                    Language.Error_EmptyName_Text), Times.Once);
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
                    Language.Error_Data_Tittle, 
                    Language.Error_EmptyPath_Text), Times.Once);
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
                    Language.Error_Data_Tittle, 
                    Language.Error_EmptyName_Text + Environment.NewLine +
                    Language.Error_EmptyPath_Text),
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
                    Language.Error_Data_Tittle,
                    Language.Error_InvalidNameFormat_Text),
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
            var exceptionToThrow = new Exception("Error desconocido");

            _viewMock.Setup(vm => vm.ConfigName).Returns("Test");
            _viewMock.Setup(vm => vm.Path).Returns(filePath);
            _modelMock.Setup(mm => mm.ReadExternalConfig(filePath)).Throws(exceptionToThrow);

            // act
            importPresenter.Import();

            // assert
            _modelMock.Verify(
                mm => mm.ReadExternalConfig(filePath)
                );
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    Language.UnexpectedError_Tittle, 
                    string.Format(Language.UnexpectedError_Text, exceptionToThrow.Message))
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
                    Language.Success_Tittle,
                    Language.SuccessImport_Text
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
                    mv.ShowMessage(
                        MessageType.YesNo,
                        Language.OverwriteMessage_Tittle,
                        Language.OverWriteMessage_Text)
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
                mv.ShowMessage(MessageType.YesNo, Language.OverwriteMessage_Tittle,
                    Language.OverWriteMessage_Text))
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
                mv.ShowMessage(MessageType.YesNo, Language.OverwriteMessage_Tittle,
                    Language.OverWriteMessage_Text))
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