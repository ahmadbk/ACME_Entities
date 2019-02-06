using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class ContractTest
    {
        [TestMethod]
        public void Contract_ValidContractId_ReturnsContract()
        {
            var contractService = new ContractService();
            Contract contract = contractService.GetById("CONTRACTID");

            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(contract.ExpirationDate > DateTime.Now);
            Assert.AreEqual("CONTRACTID", contract.ContractId);
        }
    }
}
