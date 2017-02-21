using System;
using System.IO;
using Entities;
using Model;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture()]
    public class FileManagerTests
    {
        [Test]
        public void Test1()
        {
            var mockFileHelper = new Mock<FileHelper>();
            var configuration = new EConfiguration()
            {
                Id = 0,
                Content = "172.28.129.100\tsomething.com",
                Name = "TestName"
            }; 
            var fileManager = new FileManager(mockFileHelper.Object);

            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "00001.hosts");
            var expectedContent = $"#ConfigName:TestName{Environment.NewLine}172.28.129.100\tsomething.com";

            mockFileHelper.Setup(mf => mf.WriteAllText(expectedPath, expectedContent)).Verifiable();

            // act
            fileManager.Add(configuration);

            // assert
            mockFileHelper.VerifyAll();
        }
    }
}