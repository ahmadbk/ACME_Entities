using System;
using ACME.Maintenance.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class ContractTest
    {
        [TestMethod]
        public void Contract_ValidContractId_ReturnsContract()
        {
            var contractRepository = new ContractRepository();
            var contractService = new ContractService(contractRepository);
            Contract contract = contractService.GetById("CONTRACTID");

            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(contract.ExpirationDate > DateTime.Now);
            Assert.AreEqual("CONTRACTID", contract.ContractId);
        }
    }
}
