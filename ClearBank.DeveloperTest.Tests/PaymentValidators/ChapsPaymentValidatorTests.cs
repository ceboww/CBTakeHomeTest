using ClearBank.DeveloperTest.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidators
{
    [TestFixture]
    public class ChapsPaymentValidatorTests
    {
        [Test]
        public void CheckPaymentValid_False_IfAccountNull()
        {
            var chapsPaymentValidator = new ChapsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = null;

            var validationResult = chapsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_IfAllowedSchemesFasterPayments()
        {
            var chapsPaymentValidator = new ChapsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments };

            var validationResult = chapsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_IfAllowedSchemesBacs()
        {
            var chapsPaymentValidator = new ChapsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };

            var validationResult = chapsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_AndAccountStatusIsDisabled()
        {
            var chapsPaymentValidator = new ChapsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled
            };

            var validationResult = chapsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_False_AndAccountStatusIsInboundPaymentsOnly()
        {
            var chapsPaymentValidator = new ChapsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.InboundPaymentsOnly
            };

            var validationResult = chapsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.False);
        }
        
        [Test]
        public void CheckPaymentValid_True_IfAllowedSchemesChapsAndAccountStatusIsLive()
        {
            var chapsPaymentValidator = new ChapsPaymentValidator();

            var request = new MakePaymentRequest();
            Account account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            var validationResult = chapsPaymentValidator.CheckPaymentValid(request, account);

            Assert.That(validationResult, Is.True);
        }
    }
}