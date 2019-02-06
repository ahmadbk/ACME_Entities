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
            var contract = new Contract();
            contract.FindById("CONTRACTID");
            Assert.IsTrue(contract.ExpirationDate > DateTime.Now);

        }
    }
}
