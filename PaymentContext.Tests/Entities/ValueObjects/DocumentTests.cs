using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Rafactor
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }

         [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsInvalid()
        {
            var doc = new Document("07925066000131", Domain.Enums.EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

         [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

         [TestMethod]
         [DataTestMethod]
         [DataRow("51548552054")]
         [DataRow("62583274007")]
         [DataRow("75398612077")]
        public void ShouldReturnSuccessWhenCPFIsInvalid(string cpf)
        {
             var doc = new Document(cpf, Domain.Enums.EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}