using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class OrderTest
    {
        private const string ValidPartId = "VALIDPARTID";
        private const string ValidContractId = "VALIDCONTRACTID";
        private const double ValidPartPrice = 50.0;

        private OrderService _orderService;
        private Order _order;

        private Contract _contract = new Contract
        {
            ContractId = ValidContractId,
            ExpirationDate = DateTime.Now
        };

        private Part _part = new Part()
        {
            PartId = ValidPartId,
            Price = ValidPartPrice
        };

        [TestInitialize]
        public void Initialize()
        {
            _orderService = new OrderService();
            _order = _orderService.CreateOrder(_contract);

        }

        [TestMethod]
        public void AddOrderItem_ValidOrderItem_AddsOrderItem()
        {
            //Arrange
            var orderItem = new OrderItem()
            {
                Part = _part,
                Price = _part.Price,
                Quantity = 1,
                LineTotal = _part.Price * 1
            };

            //Act
            _order.AddOrderItem(orderItem);

            //Assert
            Assert.AreEqual(_order.OrderItemCount,1);
            Assert.AreEqual(_order.Subtotal, ValidPartPrice);
        }
    }
}
