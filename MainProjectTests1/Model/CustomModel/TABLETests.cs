using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.ComponentModel;
using System.Data.Entity;

namespace MainProject.Model.Tests
{
    [TestClass]
    public class TABLETests
    {
        [TestMethod]
        public void CurrentStatus_SetProperty_CallsOnPropertyChanged()
        {
            // Arrange
            var table = new TABLE();
            var mock = new Mock<PropertyChangedEventHandler>();
            table.PropertyChanged += mock.Object;

            // Act
            table.CurrentStatus = "NewStatus";

            // Assert
            mock.Verify(e => e(It.IsAny<object>(), It.IsAny<System.ComponentModel.PropertyChangedEventArgs>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void CurrentStatus_SetProperty_UpdatesStatusProperty()
        {
            // Arrange
            var table = new TABLE();

            // Act
            table.CurrentStatus = "NewStatus";

            // Assert
            Assert.AreEqual("NewStatus", table.CurrentStatus);
        }

        
        [TestMethod]
        public void OnPropertyChanged_StatusTableChanged_UpdatesCurrentStatus()
        {
            // Arrange
            var table = new TABLE();
            var statusTable = new STATUS_TABLE { Status = "NewStatus" };

            // Act
            table.OnPropertyChanged("STATUS_TABLE", null, statusTable);

            // Assert
            Assert.AreEqual("NewStatus", table.CurrentStatus);
        }

        [TestMethod]
        public void ChangeStatusDB_FixStatus_SavesChanges()
        {
            // Arrange
            var table = new TABLE();
            var statusTable = new STATUS_TABLE { Status = "Fix" };
            var mockSet = new Mock<DbSet<TABLE>>();
            var mockContext = new Mock<mainEntities>();

            mockContext.Setup(c => c.TABLEs).Returns(mockSet.Object);
            table.ID = 1;

            // Act
            table.ChangeStatusDB(true);

            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

 

        [TestMethod]
        public void OnPropertyChanged_OtherProperty_DoesNotSaveChanges()
        {
            // Arrange
            var table = new TABLE();
            var mockContext = new Mock<mainEntities>();

            // Act
            table.OnPropertyChanged("OtherProperty", "OldValue", "NewValue");

            // Assert
            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }

        // Add more test methods to cover different scenarios and edge cases.
    }
}