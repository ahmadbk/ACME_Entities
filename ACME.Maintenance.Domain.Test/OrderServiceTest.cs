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
        private OrderService _orderService;
        private IPartServiceRepository _partServiceRepository;
        private PartService _partService;

        private const string ValidContractId = "CONTRACTID";
        private const string ExpiredContractId = "EXPIREDCONTRACTID";

        private const string ValidPartId = "VALIDPARTID";
        private const string InvalidPartId = "INVALIDPARTID";
        private const double ValidPartPrice = 50.0;

        [TestInitialize]
        public void Initialize()
        {
            // All the setup needs to happen here
            // Even the auto mapping needs to be setup here
            // Initialize serves as the "composition route"

            _contractRepository = A.Fake<IContractRepository>();
            _contractService = new ContractService(_contractRepository);

            _partServiceRepository = A.Fake<IPartServiceRepository>();
            _partService = new PartService(_partServiceRepository);

            _orderService = new OrderService();

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

            A.CallTo(() => _partServiceRepository.GetById(ValidPartId))
                .Returns(new PartDto
                {
                    PartId = ValidPartId,
                    Price = ValidPartPrice
                });

            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<ContractDto, Contract>();
                    cfg.CreateMap<PartDto, Part>();
                });
        }


        [TestMethod]
        public void CreateOrder_ValidContract_CreatesNewOrder()
        {
            //Arrange
            var contract = _contractService.GetById(ValidContractId);

            //Act
            var newOrder = _orderService.CreateOrder(contract);

            //Assert
            Assert.IsInstanceOfType(newOrder, typeof(Order));

            Guid guidOut;
            Assert.IsTrue(Guid.TryParse(newOrder.OrderId, out guidOut));
            //Assert.AreEqual(newOrder.OrderId, OrderId);
            Assert.AreEqual(newOrder.Status, OrderStatus.New);
            Assert.IsInstanceOfType(newOrder.Items, typeof(IReadOnlyList<OrderItem>));

        }

        [TestMethod,ExpectedException(typeof(ExpiredContractException))]
        public void CreateOrder_ExpiredContract_ThrowsException()
        {
            //Arrange
            var contract = _contractService.GetById(ExpiredContractId);

            //Act
            var newOrder = _orderService.CreateOrder(contract);

            //Assert
        }


        
        [TestMethod]
        public void CreateOrderItem_validPart_addsOrderItem()
        {
            //Arrange
            var contract = _contractService.GetById(ValidContractId);
            var order = _orderService.CreateOrder(contract);

            var part = _partService.GetById(ValidPartId);
            var quantity = 1;

            //Act
            var orderItem = _orderService.CreateOrderItem(part, quantity);

            //Assert
            Assert.AreEqual(orderItem.Part, part);
            Assert.AreEqual(orderItem.Quantity, quantity);
            Assert.AreEqual(orderItem.Price, ValidPartPrice);
            Assert.AreEqual(orderItem.LineTotal, quantity * ValidPartPrice);
        }
    }
}
