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
    public class ChartValueConverterTests
    {
        [TestMethod]
        public void Convert_NullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartValueConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.Convert(null, null, null, null));
        }

        [TestMethod]
        public void Convert_InvalidValueType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartValueConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.Convert("InvalidValueType", null, null, null));
        }

        [TestMethod]
        public void Convert_ValidObservableCollection_ReturnsChartValues()
        {
            // Arrange
            var converter = new ChartValueConverter();
            var list = new ObservableCollection<StatisticModel>
            {
                new StatisticModel { Revenue = 10000, Amount = 5 },
                new StatisticModel { Revenue = 20000, Amount = 10 },
                new StatisticModel { Revenue = 15000, Amount = 8 }
            };

            // Act
            var result = converter.Convert(list, null, null, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ChartValues<long>));
        }

        [TestMethod]
        public void Convert_ValidObservableCollection_PropertyNameRevenue_ReturnsChartValuesWithRevenue()
        {
            // Arrange
            var converter = new ChartValueConverter();
            converter.PropertyName = "Revenue";
            var list = new ObservableCollection<StatisticModel>
            {
                new StatisticModel { Revenue = 10000, Amount = 5 },
                new StatisticModel { Revenue = 20000, Amount = 10 },
                new StatisticModel { Revenue = 15000, Amount = 8 }
            };

            // Act
            var result = converter.Convert(list, null, null, null) as ChartValues<long>;

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new long[] { 10000, 20000, 15000 }, result);
        }

        [TestMethod]
        public void Convert_ValidObservableCollection_PropertyNameAmount_ReturnsChartValuesWithAmount()
        {
            // Arrange
            var converter = new ChartValueConverter();
            converter.PropertyName = "Amount";
            var list = new ObservableCollection<StatisticModel>
            {
                new StatisticModel { Revenue = 10000, Amount = 5 },
                new StatisticModel { Revenue = 20000, Amount = 10 },
                new StatisticModel { Revenue = 15000, Amount = 8 }
            };

            // Act
            var result = converter.Convert(list, null, null, null) as ChartValues<long>;

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new long[] { 5, 10, 8 }, result);
        }

        [TestMethod]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartValueConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithNonNullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartValueConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack("Non-Null Value", null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidValueType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartValueConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, typeof(string), null, null));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidTargetType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartValueConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithDifferentCulture_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new ChartValueConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, new CultureInfo("vi-VN")));
        }

        // Add more test methods covering different scenarios and edge cases.
    }
}