using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;
using Model;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class ModelStub : HostManagerFileDal
    {
        public ModelStub(IFileHelper fileHelper)
        {
            base.FileHelper = fileHelper;
        }
    }

    [TestFixture]
    public class ModelTests
    {
        [Test]
        public void LoadConfig_WhenValidConfigIsProvided_ShouldReplaceWindowsHostsFile()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            var configuration = new EConfiguration()
            {
                Name = "file",
                Content = "#File content \\n 192.28.129.100\tsomepage.com"
            };
            
            fileManagerMoq.Setup(
                fm => fm.WriteAllText(It.Is<string>(s => s == "C:\\Windows\\system32\\drivers\\etc\\hosts"), 
                It.Is<string>(s => s == configuration.Content)))
                .Verifiable();

            // act
            model.LoadConfig(configuration);

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadConfig_WhenConfigurationContentIsEmptyOrNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            var configuration = new EConfiguration()
            {
                Name = "file",
                Content = ""
            };
            
            fileManagerMoq.Setup(
                fm => fm.WriteAllText(It.Is<string>(s => s == "C:\\Windows\\system32\\drivers\\etc\\hosts"),
                It.Is<string>(s => s == configuration.Content)))
                .Verifiable();

            // act
            model.LoadConfig(configuration);

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadConfig_WhenIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            EConfiguration configuration = null;
            
            // act
            model.LoadConfig(configuration);
            
        }

        [Test]
        public void AddConfig_WhenValidConfigurationIsProvided_ShouldSaveConfigurationIntoFile()
        {
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            var configuration = new EConfiguration()
            {
                Content = "#File content \\n 192.28.129.100\tsomepage.com",
                Name = "test"
            };
            string expectedFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.ChangeExtension(configuration.Name, ".host"));
            fileManagerMoq.Setup(fm => fm.WriteAllText(expectedFilename, configuration.Content)).Verifiable();

            // act
            model.AddConfig(configuration);

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        public void GetAll_ShouldReturnListOfConfigurations()
        {
            // arrange
            var expectedConfigurations = new List<EConfiguration>()
            {
                new EConfiguration() {Name = "file", Content = "TestContent"},
                new EConfiguration() {Name = "file2", Content = "TestContent"},
                new EConfiguration() {Name = "file3", Content = "TestContent"}
            };

            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            fileManagerMoq.Setup(
                fm => fm.GetFiles(
                    It.Is<string>(s => s == Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"*.host"))))
                .Returns(new[] {"D:\\file.host", "D:\\file2.host", "D:\\file3.host"})
                .Verifiable();

            fileManagerMoq.Setup(
                fm => fm.Exists(It.IsAny<string>())).Returns(true);

            fileManagerMoq.Setup(
                fm => fm.ReadAllText(It.IsAny<string>()))
                .Returns("TestContent").Verifiable();
               
           
            // act
            var configList = model.GetAll();

            // assert
            foreach (var configuration in configList)
            {
                Assert.IsTrue(expectedConfigurations.Any(l => l.Equals(configuration)));
            }
        }

        [Test]
        public void GetConfig_WhenValidFileIsProvided_ShouldReturnSingleConfiguration()
        {
            // arrange
            var expectedConfiguration = new EConfiguration()
            { Name = "file", Content = "TestContent"};
            
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
           
            fileManagerMoq.Setup(
                fm => fm.Exists(It.IsAny<string>())).Returns(true);

            fileManagerMoq.Setup(
                fm => fm.ReadAllText(It.IsAny<string>()))
                .Returns("TestContent").Verifiable();


            // act
            var configuration = model.ReadExternalConfig("D:\\file.host");

            // assert
           Assert.AreEqual(configuration, expectedConfiguration);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetConfig_WhenNotExistingFileIsProvided_ShouldThrowFileNotFoundException()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);

            fileManagerMoq.Setup(
                fm => fm.Exists(It.IsAny<string>())).Returns(false);
            
            // act
            var configuration = model.ReadExternalConfig("D:\\file.host");
        }

        [Test]
        public void DeleteConfig_WhenValidConfigurationIsProvided_ShouldDeleteConfigurationFile()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            var configuration = new EConfiguration()
            {
                Content = "#File content \\n 192.28.129.100\tsomepage.com",
                Name = "test"
            };
            string expectedFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.ChangeExtension(configuration.Name, ".host"));
            fileManagerMoq.Setup(
                fm => fm.Delete(It.Is<string>(s => s ==expectedFilename))
                ).Verifiable();

            // act
            model.DeleteConfig(configuration);
            
            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        public void Exist_WhenConfigurationExists_ShouldReturnTrue()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            var configuration = new EConfiguration()
            {
                Content = "#File content \\n 192.28.129.100\tsomepage.com",
                Name = "test"
            };
            string expectedFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.ChangeExtension(configuration.Name, ".host"));
            fileManagerMoq.Setup(
                fm => fm.Exists(It.Is<string>(s => s == expectedFilename))
                ).Returns(true).Verifiable();

            // act
            var result = model.Exists(configuration);

            // assert
            fileManagerMoq.Verify();
            Assert.IsTrue(result);
        }

        [Test]
        public void Exist_WhenConfigurationDontExists_ShouldReturnFalse()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new ModelStub(fileManagerMoq.Object);
            var configuration = new EConfiguration()
            {
                Content = "#File content \\n 192.28.129.100\tsomepage.com",
                Name = "test"
            };
            string expectedFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.ChangeExtension(configuration.Name, ".host"));
            fileManagerMoq.Setup(
                fm => fm.Exists(It.Is<string>(s => s == expectedFilename))
                ).Returns(false).Verifiable();

            // act
            var result = model.Exists(configuration);

            // assert
            fileManagerMoq.Verify();
            Assert.IsFalse(result);
        }

    }
}
