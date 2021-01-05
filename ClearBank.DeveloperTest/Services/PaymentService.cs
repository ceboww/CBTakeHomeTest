using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.PaymentValidators;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStore dataStore;
        private readonly IPaymentValidatorFactory paymentValidatorFactory;

        public PaymentService(IDataStore dataStore, IPaymentValidatorFactory paymentValidatorFactory)
        {
            this.dataStore = dataStore;
            this.paymentValidatorFactory = paymentValidatorFactory;
        }
        
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = this.dataStore.GetAccount(request.DebtorAccountNumber);

            var paymentValidator = this.paymentValidatorFactory.GetValidator(request);
            var result = new MakePaymentResult();
            var paymentIsValid = paymentValidator.CheckPaymentValid(request, account);
            result.Success = paymentIsValid;
            
            if (result.Success)
            {
                account.Balance -= request.Amount;
                this.dataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
