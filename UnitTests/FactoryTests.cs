using System;
using NUnit.Framework;
using View;

namespace UnitTests
{
    [TestFixture]
    public class FactoryTests
    {
        [Test]
        public void ShouldCreateEditView()
        {
            // Arrange
            ViewFactory factory = new ViewFactory();

            // act
            var result = factory.Create("EditView");

            // assert
            Assert.IsInstanceOf<EditView>(result);
        }

        [Test]
        public void ShouldCreateImportView()
        {
            // Arrange
            ViewFactory factory = new ViewFactory();

            // act
            var result = factory.Create("ImportView");

            // assert
            Assert.IsInstanceOf<ImportView>(result);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenInvalidViewNameIsProvided_ShouldThrowException()
        {
            // Arrange
            ViewFactory factory = new ViewFactory();

            // act
            var result = factory.Create("asded");

            // assert
            Assert.IsInstanceOf<ImportView>(result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenNullOrEmptyIsProvided_ShouldThrowException()
        {
            // Arrange
            ViewFactory factory = new ViewFactory();

            // act
            var result = factory.Create(null);

            // assert
            Assert.IsInstanceOf<ImportView>(result);
        }
    }
}