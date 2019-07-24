
using System;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            string cardHolderName,
            string cardNumber,
            string lastTransactionNumber,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPid,
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
            this.cardHolderName = cardHolderName;
            this.cardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string cardHolderName { get; private set; }
        public string cardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }

    }
}