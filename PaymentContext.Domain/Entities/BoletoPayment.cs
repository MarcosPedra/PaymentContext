using System;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(
            string barCode, 
        string boletoNumber,
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
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }

    }
}