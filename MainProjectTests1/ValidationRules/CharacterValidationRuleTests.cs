using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.ValidationRules;

namespace MainProjectTests.ValidationRules
{
    [TestClass]
    public class CharacterValidationRuleTests
    {
        [TestMethod]
        public void Validate_ValidCharacters_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new CharacterValidationRule
            {
                ValidCharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                ErrorMessage = "Invalid characters"
            };

            // Act
            var result = validationRule.Validate("ValidInput", null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }

        [TestMethod]
        public void Validate_InvalidCharacters_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new CharacterValidationRule
            {
                ValidCharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                ErrorMessage = "Invalid characters"
            };

            // Act
            var result = validationRule.Validate("Invalid@Input", null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid characters", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_NullInput_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new CharacterValidationRule
            {
                ValidCharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                ErrorMessage = "Invalid characters"
            };

            // Act
            var result = validationRule.Validate(null, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.ErrorContent);
        }

        [TestMethod]
        public void Validate_NonStringInput_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new CharacterValidationRule
            {
                ValidCharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                ErrorMessage = "Invalid characters"
            };

            // Act
            var result = validationRule.Validate(123, null);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("This type cannot be check : System.Int32", result.ErrorContent);
        }
    }
}
