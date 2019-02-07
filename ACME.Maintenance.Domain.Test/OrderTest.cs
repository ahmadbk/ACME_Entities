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

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void AddOrderItem_ValidOrderItem_AddsOrderItem()
        {
            //Arrange
            var orderService = new OrderService();

            var contract = new Contract
            {
                ContractId = ValidContractId,
                ExpirationDate = DateTime.Now
            };

            var order = orderService.CreateOrder(contract);

            var part = new Part()
            {
                PartId = ValidPartId,
                Price = ValidPartPrice
            };

            var orderItem = new OrderItem()
            {
                Part = part,
                Price = part.Price,
                Quantity = 1,
                LineTotal = part.Price * 1
            };


            //Act
            order.AddOrderItem(orderItem);

            //Assert
            Assert.AreEqual(order.OrderItemCount,1);
            Assert.AreEqual(order.Subtotal, ValidPartPrice);
        }
    }
}
