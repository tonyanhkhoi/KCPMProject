using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.ValidationRules;

namespace MainProjectTests.ValidationRules
{
    [TestClass]
    public class NotNullableValidationRuleTests
    {
        [TestMethod]
        public void Validate_ValidInput_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new NotNullableValidationRule
            {
                ErrorMessage = "This field is required"
            };

            // Act
            var result = validationRule.Validate("ValidInput", null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }

        [TestMethod]
        public void Validate_NullInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new NotNullableValidationRule
            {
                ErrorMessage = "This field is required"
            };

            // Act
            var result = validationRule.Validate(null, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("This field is required", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_EmptyInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new NotNullableValidationRule
            {
                ErrorMessage = "This field is required"
            };

            // Act
            var result = validationRule.Validate(string.Empty, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("This field is required", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_WhiteSpaceInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new NotNullableValidationRule
            {
                ErrorMessage = "This field is required"
            };

            // Act
            var result = validationRule.Validate("    ", null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("This field is required", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_InvalidInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new NotNullableValidationRule
            {
                ErrorMessage = "This field is required"
            };

            // Act
            var result = validationRule.Validate("", null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("This field is required", result.ErrorContent);
        }
    }
}
