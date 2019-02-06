using System;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class ContractServiceTest
    {
        private IContractRepository _contractRepository;
        private ContractService _contractService;

        private const string validContractId = "CONTRACTID";
        private const string expiredContractId = "EXPIREDCONTRACTID";

        [TestInitialize]
        public void Initialize()
        {
            // All the setup needs to happen here
            // Even the auto mapping needs to be setup here
            // Initialize serves as the "composition route"

            _contractRepository = A.Fake<IContractRepository>();
            _contractService = new ContractService(_contractRepository);

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

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<ContractDto, Contract>());

        }

        //Naming Convention: MethodName_StateUnderTest_ExpectedBehaviour
        //Pattern to follow in each unit test: Arrange, Act, Assert

        [TestMethod]
        public void GetById_ValidContractId_ReturnsContract()
        {
            //Arrange - All necessary pre-conditions and inputs 
            
            //Act - Act on the object or method under test
            Contract contract = _contractService.GetById(validContractId);

            //Assert - Test for the expected result
            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(contract.ExpirationDate > DateTime.Now);
            Assert.AreEqual(validContractId, contract.ContractId);
        }

        [TestMethod]
        public void GetById_ExpiredContractId_ReturnsExpiredContract()
        {
            Contract contract = _contractService.GetById(expiredContractId);

            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(DateTime.Now > contract.ExpirationDate);
            Assert.AreEqual(expiredContractId, contract.ContractId);
        }
    }
}
