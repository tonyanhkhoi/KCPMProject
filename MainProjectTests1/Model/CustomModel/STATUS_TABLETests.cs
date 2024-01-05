using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MainProject.Model.Tests
{
    [TestClass]
    public class STATUS_TABLETests
    {
      

        [TestMethod]
        public void Instance_GetInstance_ReturnsNonNullInstance()
        {
            // Act
            var result = STATUS_TABLE.Instance;

            // Assert
            Assert.IsNotNull(result);
        }
     
        [TestMethod]
        public void Instance_GetInstance_ReturnsSameInstance()
        {
            // Arrange
            var instance1 = STATUS_TABLE.Instance;

            // Act
            var instance2 = STATUS_TABLE.Instance;

            // Assert
            Assert.AreSame(instance1, instance2);
        }

      
    }

    public class MockMainEntities : mainEntities
    {
        public bool ThrowDatabaseError { get; set; }

        public List<string> StatusList { get; private set; } = new List<string>();

        public void AddStatus(string status)
        {
            StatusList.Add(status);
        }

        public List<string> GetStatusList()
        {
            return StatusList.ToList();
        }

        public override int SaveChanges()
        {
            if (ThrowDatabaseError)
            {
                throw new Exception("Simulated database error");
            }
            return 0;
        }
    }

    // Add more test methods covering different scenarios and edge cases.
}

    public class MockMainEntities : mainEntities
    {
        public static bool NullDatabaseInstance { get; set; }

        public bool ThrowDatabaseError { get; set; }

        public List<string> StatusList { get; private set; } = new List<string>();

        public void AddStatus(string status)
        {
            StatusList.Add(status);
        }

        public List<string> GetStatusList()
        {
            return StatusList.ToList();
        }

        public override int SaveChanges()
        {
            if (ThrowDatabaseError)
            {
                throw new Exception("Simulated database error");
            }
            return 0;
        }

        public static mainEntities GetInstance()
        {
            if (NullDatabaseInstance)
            {
                return null;
            }
            return new MockMainEntities();
        }

    }
    