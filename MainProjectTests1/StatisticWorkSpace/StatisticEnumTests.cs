using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.StatisticWorkSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.StatisticWorkSpace.Tests
{
    [TestClass]
    public class StatisticEnumTests
    {
        [TestMethod]
        public void GetString_DayOfWeek_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.DayOfWeek;

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.AreEqual("Từng ngày trong tuần", result);
        }

        [TestMethod]
        public void GetString_DayOfMonth_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.DayOfMonth;

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.AreEqual("Từng ngày trong tháng", result);
        }

        [TestMethod]
        public void GetString_WeekOfMonth_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.WeekOfMonth;

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.AreEqual("Từng tuần trong tháng", result);
        }

        [TestMethod]
        public void GetString_MonthOfYear_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.MonthOfYear;

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.AreEqual("Từng tháng trong năm", result);
        }

        [TestMethod]
        public void GetString_InvalidMode_ReturnsNull()
        {
            // Arrange
            var mode = (StatisticMode)10; // An invalid mode

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetString_NegativeMode_ReturnsNull()
        {
            // Arrange
            var mode = (StatisticMode)(-1); // An invalid mode

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetString_MinValueMode_ReturnsNull()
        {
            // Arrange
            var mode = (StatisticMode)int.MinValue; // An invalid mode

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetString_MaxValueMode_ReturnsNull()
        {
            // Arrange
            var mode = (StatisticMode)int.MaxValue; // An invalid mode

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void GetString_EmptyStringMode_ReturnsNull()
        {
            // Arrange
            var mode = (StatisticMode)0; // Default mode value

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetString_DayOfWeekUpperCase_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.DayOfWeek;

            // Act
            var result = StatisticEnum.GetString(mode).ToUpper();

            // Assert
            Assert.AreEqual("TỪNG NGÀY TRONG TUẦN", result);
        }

        [TestMethod]
        public void GetString_DayOfMonthUpperCase_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.DayOfMonth;

            // Act
            var result = StatisticEnum.GetString(mode).ToUpper();

            // Assert
            Assert.AreEqual("TỪNG NGÀY TRONG THÁNG", result);
        }

        [TestMethod]
        public void GetString_WeekOfMonthUpperCase_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.WeekOfMonth;

            // Act
            var result = StatisticEnum.GetString(mode).ToUpper();

            // Assert
            Assert.AreEqual("TỪNG TUẦN TRONG THÁNG", result);
        }

        [TestMethod]
        public void GetString_MonthOfYearUpperCase_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.MonthOfYear;

            // Act
            var result = StatisticEnum.GetString(mode).ToUpper();

            // Assert
            Assert.AreEqual("TỪNG THÁNG TRONG NĂM", result);
        }

        [TestMethod]
        public void GetString_InvalidModeUpperCase_ReturnsNull()
        {
            // Arrange
            var mode = (StatisticMode)10; // An invalid mode

            // Act
            var result = StatisticEnum.GetString(mode);

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void GetString_UnderScoreMode_ReturnsNull()
        {
            // Arrange
            var mode = StatisticMode.WeekOfMonth;

            // Act
            var result = StatisticEnum.GetString(mode).Replace(" ", "_");

            // Assert
            Assert.AreNotEqual("TỪNG_TUẦN_TRONG_THÁNG", result);
        }

        [TestMethod]
        public void GetString_ApostropheMode_ReturnsNull()
        {
            // Arrange
            var mode = StatisticMode.DayOfWeek;

            // Act
            var result = StatisticEnum.GetString(mode).Replace(" ", "'");

            // Assert
            Assert.AreNotEqual("TỪNG'NGÀY'TRONG'TUẦN", result);
        }

        [TestMethod]
        public void GetString_WithSpecialCharacters_ReturnsCorrectString()
        {
            // Arrange
            var mode = StatisticMode.MonthOfYear;

            // Act
            var result = StatisticEnum.GetString(mode) + "!@#$%^&*()";

            // Assert
            Assert.AreNotEqual("TỪNG THÁNG TRONG NĂM!@#$%^&*()", result);
        }
    }
}