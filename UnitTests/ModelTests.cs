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
    [TestFixture]
    public class ModelTests
    {
        [Test]
        public void LoadConfig_WhenValidFileIsProvided_ShouldReplaceWindowsHostsFile()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);
            fileManagerMoq.Setup(fm => fm.Exists(It.IsAny<string>())).Returns(true).Verifiable();
            fileManagerMoq.Setup(fm => fm.Copy(It.Is<string>(s => s== "D:\\file.host"),It.Is<string>(s => s == "C:\\Windows\\system32\\drivers\\etc\\hosts"), It.Is<bool>(t => t))).Verifiable();

            // act
            model.LoadConfig("D:\\file.host");

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadConfig_WhenNoExistFileIsProvided_ShouldThrowFileNotFoundException()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            fileManagerMoq.Setup(fm => fm.Exists(It.IsAny<string>())).Returns(false).Verifiable();
            fileManagerMoq.Setup(fm => fm.Copy(It.Is<string>(s => s == "D:\\file.host"), It.Is<string>(s => s == "C:\\Windows\\system32\\drivers\\etc\\hosts"), It.Is<bool>(t => t))).Verifiable();

            var model = new HostManagerFileDal(fileManagerMoq.Object);
            
            // act
            model.LoadConfig("D:\\file.host");

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        public void AddConfig_WhenValidTextIsProvided_ShouldCopyFileToProgramFolder()
        {
            // arrange
            var hostContent = "#File content \\n 192.28.129.100\tsomepage.com";
            var contentName = "Test"; 
            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.ChangeExtension(contentName, ".host"));
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);
            fileManagerMoq.Setup(
                fm => fm.WriteAllText(
                        It.Is<string>(s => s == expectedPath),
                        It.Is<string>(s => s == hostContent))
                ).Verifiable();

            // act
            model.AddConfig(contentName, hostContent);

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        [TestCase("", "Some")]
        [TestCase(" ", "Some")]
        [TestCase(null, "Some")]
        [TestCase("Some", "")]
        [TestCase("Some", " ")]
        [TestCase("Some", null)]
        [TestCase(null, null)]
        [TestCase(" ", " ")]
        [TestCase("", "")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddConfig_WhenContentOrNameIsNullOrEmpty_ShouldThrowNullArgumentException(string hostContent, string contentName)
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);
            
            // act
            model.AddConfig(contentName, hostContent);
        }

        [Test]
        public void AddConfig_WhenValidFileIsProvided_ShouldAddFileToProgramFolder()
        {
            // arrange
            var expectedSource = "D:\\file.txt";
            var expectedDestination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.ChangeExtension(Path.GetFileName(expectedSource),"host"));

            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);

            fileManagerMoq.Setup(
                fm => fm.Exists(It.IsAny<string>()))
                .Returns(true);

            fileManagerMoq.Setup(
                fm => fm.Copy(
                    It.Is<string>(s => s == expectedSource), 
                    It.Is<string>(s => s == expectedDestination), 
                    It.Is<bool>(t => t)))
                .Verifiable();

            // act
            model.AddConfig(expectedSource);

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        public void AddConfig_WhenConfigurationIsProvided_ShouldCopyFileToProgramFolder()
        {
            // arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.ChangeExtension("test", ".host"));
            var configuration = new EConfiguration()
            {
                Content = "#File content \\n 192.28.129.100\tsomepage.com",
                Path = filePath
            };
            
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);
            fileManagerMoq.Setup(
                fm => fm.WriteAllText(
                        It.Is<string>(s => s == filePath),
                        It.Is<string>(s => s == "#File content \\n 192.28.129.100\tsomepage.com"))
                ).Verifiable();

            // act
            model.AddConfig(configuration);

            // assert
            fileManagerMoq.Verify();
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void AddConfig_WhenNoExistentFileIsProvided_ShouldThrowFileNotFoundExcepetion()
        {
            // arrange
            var expectedSource = "D:\\file.txt";
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);

            fileManagerMoq.Setup(
                fm => fm.Exists(It.IsAny<string>())
                ).Returns(false);

            // act
            model.AddConfig(expectedSource);
        }

        [Test]
        public void GetAll_ShouldReturnListOfConfigurations()
        {
            // arrange
            var expectedConfigurations = new List<EConfiguration>()
            {
                new EConfiguration() {Path = "D:\\file.host", Content = "TestContent"},
                new EConfiguration() {Path = "D:\\file2.host", Content = "TestContent"},
                new EConfiguration() {Path = "D:\\file3.host", Content = "TestContent"}
            };

            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);
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
            { Path = "D:\\file.host", Content = "TestContent"};
            
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);
           
            fileManagerMoq.Setup(
                fm => fm.Exists(It.IsAny<string>())).Returns(true);

            fileManagerMoq.Setup(
                fm => fm.ReadAllText(It.IsAny<string>()))
                .Returns("TestContent").Verifiable();


            // act
            var configuration = model.GetConfig("D:\\file.host");

            // assert
           Assert.AreEqual(configuration, expectedConfiguration);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetConfig_WhenNotExistingFileIsProvided_ShouldThrowFileNotFoundException()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);

            fileManagerMoq.Setup(
                fm => fm.Exists(It.IsAny<string>())).Returns(false);
            
            // act
            var configuration = model.GetConfig("D:\\file.host");
        }

        [Test]
        public void DeleteConfig_WhenValidConfigurationIsProvided_ShouldDeleteConfigurationFile()
        {
            // arrange
            var fileManagerMoq = new Mock<IFileHelper>();
            var model = new HostManagerFileDal(fileManagerMoq.Object);
            var configuration = new EConfiguration()
            {
                Content = "#File content \\n 192.28.129.100\tsomepage.com",
                Path = "c:\\test.host"
            };

            fileManagerMoq.Setup(
                fm => fm.Delete(It.Is<string>(s => s ==configuration.Path))
                ).Verifiable();

            // act
            model.DeleteConfig(configuration);
            
            // assert
            fileManagerMoq.Verify();
        }
    }
}
