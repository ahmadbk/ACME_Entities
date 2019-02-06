using System;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Interfaces;
using ACME.Maintenance.Domain.Exceptions;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class ContractServiceTest
    {
        private IContractRepository _contractRepository;
        private ContractService _contractService;

        private const string ValidContractId = "CONTRACTID";
        private const string ExpiredContractId = "EXPIREDCONTRACTID";
        private const string InvalidContractId = "INVALIDCONTRACTID";

        [TestInitialize]
        public void Initialize()
        {
            // All the setup needs to happen here
            // Even the auto mapping needs to be setup here
            // Initialize serves as the "composition route"

            _contractRepository = A.Fake<IContractRepository>();
            _contractService = new ContractService(_contractRepository);

            A.CallTo(() => _contractRepository.GetById(ValidContractId))
                .Returns(new ContractDto
                {
                    ContractId = ValidContractId,
                    ExpirationDate = DateTime.Now.AddDays(1)
                });

            A.CallTo(() => _contractRepository.GetById(ExpiredContractId))
                .Returns(new ContractDto
                {
                    ContractId = ExpiredContractId,
                    ExpirationDate = DateTime.Now.AddDays(-1)
                });

            A.CallTo(() => _contractRepository.GetById(InvalidContractId))
                .Throws<ContractNotFoundException>();

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<ContractDto, Contract>());

        }

        //Naming Convention: MethodName_StateUnderTest_ExpectedBehaviour
        //Pattern to follow in each unit test: Arrange, Act, Assert

        [TestMethod]
        public void GetById_ValidContractId_ReturnsContract()
        {
            //Arrange - All necessary pre-conditions and inputs 
            
            //Act - Act on the object or method under test
            Contract contract = _contractService.GetById(ValidContractId);

            //Assert - Test for the expected result
            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(contract.ExpirationDate > DateTime.Now);
            Assert.AreEqual(ValidContractId, contract.ContractId);
        }

        [TestMethod]
        public void GetById_ExpiredContractId_ReturnsExpiredContract()
        {
            Contract contract = _contractService.GetById(ExpiredContractId);

            Assert.IsInstanceOfType(contract, typeof(Contract));
            Assert.IsTrue(DateTime.Now > contract.ExpirationDate);
            Assert.AreEqual(ExpiredContractId, contract.ContractId);
        }

        [TestMethod,ExpectedException(typeof(ContractNotFoundException))]
        public void GetById_InvalidContractId_ThrowsException()
        {
            //Act
            var contract = _contractService.GetById(InvalidContractId);
        }
    }
}
