using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidators
{
    public class FasterPaymentsPaymentValidator : IPaymentValidator
    {
        public PaymentScheme PaymentScheme { get; } = PaymentScheme.FasterPayments;
        
        public bool CheckPaymentValid(MakePaymentRequest request, Account account)
        {
            return account?.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) == true 
            && account.Balance >= request.Amount;
        }
    }
}