using System;
using System.Collections.Generic;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Exceptions;
using ACME.Maintenance.Domain.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACME.Maintenance.Domain.Enums;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class OrderServiceTest
    {
        private IContractRepository _contractRepository;
        private ContractService _contractService;

        private const string ValidContractId = "CONTRACTID";
        private const string ExpiredContractId = "EXPIREDCONTRACTID";

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

            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<ContractDto, Contract>());

        }


        [TestMethod]
        public void CreateOrder_ValidContract_CreatesNewOrder()
        {
            //Arrange
            var orderService = new OrderService();
            var contractService = new ContractService(_contractRepository);
            var contract = contractService.GetById(ValidContractId);


            //Act
            var newOrder = orderService.CreateOrder(contract);

            //Assert
            Assert.IsInstanceOfType(newOrder, typeof(Order));

            Guid guidOut;
            Assert.IsTrue(Guid.TryParse(newOrder.OrderId, out guidOut));
            //Assert.AreEqual(newOrder.OrderId, OrderId);
            Assert.AreEqual(newOrder.Status, OrderStatus.New);
            Assert.IsInstanceOfType(newOrder.OrderItems, typeof(List<OrderItem>));

        }

        [TestMethod,ExpectedException(typeof(ExpiredContractException))]
        public void CreateOrder_ExpiredContract_ThrowsException()
        {
            //Arrange
            var orderService = new OrderService();
            var contractService = new ContractService(_contractRepository);
            var contract = contractService.GetById(ExpiredContractId);


            //Act
            var newOrder = orderService.CreateOrder(contract);

            //Assert
        }
    }
}
