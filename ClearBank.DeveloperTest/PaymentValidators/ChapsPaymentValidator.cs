using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidators
{
    public class ChapsPaymentValidator : IPaymentValidator
    {
        public PaymentScheme PaymentScheme { get; } = PaymentScheme.Chaps;
        
        public bool CheckPaymentValid(MakePaymentRequest request, Account account)
        {
           return account?.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) == true 
            && account.Status == AccountStatus.Live;
        }
    }
}