using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreateBoletoSubscriptionCommand : Notifiable, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
             .Requires()
             .HasMinLen(FirstName, 3, "Nome.FirstName", "Nome deve conter pelo menos 3 caracteres")
             .HasMinLen(FirstName, 3, "Nome.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
            );
        }
    }
}