using MainProject.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MainProject.ViewModel
{
    [TestClass]
    public class SettingViewModelTests
    {
        [TestMethod]
        public void Save_Data_Store_ValidData_ShouldSaveData()
        {
            // Arrange
            var viewModel = new SettingViewModel();
            viewModel.NameStore = "New Store";
            viewModel.NumberPhone = "123456789";
            viewModel.Address = "New Address";

            // Act
            viewModel.Save_Data_Store.Execute(null);

            // Assert
            Assert.AreEqual("New Store", viewModel.context.PARAMETERs.FirstOrDefault(p => p.NAME == "StoreName").Value);
            Assert.AreEqual("123456789", viewModel.context.PARAMETERs.FirstOrDefault(p => p.NAME == "StorePhone").Value);
            Assert.AreEqual("New Address", viewModel.context.PARAMETERs.FirstOrDefault(p => p.NAME == "StoreAddress").Value);
        }

        [TestMethod]
        public void Save_Data_Store_EmptyData_ShouldThrowException()
        {
            // Arrange
            var viewModel = new SettingViewModel();

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => viewModel.Save_Data_Store.Execute(null));
        }

        [TestMethod]
        public void Change_Data_Store_ShouldChangeModeToEdit()
        {
            // Arrange
            var viewModel = new SettingViewModel();

            // Act
            viewModel.Change_Data_Store.Execute(null);

            // Assert
            Assert.AreEqual(SettingViewModel.ModeButton.edit, viewModel.Mode_btn);
        }

        [TestMethod]
        public void Cancel_Change_Data_Store_ShouldRestoreDataAndChangeModeToSave()
        {
            // Arrange
            var viewModel = new SettingViewModel();
            viewModel.NameStore = "New Store";
            viewModel.NumberPhone = "123456789";
            viewModel.Address = "New Address";
            viewModel.Save_Data_Store.Execute(null);

            // Act
            viewModel.NameStore = "Updated Store";
            viewModel.NumberPhone = "987654321";
            viewModel.Address = "Updated Address";
            viewModel.Cancel_Change_Data_Store.Execute(null);

            // Assert
            Assert.AreEqual("New Store", viewModel.NameStore);
            Assert.AreEqual("123456789", viewModel.NumberPhone);
            Assert.AreEqual("New Address", viewModel.Address);
            Assert.AreEqual(SettingViewModel.ModeButton.save, viewModel.Mode_btn);
        }

        [TestMethod]
        public void Save_Data_Store_InvalidData_ShouldShowErrorMessage()
        {
            // Arrange
            var viewModel = new SettingViewModel();
            viewModel.NameStore = "";
            viewModel.NumberPhone = "123456789";
            viewModel.Address = "New Address";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => viewModel.Save_Data_Store.Execute(null));
        }
    }
}
