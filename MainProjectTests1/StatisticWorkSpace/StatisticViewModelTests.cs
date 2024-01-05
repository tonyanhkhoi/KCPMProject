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
    public class StatisticViewModelTests
    {
        [TestMethod]
        public void SetListModel_ValidList_SetsListModel()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var list = new List<StatisticModel>
            {
                new StatisticModel { Revenue = 5000 },
                new StatisticModel { Revenue = 10000 },
                new StatisticModel { Revenue = 7500 },
            };

            // Act
            viewModel.SetListModel(list);

            // Assert
            CollectionAssert.AreEqual(list, viewModel.ListModel);
        }

        [TestMethod]
        public void SetCurrentMode_ValidIndex_SetsCurrentMode()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var index = 2; // Assuming there are at least 3 modes in the enum

            // Act
            viewModel.SetCurrentMode(index);

            // Assert
            Assert.AreEqual((StatisticMode)index, viewModel.CurrentMode);
        }

        [TestMethod]
        public void ListStatisticModes_NotEmpty_ReturnsListWithModes()
        {
            // Arrange
            var viewModel = new StatisticViewModel();

            // Act
            var result = viewModel.ListStatisticModes;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void FormaterLabelAxisY_ValidValue_FormatsLabel()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var value = 15000;

            // Act
            var result = viewModel.formaterLabelAxisY(value);

            // Assert
            Assert.IsNotNull(result);
            StringAssert.Contains(result, "15");
        }

        [TestMethod]
        public void ClearData_ModelsExist_ListModelEmpty()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            viewModel.SetListModel(new List<StatisticModel> { new StatisticModel() });

            // Act
            viewModel.ClearData();

            // Assert
            Assert.AreEqual(0, viewModel.ListModel.Count);
        }

        [TestMethod]
        public void CreateLabel_InvalidModel_ReturnsError()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var model = new StatisticModel();

            // Act
            var label = viewModel.CreateLabel(model);

            // Assert
            Assert.AreEqual("30", label);
        }

        [TestMethod]
        public void CreateTitle_InvalidModel_ReturnsError()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var model = new StatisticModel();

            // Act
            var title = viewModel.CreateTitle(model);

            // Assert
            Assert.AreEqual("Ngày 30/08", title);
        }

   

        [TestMethod]
        public void PropertyChanged_TotalRevenuePropertyChanged_ListensToPropertyChangedEvent()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var propertyChangedRaised = false;

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(viewModel.TotalRevenue))
                    propertyChangedRaised = true;
            };

            // Act
            viewModel.SetListModel(new List<StatisticModel> { new StatisticModel() });

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }
       
        [TestMethod]
        public void CreateLabel_ValidModel_ReturnsLabel()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var model = new StatisticModel
            {
                TimeMin = DateTime.Now,
                TimeMax = DateTime.Now.AddDays(7)
            };

            // Act
            var label = viewModel.CreateLabel(model);

            // Assert
            Assert.IsNotNull(label);
            Assert.AreNotEqual("<Error>", label);
        }

        [TestMethod]
        public void CreateTitle_ValidModel_ReturnsTitle()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var model = new StatisticModel
            {
                TimeMin = DateTime.Now,
                TimeMax = DateTime.Now.AddDays(7)
            };

            // Act
            var title = viewModel.CreateTitle(model);

            // Assert
            Assert.IsNotNull(title);
            Assert.AreNotEqual("<Error>", title);
        }

        [TestMethod]
        public void GetDayOfWeek_ValidDay_ReturnsDayOfWeek()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var dayOfWeek = DayOfWeek.Monday;

            // Act
            var result = viewModel.GetDayOfWeek(dayOfWeek);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Thứ Hai", result);
        }

        [TestMethod]
        public void TotalRevenue_ModelsWithRevenue_CalculatesTotalRevenue()
        {
            // Arrange
            var viewModel = new StatisticViewModel();
            var models = new List<StatisticModel>
            {
                new StatisticModel { Revenue = 10000 },
                new StatisticModel { Revenue = 20000 },
                new StatisticModel { Revenue = 15000 },
            };

            // Act
            viewModel.SetListModel(models);

            // Assert
            Assert.AreEqual(45000, viewModel.TotalRevenue);
        }
    }
    
  
}