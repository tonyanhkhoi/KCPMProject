using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.ValidationRules;

namespace MainProjectTests.ValidationRules
{
    [TestClass]
    public class ExistVoucherValidationRuleTests
    {
        [TestMethod]
        public void Validate_ValidCode_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new ExistVoucherValidationRule
            {
                ErrorMsg = "Invalid voucher code"
            };

            // Act
            var result = validationRule.Validate("ValidCode", null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }

        [TestMethod]
        public void Validate_InvalidCode_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new ExistVoucherValidationRule
            {
                ErrorMsg = "Invalid voucher code"
            };

            // Act
            var result = validationRule.Validate("Invalid@Code", null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreNotEqual("Invalid voucher code", result.ErrorContent);
        }

        [TestMethod]
        public void Validate_EmptyString_ShouldReturnValidResult()
        {
            // Arrange
            var validationRule = new ExistVoucherValidationRule
            {
                ErrorMsg = "Invalid voucher code"
            };

            // Act
            var result = validationRule.Validate(string.Empty, null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }

        [TestMethod]
        public void Validate_WhitespaceCode_ShouldReturnInvalidResult()
        {
            // Arrange
            var validationRule = new ExistVoucherValidationRule
            {
                ErrorMsg = "Invalid voucher code"
            };

            // Act
            var result = validationRule.Validate("    ", null);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreNotEqual("Invalid voucher code", result.ErrorContent);
        }
    }
}
