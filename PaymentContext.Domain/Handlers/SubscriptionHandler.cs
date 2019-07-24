using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailServicer;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailServicer = emailService;
        }
        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            // Fail fast validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
              AddNotification("Document", "Este CPF já está em uso.");

            // Verificar se e-mail já está cadastrado
            if(_repository.EmailExists(command.Email))
              AddNotification("Email", "Este e-mail já está em uso");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            //var address = new Address(command.Address);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate,
                command.Total, 
                command.TotalPid, 
                command.Payer, 
                new Document(command.PayerDocument, 
                command.PayerDocumentType),
                command.Address, email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validaçoes
            AddNotifications(name, document, email, student, subscription, payment);

            // Checar as validações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");

            // Salvar as Informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailServicer.Send(student.Name.ToString(), student.Email.Address, "Bem vindo.", "Sua assinatura foi criada");

           return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handler(CreatePayPalSubscriptionCommand command)
        {
            // Verificar se documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
              AddNotification("Document", "Este CPF já está em uso.");

            // Verificar se e-mail já está cadastrado
            if(_repository.EmailExists(command.Email))
              AddNotification("Email", "Este e-mail já está em uso");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            //var address = new Address(command.Address);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode, 
                command.PaidDate, 
                command.ExpireDate,
                command.Total, 
                command.TotalPid, 
                command.Payer, 
                new Document(command.PayerDocument, 
                command.PayerDocumentType),
                command.Address, email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validaçoes
            AddNotifications(name, document, email, student, subscription, payment);

            // Checar as validações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");

            // Salvar as Informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailServicer.Send(student.Name.ToString(), student.Email.Address, "Bem vindo.", "Sua assinatura foi criada");

           return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}