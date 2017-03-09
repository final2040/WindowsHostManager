using System;
using System.Security.Policy;
using System.Windows.Forms;
using AppResources;
using Moq;
using NUnit.Framework;
using Entities;
using Presenter;

namespace UnitTests
{
    [TestFixture]
    public class EditPresenterTests
    {
        private Mock<IEditView> _viewMock;
        private Mock<IHostManager> _modelMock;

        [SetUp]
        public void Setup()
        {
            _viewMock = new Mock<IEditView>();
            _modelMock = new Mock<IHostManager>();
        }

        [Test]
        public void Save_WhenNameIsEmpty_ShoulDisplayErrorMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "", "Test");
            _viewMock.Setup(vm => vm.Configuration.Name).Returns("");
            _viewMock.Setup(vm => vm.Configuration.Content).Returns("Test");

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error,
                    Language.Error_Data_Tittle,
                    Language.Error_EmptyName_Text),
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
                    Language.Error_Data_Tittle,
                    Language.Error_EmptyContent_Text),
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
                    Language.Error_Data_Tittle,
                    Language.Error_InvalidNameFormat_Text),
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
                    Language.Error_Data_Tittle,
                    Language.Error_EmptyName_Text+ Environment.NewLine +
                    Language.Error_EmptyContent_Text),
                Times.Once);
        }

        [Test]
        public void Save_WhenConfigurationIsCorrectAndChangesAreMaded_ShouldSaveConfiguration()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);

            // act
            presenter.Save();

            // assert
            _viewMock.VerifySet(vm => vm.DialogResult = DialogResult.OK);
            _viewMock.Verify(vm => vm.Close());
            _modelMock.Verify(mm => mm.AddConfig(editedConfiguration), Times.Once);
        }

        [Test]
        public void Save_WhenConfigurationIsSaved_ShouldDisplaySuccessMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Info, 
                    Language.Success_Tittle,
                    Language.SuccessEdit_Text), Times.Once);
            _viewMock.VerifySet(vm => vm.DialogResult = DialogResult.OK);
            _viewMock.Verify(vm => vm.Close());
            
        }

        [Test]
        public void Save_WhenConfigurationIsNotChanged_ShouldDoNothing()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(false);

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(vm => vm.Close());
            _modelMock.Verify(mm => mm.AddConfig(editedConfiguration), Times.Never);
        }

        [Test]
        public void Cancel_ShouldCloseView()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(false);

            // act
            presenter.Cancel();

            // assert
            _viewMock.VerifySet(vm => vm.DialogResult = DialogResult.Cancel);
            _viewMock.Verify(vm => vm.Close());

        }

        [Test]
        public void Cancel_WhenChangesAreMade_ShouldShowAConfirmationMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);

            // act
            presenter.Cancel();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.YesNo,
                    Language.Warning_Tittle,
                    Language.EditCancel_Confirmation));
        }

        [Test]
        public void Cancel_WhenYesIsSelected_ShouldCloseView()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _viewMock.Setup(vm => vm.ShowMessage(It.IsAny<MessageType>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(DialogResult.Yes);
            // act
            presenter.Cancel();

            // assert
            _viewMock.VerifySet(vm => vm.DialogResult = DialogResult.Cancel);
            _viewMock.Verify(vm => vm.Close());
        }

        [Test]
        public void Cancel_WhenNoIsSelected_ShouldDoNothing()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _viewMock.Setup(vm => vm.ShowMessage(It.IsAny<MessageType>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(DialogResult.No);
            // act
            presenter.Cancel();

            // assert
            _viewMock.VerifySet(vm => vm.DialogResult = DialogResult.Cancel, Times.Never);
            _viewMock.Verify(vm => vm.Close(), Times.Never);
        }
        
        [Test]
        public void Save_WhenUnexpectedExceptionIsThrown_ShouldDisplayError()
        {
            // arrange
            var presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");
            var exceptionToThrow = new Exception("Unknown Error");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _modelMock.Setup(mm => mm.AddConfig(editedConfiguration)).Throws(exceptionToThrow);
            
            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.Error, 
                    Language.UnexpectedError_Tittle,
                    string.Format(Language.UnexpectedError_Text,exceptionToThrow.Message)));
            
        }

        // New Configuration mode
        [Test]
        public void Save_WhenNewConfigModeIsOn_ShouldCheckIfAnotherConfigurationWithTheSameNameExists()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _viewMock.Setup(vm => vm.EditMode).Returns(EditMode.New);

            // act
            presenter.Save();

            // assert
            _modelMock.Verify(mm => mm.Exists(editedConfiguration), Times.Once);
        }

        [Test]
        public void Save_WhenNewConfigAnotherConfigExists_ShouldDisplayConfirmationMessage()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _viewMock.Setup(vm => vm.EditMode).Returns(EditMode.New);
            _modelMock.Setup(mm => mm.Exists(editedConfiguration)).Returns(true);

            // act
            presenter.Save();

            // assert
            _viewMock.Verify(
                vm => vm.ShowMessage(
                    MessageType.YesNo, 
                    Language.OverwriteMessage_Tittle,
                    Language.OverWriteMessage_Text));
        }

        [Test]
        public void Save_WhenNewConfig_ShouldSaveConfiguration()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _viewMock.Setup(vm => vm.EditMode).Returns(EditMode.New);
            _modelMock.Setup(mm => mm.Exists(editedConfiguration)).Returns(false);

            // act
            presenter.Save();

            // assert
            _modelMock.Verify(mm => mm.AddConfig(editedConfiguration), Times.Once);
        }


        [Test]
        public void Save_WhenNewConfigYesSelected_ShouldSaveConfig()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _viewMock.Setup(vm => vm.EditMode).Returns(EditMode.New);
            _modelMock.Setup(mm => mm.Exists(editedConfiguration)).Returns(true);
            _viewMock.Setup(
                vm => vm.ShowMessage(
                    MessageType.YesNo,
                    Language.OverwriteMessage_Tittle,
                    Language.OverWriteMessage_Text))
                    .Returns(DialogResult.Yes);
            // act
            presenter.Save();

            // assert
            _modelMock.Verify(
                mm => mm.AddConfig(editedConfiguration), Times.Once);
        }

        [Test]
        public void Save_WhenNewConfigNoSelected_ShouldDoNothing()
        {
            // arrange
            PEdit presenter = new PEdit(_viewMock.Object, _modelMock.Object);
            var editedConfiguration = new EConfiguration(0, "test", "Test");

            _viewMock.Setup(vm => vm.Configuration).Returns(editedConfiguration).Verifiable();
            _viewMock.Setup(vm => vm.IsDirty).Returns(true);
            _viewMock.Setup(vm => vm.EditMode).Returns(EditMode.New);
            _modelMock.Setup(mm => mm.Exists(editedConfiguration)).Returns(true);
            _viewMock.Setup(
                vm => vm.ShowMessage(
                    MessageType.YesNo,
                    Language.OverwriteMessage_Tittle,
                    Language.OverWriteMessage_Text))
                    .Returns(DialogResult.No);
            // act
            presenter.Save();

            // assert
            _modelMock.Verify(
                mm => mm.AddConfig(editedConfiguration), Times.Never);
            _viewMock.Verify(vm => vm.Close(), Times.Never);
        }

    }
}