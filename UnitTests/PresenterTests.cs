using System.Collections.Generic;
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
        public void ShouldRetrieveAndPopulateConfiguationList()
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
            var mainPresenter = new PMain(mainViewMock.Object, modelMock.Object);
            modelMock.Setup(mm => mm.GetAll()).Returns(configurations).Verifiable();
            mainViewMock.SetupSet(mv => mv.Configurations = configurations).Verifiable();

            // act
            mainPresenter.UpdateView();

            // assert
            mainViewMock.Verify();
            modelMock.Verify();
        }

        [Test]
        public void WhenThereIsNotConfigToLoad_ShouldThrowErrorMessage()
        {
            // arrange
            var configurations = new List<EConfiguration>();
            var mainViewMock = new Mock<IMainView>();
            var modelMock = new Mock<IHostManager>();
            var mainPresenter = new PMain(mainViewMock.Object, modelMock.Object);
            modelMock.Setup(mm => mm.GetAll()).Returns(configurations).Verifiable();
            mainViewMock.Setup(
                mv =>
                    mv.ShowMessage(MessageType.Error, "Sin Resultados",
                        "No hay configuraciones para cargar, por favor cree o importe configuraciones de archivo Host")).Verifiable();

            // act
            mainPresenter.UpdateView();

            // assert
            mainViewMock.Verify();
            modelMock.Verify();
        }

    }
}