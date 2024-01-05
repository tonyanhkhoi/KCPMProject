using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace MainProject.HelperClass.Tests
{
    [TestClass]
    public class WindowServiceTests
    {
        [TestMethod]
        public void WindowService_Instance_IsNotNull()
        {
            // Arrange & Act
            var instance = WindowService.Instance;

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void WindowService_Instance_IsSingleton()
        {
            // Arrange & Act
            var instance1 = WindowService.Instance;
            var instance2 = WindowService.Instance;

            // Assert
            Assert.AreSame(instance1, instance2);
        }

      

        // Add more test methods to cover various scenarios and edge cases
    }
}