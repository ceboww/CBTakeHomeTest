using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidators
{
    public interface IPaymentValidator 
    {
        PaymentScheme PaymentScheme { get; }
        bool CheckPaymentValid(MakePaymentRequest request, Account account);
    }
}