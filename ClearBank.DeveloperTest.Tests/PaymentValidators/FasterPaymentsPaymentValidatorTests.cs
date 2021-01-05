using ClearBank.DeveloperTest.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidators
{
    [TestFixture]
    public class FasterPaymentsPaymentValidatorTests
    {
        [Test]
        public void CheckPaymentValid_False_IfAccountNull()
        {
            var fasterPaymentsPaymentValidator = new FasterPaymentsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = null;

            var validationResult = fasterPaymentsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_AllowedSchemesBacs()
        {
            var fasterPaymentsPaymentValidator = new FasterPaymentsPaymentValidator();

            var request = new MakePaymentRequest { Amount = 66.99m };
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, 
                Balance = 66.99m 
            };

            var validationResult = fasterPaymentsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_AllowedSchemesChaps()
        {
            var fasterPaymentsPaymentValidator = new FasterPaymentsPaymentValidator();

            var request = new MakePaymentRequest { Amount = 66.99m };
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, 
                Balance = 66.99m 
            };

            var validationResult = fasterPaymentsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_IfInsufficientBalance()
        {
            var fasterPaymentsPaymentValidator = new FasterPaymentsPaymentValidator();

            var request = new MakePaymentRequest { Amount = 66.99m };
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, 
                Balance = 66.98m 
            };

            var validationResult = fasterPaymentsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_True_IfAllowedSchemesFasterPaymentsAndExactAmountInAccountBalance()
        {
            var fasterPaymentsPaymentValidator = new FasterPaymentsPaymentValidator();

            var request = new MakePaymentRequest { Amount = 66.99m };
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, 
                Balance = 66.99m 
            };

            var validationResult = fasterPaymentsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.True);
        }
        
        [Test]
        public void CheckPaymentValid_True_IfAllowedSchemesFasterPaymentsAndExcessAmountInAccountBalance()
        {
            var fasterPaymentsPaymentValidator = new FasterPaymentsPaymentValidator();

            var request = new MakePaymentRequest { Amount = 66.99m };
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, 
                Balance = 67.00m 
            };

            var validationResult = fasterPaymentsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.True);
        }
    }
}