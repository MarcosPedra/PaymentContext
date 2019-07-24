using System;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{

    public class PayPalPayment : Payment
    {
        public PayPalPayment(
            string transactionCode,
             DateTime paidDate,
             DateTime expireDate,
             decimal total, decimal totalPid,
              string payer,
              Document document,
              string address,
              Email email) : base(
                  paidDate,
                   expireDate, 
                   total, 
                   totalPid, 
                   payer, 
                   document, 
                   address, 
                   email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }

}