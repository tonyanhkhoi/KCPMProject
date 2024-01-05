using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.StatisticWorkSpace.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MainProject.StatisticWorkSpace.Converter.Tests
{
    [TestClass]
    public class TimeConverterTests
    {
        [TestMethod]
        public void Convert_DayOfWeek_NullModel_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.Convert(null, typeof(string), StatisticMode.DayOfWeek, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Convert_DayOfWeek_InvalidModelType_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.Convert("InvalidModelType", typeof(string), StatisticMode.DayOfWeek, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Convert_WeekOfMonth_NullModel_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.Convert(null, typeof(string), StatisticMode.WeekOfMonth, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Convert_MonthOfYear_NullModel_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.Convert(null, typeof(string), StatisticMode.MonthOfYear, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ConvertBack_WithNonNullValue_ReturnsNull()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.ConvertBack("Non-Null Value", null, null, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertBack_WithInvalidModelType_ReturnsNull()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.ConvertBack(null, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void Convert_DayOfWeek_ReturnsCorrectString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel() { TimeMin = new DateTime(2021, 1, 4) }; // Monday

            // Act
            var result = converter.Convert(model, typeof(string), StatisticMode.DayOfWeek, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("Thứ Hai", result);
        }

        [TestMethod]
        public void Convert_WeekOfMonth_ReturnsCorrectString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel()
            {
                TimeMin = new DateTime(2021, 1, 4), // Monday
                TimeMax = new DateTime(2021, 1, 10) // Sunday
            };

            // Act
            var result = converter.Convert(model, typeof(string), StatisticMode.WeekOfMonth, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("Từ 04/01 - 10/01", result);
        }

        [TestMethod]
        public void Convert_MonthOfYear_ReturnsCorrectString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel() { TimeMin = new DateTime(2021, 5, 1) }; // May

            // Act
            var result = converter.Convert(model, typeof(string), StatisticMode.MonthOfYear, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("Năm 2021", result);
        }

        [TestMethod]
        public void Convert_InvalidMode_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel() { TimeMin = new DateTime(2021, 1, 4) }; // Monday

            // Act
            var result = converter.Convert(model, typeof(string), (StatisticMode)(-1), CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Convert_NullModel_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.Convert(null, typeof(string), StatisticMode.DayOfWeek, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ConvertBack_ReturnsNull()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.ConvertBack(null, null, null, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void Convert_DayOfWeek_WithDifferentCulture_ReturnsCorrectString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel() { TimeMin = new DateTime(2021, 1, 4) }; // Monday

            // Act
            var result = converter.Convert(model, typeof(string), StatisticMode.DayOfWeek, new CultureInfo("vi-VN"));

            // Assert
            Assert.AreEqual("Thứ Hai", result);
        }

        [TestMethod]
        public void Convert_WeekOfMonth_WithDifferentCulture_ReturnsCorrectString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel()
            {
                TimeMin = new DateTime(2021, 1, 4), // Monday
                TimeMax = new DateTime(2021, 1, 10) // Sunday
            };

            // Act
            var result = converter.Convert(model, typeof(string), StatisticMode.WeekOfMonth, new CultureInfo("vi-VN"));

            // Assert
            Assert.AreEqual("Từ 04/01 - 10/01", result);
        }

        [TestMethod]
        public void Convert_MonthOfYear_WithDifferentCulture_ReturnsCorrectString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel() { TimeMin = new DateTime(2021, 5, 1) }; // May

            // Act
            var result = converter.Convert(model, typeof(string), StatisticMode.MonthOfYear, new CultureInfo("vi-VN"));

            // Assert
            Assert.AreEqual("Năm 2021", result);
        }

        [TestMethod]
        public void Convert_InvalidMode_WithDifferentCulture_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();
            var model = new StatisticModel() { TimeMin = new DateTime(2021, 1, 4) }; // Monday

            // Act
            var result = converter.Convert(model, typeof(string), (StatisticMode)(-1), new CultureInfo("vi-VN"));

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Convert_NullModel_WithDifferentCulture_ReturnsEmptyString()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.Convert(null, typeof(string), StatisticMode.DayOfWeek, new CultureInfo("vi-VN"));

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ConvertBack_WithNonNullValue_WithDifferentCulture_ReturnsNull()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.ConvertBack("Non-Null Value", null, null, new CultureInfo("vi-VN"));

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertBack_WithInvalidModelType_WithDifferentCulture_ReturnsNull()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.ConvertBack(null, typeof(string), null, new CultureInfo("vi-VN"));

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertBack_WithDifferentCulture_ReturnsNull()
        {
            // Arrange
            var converter = new TimeConverter();

            // Act
            var result = converter.ConvertBack(null, null, null, new CultureInfo("vi-VN"));

            // Assert
            Assert.IsNull(result);
        }

        // Add more test methods covering different scenarios and edge cases.
    }
}