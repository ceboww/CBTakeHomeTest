using ClearBank.DeveloperTest.Types;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClearBank.DeveloperTest.PaymentValidators
{
    public interface IPaymentValidatorFactory 
    {
        IPaymentValidator GetValidator(MakePaymentRequest request);
    }
    
    public class PaymentValidatorFactory : IPaymentValidatorFactory
    {
        private readonly List<IPaymentValidator> PaymentValidators;

        public PaymentValidatorFactory(List<IPaymentValidator> PaymentValidators)
        {
            this.PaymentValidators = PaymentValidators;
        }
        
        public IPaymentValidator GetValidator(MakePaymentRequest request)
        {
            var paymentValidator = this.PaymentValidators.SingleOrDefault(PaymentValidator => PaymentValidator.PaymentScheme == request.PaymentScheme);
            
            if(paymentValidator == null)
            {
                throw new InvalidOperationException("Payment scheme provided not supported.");
            }

            return paymentValidator;
        }
    }
}

    