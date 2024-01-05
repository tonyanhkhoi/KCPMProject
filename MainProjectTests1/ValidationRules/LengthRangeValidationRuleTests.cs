using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.ValidationRules;

namespace MainProjectTests.ValidationRules
{
    [TestClass]
    public class LengthRangeValidationRuleTests
    {
        [TestMethod]
        public void Validate_ValidInput_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new LengthRangeValidationRule
            {
                Min = 2,
                Max = 5,
                ErrorMessage = "Invalid length range"
            };

            // Act
            var result = validationRule.Validate("abc", null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }

        [TestMethod]
        public void Validate_LengthBelowMin_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new LengthRangeValidationRule
            {
                Min = 2,
                Max = 5,
                ErrorMessage = "Invalid length range"
            };

            // Act
            var result = validationRule.Validate("a", null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid length range", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_LengthAboveMax_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new LengthRangeValidationRule
            {
                Min = 2,
                Max = 5,
                ErrorMessage = "Invalid length range"
            };

            // Act
            var result = validationRule.Validate("abcdef", null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid length range", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_NullInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new LengthRangeValidationRule
            {
                Min = 2,
                Max = 5,
                ErrorMessage = "Invalid length range"
            };

            // Act
            var result = validationRule.Validate(null, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid length range", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_EmptyString_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new LengthRangeValidationRule
            {
                Min = 0,
                Max = 5,
                ErrorMessage = "Invalid length range"
            };

            // Act
            var result = validationRule.Validate(string.Empty, null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }
    }
}
