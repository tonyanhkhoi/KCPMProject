using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.ValidationRules;

namespace MainProjectTests.ValidationRules
{
    [TestClass]
    public class RangeNumberValidationRuleTests
    {
        [TestMethod]
        public void Validate_ValidInputWithinRange_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new RangeNumberValidationRule
            {
                Min = 1,
                Max = 100,
                ErrorMsg = "Value must be between 1 and 100"
            };

            // Act
            var result = validationRule.Validate(50, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.ErrorContent);
        }

        [TestMethod]
        public void Validate_InputBelowMin_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new RangeNumberValidationRule
            {
                Min = 1,
                Max = 100,
                ErrorMsg = "Value must be between 1 and 100"
            };

            // Act
            var result = validationRule.Validate(0, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Value must be between 1 and 100", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_InputAboveMax_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new RangeNumberValidationRule
            {
                Min = 1,
                Max = 100,
                ErrorMsg = "Value must be between 1 and 100"
            };

            // Act
            var result = validationRule.Validate(150, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Value must be between 1 and 100", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_InvalidInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new RangeNumberValidationRule
            {
                Min = 1,
                Max = 100,
                ErrorMsg = "Value must be between 1 and 100"
            };

            // Act
            var result = validationRule.Validate("invalid", null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("ERROR: Object is NULL or invalid type", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_NullInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new RangeNumberValidationRule
            {
                Min = 1,
                Max = 100,
                ErrorMsg = "Value must be between 1 and 100"
            };

            // Act
            var result = validationRule.Validate(null, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("ERROR: Object is NULL or invalid type", result.ErrorContent);
        }
    }
}
