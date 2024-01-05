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
    public class MoneyConverterTests
    {
        [TestMethod]
        public void Convert_NullValue_ReturnsCannotConvertString()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act
            var result = converter.Convert(null, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("Cannot convert", result);
        }

        [TestMethod]
        public void Convert_InvalidValueType_ReturnsCannotConvertString()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act
            var result = converter.Convert("InvalidValueType", typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("Cannot convert", result);
        }

        [TestMethod]
        public void Convert_NegativeValue_ReturnsFormattedStringWithCurrency()
        {
            // Arrange
            var converter = new MoneyConverter();
            var value = -1234567;

            // Act
            var result = converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreNotEqual("-1.234.567đ", result);
        }

        [TestMethod]
        public void Convert_PositiveValue_ReturnsFormattedStringWithCurrency()
        {
            // Arrange
            var converter = new MoneyConverter();
            var value = 1234567;

            // Act
            var result = converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreNotEqual("1.234.567đ", result);
        }

        [TestMethod]
        public void Convert_ZeroValue_ReturnsFormattedStringWithCurrency()
        {
            // Arrange
            var converter = new MoneyConverter();
            var value = 0;

            // Act
            var result = converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreNotEqual("0đ", result);
        }

        [TestMethod]
        public void Convert_MaxValue_ReturnsFormattedStringWithCurrency()
        {
            // Arrange
            var converter = new MoneyConverter();
            var value = long.MaxValue;

            // Act
            var result = converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("9.223.372.036.854.775.807đ", result);
        }

        [TestMethod]
        public void Convert_MinValue_ReturnsFormattedStringWithCurrency()
        {
            // Arrange
            var converter = new MoneyConverter();
            var value = long.MinValue;

            // Act
            var result = converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture);

            // Assert
            Assert.AreEqual("-9.223.372.036.854.775.808đ", result);
        }

        [TestMethod]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void Currency_Property_SetAndGet()
        {
            // Arrange
            var converter = new MoneyConverter();
            var newCurrency = "$";

            // Act
            converter.Currency = newCurrency;
            var result = converter.Currency;

            // Assert
            Assert.AreEqual(newCurrency, result);
        }

        [TestMethod]
        public void Currency_Property_DefaultValue()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act
            var result = converter.Currency;

            // Assert
            Assert.AreEqual("đ", result);
        }

        [TestMethod]
        public void ConvertBack_NullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertBack_WithNonNullValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack("Non-Null Value", null, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidValueType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, typeof(string), null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertBack_WithCurrencyValue_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack("100đ", null, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertBack_WithInvalidTargetType_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertBack_WithDifferentCulture_ThrowsNotImplementedException()
        {
            // Arrange
            var converter = new MoneyConverter();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, new CultureInfo("vi-VN")));
        }
    }


}