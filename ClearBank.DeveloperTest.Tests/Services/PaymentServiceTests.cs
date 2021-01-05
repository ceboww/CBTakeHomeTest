using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.PaymentValidators;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private PaymentService paymentService;
        private Mock<IPaymentValidatorFactory> mockPaymentValidatorFactory;
        private Mock<IDataStore> mockDataStore;
        private Mock<IPaymentValidator> mockPaymentValidator;
        private MakePaymentRequest makePaymentRequest;
        private Account account;

        [SetUp]
        public void SetUp()
        {
            mockDataStore = new Mock<IDataStore>(MockBehavior.Strict);
            mockPaymentValidatorFactory = new Mock<IPaymentValidatorFactory>(MockBehavior.Strict);
            mockPaymentValidator = new Mock<IPaymentValidator>(MockBehavior.Strict);
            paymentService = new PaymentService(mockDataStore.Object, mockPaymentValidatorFactory.Object);
            
            makePaymentRequest = new MakePaymentRequest
            {
                DebtorAccountNumber = "123456789",
                Amount = 22.22m
            };
            account = new Account
            {
                Balance = 66.66m
            };

            mockDataStore.Setup(store => store.GetAccount(makePaymentRequest.DebtorAccountNumber))
                .Returns(account);
            mockPaymentValidatorFactory.Setup(factory => factory.GetValidator(makePaymentRequest))
                .Returns(this.mockPaymentValidator.Object);
        }
        
        [Test]
        public void MakePayment_Should_ReturnFalseSuccessAndNotUpdateAccount_IfPaymentInvalid()
        {
            mockPaymentValidator.Setup(validator => validator.CheckPaymentValid(makePaymentRequest, account))
                .Returns(false);
            
            var result = paymentService.MakePayment(makePaymentRequest);
            
            Assert.That(result.Success, Is.False);
        }
        
        [Test]
        public void MakePayment_Should_ReturnTrueSuccessAndUpdateAccount_IfPaymentInvalid()
        {
            mockPaymentValidator.Setup(validator => validator.CheckPaymentValid(makePaymentRequest, account))
                .Returns(true);
            mockDataStore.Setup(store => store.UpdateAccount(account));
            
            var result = paymentService.MakePayment(makePaymentRequest);
            
            Assert.That(result.Success, Is.True);
            Assert.That(account.Balance, Is.EqualTo(44.44m));
        }

        [TearDown]
        public void TearDown()
        {
            mockDataStore.VerifyAll();
            mockPaymentValidatorFactory.VerifyAll();
        }
    }
}