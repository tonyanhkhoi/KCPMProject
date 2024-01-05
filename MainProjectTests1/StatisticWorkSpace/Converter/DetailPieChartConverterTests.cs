using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.StatisticWorkSpace.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using OxyPlot.Series;

namespace MainProject.StatisticWorkSpace.Converter.Tests
{
    [TestClass]
    public class DetailPieChartConverterTests
    {
        [TestMethod]
        public void Convert_NullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.Convert(null, null, null, null));
        }

        [TestMethod]
        public void Convert_InvalidValueType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.Convert("InvalidValueType", null, null, null));
        }

        [TestMethod]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }




        [TestMethod]
        public void ConvertBack_NullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithNonNullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack("Non-Null Value", null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidValueType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, typeof(string), null, null));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidTargetType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }

        [TestMethod]
        public void ConvertBack_WithDifferentCulture_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new DetailPieChartConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, new CultureInfo("vi-VN")));
        }

    }
}