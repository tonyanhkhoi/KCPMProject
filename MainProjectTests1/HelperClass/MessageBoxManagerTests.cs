using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.HelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.HelperClass.Tests
{
        [TestClass]
    public class MessageBoxManagerTests
    {
        [TestMethod]
        public void MessageBoxManager_Register_Unregister_Success()
        {
            // Arrange & Act
            MessageBoxManager.Register();
            MessageBoxManager.Unregister();

            // Assert: No exception should be thrown
        }

        [TestMethod]
        public void MessageBoxManager_RegisterTwice_ThrowsException()
        {
            // Arrange
            MessageBoxManager.Register();

            // Act & Assert
            Assert.ThrowsException<NotSupportedException>(() => MessageBoxManager.Register());

            // Cleanup
            MessageBoxManager.Unregister();
        }


        [TestMethod]
        public void MessageBoxManager_ConstantTextValues_AreNotNullOrEmpty()
        {
            // Arrange & Act & Assert
            Assert.IsFalse(string.IsNullOrEmpty(MessageBoxManager.OK));
            Assert.IsFalse(string.IsNullOrEmpty(MessageBoxManager.Cancel));
            Assert.IsFalse(string.IsNullOrEmpty(MessageBoxManager.Abort));
            Assert.IsFalse(string.IsNullOrEmpty(MessageBoxManager.Retry));
            Assert.IsFalse(string.IsNullOrEmpty(MessageBoxManager.Ignore));
            Assert.IsFalse(string.IsNullOrEmpty(MessageBoxManager.Yes));
            Assert.IsFalse(string.IsNullOrEmpty(MessageBoxManager.No));
        }

     
        // Add more test methods to cover various scenarios and edge cases
    }
}