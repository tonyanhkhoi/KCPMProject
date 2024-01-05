using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MainProject.Converters.Tests
{
    [TestClass]
    public class TableConverterTests
    {
        private TableConverter _converter;

        [TestInitialize]
        public void Initialize()
        {
            _converter = new TableConverter();
        }

        [TestMethod]
        public void Convert_ZeroValue_ReturnsMangVe()
        {
            // Arrange
            long value = 0;

            // Act
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("Mang về", result);
        }

        [TestMethod]
        public void Convert_NonZeroValue_ReturnsOriginalValue()
        {
            // Arrange
            long value = 42;

            // Act
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void Convert_NegativeValue_ReturnsOriginalValue()
        {
            // Arrange
            long value = -5;

            // Act
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(value, result);
        }



        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Act & Assert
            _converter.ConvertBack(null, null, null, CultureInfo.CurrentCulture);
        }

        [TestMethod]
        public void ProvideValue_ReturnsSelf()
        {
            // Arrange
            var serviceProvider = new MockServiceProvider();

            // Act
            var result = _converter.ProvideValue(serviceProvider);

            // Assert
            Assert.AreSame(_converter, result);
        }

        // Add more test methods to cover different scenarios and edge cases.
    }

    // Helper class to mock IServiceProvider
    public class MockServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return null;
        }
    
}
}