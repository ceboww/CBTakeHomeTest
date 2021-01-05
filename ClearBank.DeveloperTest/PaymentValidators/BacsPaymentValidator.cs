using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidators
{
    public class BacsPaymentValidator : IPaymentValidator
    {
        public PaymentScheme PaymentScheme { get; } = PaymentScheme.Bacs;
        
        public bool CheckPaymentValid(MakePaymentRequest request, Account account)
        {
            return account?.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs) == true;
        }
    }
}