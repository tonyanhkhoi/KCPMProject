using MainProject.ViewModel;
using MainProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MainProject.ViewModel
{

    [TestClass]
    public class ManageProductviewModelTests
    {
        [TestMethod]
        public void AddEditCategory_ValidInput_ShouldAddCategory()
        {
            // Arrange
            var viewModel = new ManageProductviewModel();
            viewModel.NameNewTypeProduct = "NewCategory";

            // Act
            viewModel.AddEditCategory_Command.Execute(null);

            // Assert
            Assert.IsTrue(viewModel.MainVM.ListType.Any(c => c.Type == "NewCategory"));
        }

        [TestMethod]
        public void AddEditCategory_EmptyName_ShouldShowErrorMessage()
        {
            // Arrange
            var viewModel = new ManageProductviewModel();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => viewModel.AddEditCategory_Command.Execute(null));
        }

        [TestMethod]
        public void OpenViewAddCategory_Command_ShouldOpenAddCategoryWindow()
        {
            // Arrange
            var viewModel = new ManageProductviewModel();

            // Act
            viewModel.OpenViewAddCategory_Command.Execute(null);

            // Assert
            // Ensure that the "NewType" window is opened
            Assert.IsTrue(WindowService.Instance.FindWindowbyTag("NewType").Any());
        }

        [TestMethod]
        public void DeleteTypeEditCategory_ValidInput_ShouldDeleteCategory()
        {
            // Arrange
            var viewModel = new ManageProductviewModel();
            viewModel.NameNewTypeProduct = "CategoryToDelete";
            viewModel.AddEditCategory_Command.Execute(null);

            // Act
            viewModel.CurrentTypeInProManager = viewModel.MainVM.ListType.Last();
            viewModel.DeleteTypeEditCategory_Command.Execute(null);

            // Assert
            Assert.IsFalse(viewModel.MainVM.ListType.Any(c => c.Type == "CategoryToDelete"));
        }

        [TestMethod]
        public void OpenViewEditCategory_Command_ShouldOpenEditCategoryWindow()
        {
            // Arrange
            var viewModel = new ManageProductviewModel();

            // Act
            viewModel.OpenViewEditCategory_Command.Execute(null);

            // Assert
            // Ensure that the "EditType" window is opened
            Assert.IsTrue(WindowService.Instance.FindWindowbyTag("EditType").Any());
        }
    }
}