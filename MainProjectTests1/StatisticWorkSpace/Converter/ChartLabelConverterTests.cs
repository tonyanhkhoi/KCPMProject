using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.StatisticWorkSpace.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MainProject.StatisticWorkSpace.Converter.Tests
{
    [TestClass]
    public class ChartLabelConverterTests
    {
        [TestMethod]
        public void Convert_NullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartLabelConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.Convert(null, null, null, null));
        }

        [TestMethod]
        public void Convert_InvalidValueType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartLabelConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.Convert("InvalidValueType", null, null, null));
        }

        [TestMethod]
        public void Convert_ValidObservableCollection_ReturnsArrayOfStrings()
        {
            // Arrange
            var converter = new ChartLabelConverter();
            var list = new ObservableCollection<StatisticModel>
            {
                new StatisticModel { Label = "Label1" },
                new StatisticModel { Label = "Label2" },
                new StatisticModel { Label = "Label3" }
            };

            // Act
            var result = converter.Convert(list, null, null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(string[]));
        }

        [TestMethod]
        public void Convert_ValidObservableCollection_ReturnsCorrectArrayOfLabels()
        {
            // Arrange
            var converter = new ChartLabelConverter();
            var list = new ObservableCollection<StatisticModel>
            {
                new StatisticModel { Label = "Label1" },
                new StatisticModel { Label = "Label2" },
                new StatisticModel { Label = "Label3" }
            };

            // Act
            var result = converter.Convert(list, null, null, null) as string[];

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new string[] { "Label1", "Label2", "Label3" }, result);
        }

        [TestMethod]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartLabelConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithNonNullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartLabelConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack("Non-Null Value", null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidValueType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartLabelConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, typeof(string), null, null));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidTargetType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartLabelConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithDifferentCulture_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartLabelConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, new CultureInfo("vi-VN")));
        }

        // Add more test methods covering different scenarios and edge cases.
    }
}