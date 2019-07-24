using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("66047915051", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
           
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
             var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, "Bat Caverna, n.º 0", _email);

            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenHadActiveSubscription()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

          [TestMethod]
        public void ShouldReturnSuccessWhenNoActiveSubscription()
        {
             var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, "Bat Caverna, n.º 0", _email);
            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}