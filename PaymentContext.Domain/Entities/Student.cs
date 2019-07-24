using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Adress { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriprtions { get { return _subscriptions.ToArray(); } }
        public void AddSubscription(Subscription subscription)
        {
            var hasSubcriptionActive = false;
            foreach (var sub in Subscriprtions)
            {
               if (sub.Active)
                 hasSubcriptionActive = true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubcriptionActive, "Student.Subscriptions", "Você já possui uma assinatura ativa")
                .IsLowerThan(0, subscription.Payments.Count, "Student.Subscription.Payments", "Esta assinatura não possui pagamentos")

            );

            _subscriptions.Add(subscription);
        }
    }
}