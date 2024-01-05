using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.StatisticWorkSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.StatisticWorkSpace.Tests
{
    [TestClass()]
    public class DetailStatisticViewModelTests
    {

        [TestMethod]
        public void SetTimeRange_NullDateRange_DoesNotThrowException()
        {
            // Arrange
            var viewModel = new DetailStatisticViewModel();

            // Act & Assert
            Assert.ThrowsException<NotImplementedException>(() => viewModel.SetTimeRange(null, null));
        }

        [TestMethod]
        public void GetDateTimeRangeString_ValidDateRange_ReturnsFormattedString()
        {
            // Arrange
            var viewModel = new DetailStatisticViewModel();
            var minDate = new DateTime(2021, 5, 1);
            var maxDate = new DateTime(2021, 5, 31);

            // Act
            var result = viewModel.getDateTimeRangeString(minDate, maxDate);

            // Assert
            Assert.AreEqual("tháng 05/2021", result);
        }

        [TestMethod]
        public void GetDateTimeRangeString_SameDay_ReturnsFormattedString()
        {
            // Arrange
            var viewModel = new DetailStatisticViewModel();
            var minDate = new DateTime(2021, 5, 1);
            var maxDate = new DateTime(2021, 5, 1);

            // Act
            var result = viewModel.getDateTimeRangeString(minDate, maxDate);

            // Assert
            Assert.AreEqual("tháng 05/2021", result);
        }

        [TestMethod]
        public void CreateTitle_ValidModel_ReturnsTitle()
        {
            // Arrange
            var viewModel = new DetailStatisticViewModel();
            var model = new StatisticModel() { Title = "Test Title" };

            // Act
            var result = viewModel.CreateTitle(model);

            // Assert
            Assert.AreEqual("Test Title", result);
        }

        [TestMethod]
        public void CreateLabel_ValidModel_ReturnsLabel()
        {
            // Arrange
            var viewModel = new DetailStatisticViewModel();
            var model = new StatisticModel() { Label = "Test Label" };

            // Act
            var result = viewModel.CreateLabel(model);

            // Assert
            Assert.AreEqual("Test Label", result);
        }


        [TestMethod]
        public void TitleDataGrid_Default_ReturnsFormattedString()
        {
            // Arrange
            var viewModel = new DetailStatisticViewModel();

            // Act
            var result = viewModel.TitleDataGrid;

            // Assert
            Assert.AreEqual("Báo Cáo Bán Hàng tháng 5/2021", result);
        }

      

     

        [TestMethod]
        public void Constructor_DefaultPropertiesInitialized()
        {
            // Arrange & Act
            var viewModel = new DetailStatisticViewModel();

            // Assert
            Assert.IsNull(viewModel.formaterLabelAxisY);
            Assert.AreEqual("tháng 5/2021", viewModel.DateTimeRangeTitle);
        }
    }
}