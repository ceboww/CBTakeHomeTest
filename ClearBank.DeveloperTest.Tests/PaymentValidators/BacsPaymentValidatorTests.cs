using ClearBank.DeveloperTest.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidators
{
    [TestFixture]
    public class BacsPaymentValidatorTests
    {
        [Test]
        public void CheckPaymentValid_False_IfAccountNull()
        {
            var bacsPaymentValidator = new BacsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = null;

            var validationResult = bacsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_IfAllowedSchemesFasterPayments()
        {
            var bacsPaymentValidator = new BacsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments };

            var validationResult = bacsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_IfAllowedSchemesChaps()
        {
            var bacsPaymentValidator = new BacsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps };

            var validationResult = bacsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_True_IfAllowedSchemesBacs()
        {
            var bacsPaymentValidator = new BacsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };

            var validationResult = bacsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.True);
        }
    }
}