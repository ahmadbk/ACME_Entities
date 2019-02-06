using System;
using ACME.Maintenance.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using ACME.Maintenance.Domain.DTO;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class ContractTest
    {
        private IContractRepository _contractRepository;

        private const string validContractId = "CONTRACTID";
        private const string expiredContractId = "EXPIREDCONTRACTID";

        [TestInitialize]
        public void Initialize()
        {
            _contractRepository = A.Fake<IContractRepository>();

            A.CallTo(() => _contractRepository.GetById(validContractId))
                .Returns(new ContractDto
                {
                    ContractId = validContractId,
                    ExpirationDate = DateTime.Now.AddDays(1)
                });

            A.CallTo(() => _contractRepository.GetById(expiredContractId))
                .Returns(new ContractDto
                {
                    ContractId = expiredContractId,
                    ExpirationDate = DateTime.Now.AddDays(-1)
                });
        }

        [TestMethod]
        public void Contract_ValidContractId_ReturnsContract()
        {
            //var contractRepository = new ContractRepository();
            var contractService = new ContractService(_contractRepository);
            Contract contract = contractService.GetById(validContractId);

            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(contract.ExpirationDate > DateTime.Now);
            Assert.AreEqual(validContractId, contract.ContractId);
        }

        [TestMethod]
        public void Contract_ExpiredContractId_ReturnsExpiredContract()
        {
            //var contractRepository = new ContractRepository();
            var contractService = new ContractService(_contractRepository);
            Contract contract = contractService.GetById(expiredContractId);

            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(DateTime.Now > contract.ExpirationDate);
            Assert.AreEqual(expiredContractId, contract.ContractId);
        }
    }
}
