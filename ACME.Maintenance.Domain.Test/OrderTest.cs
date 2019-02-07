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

        [TestMethod]
        public void Items_ContainingValidOrderItems_CanBeIteratedOver()
        {
            var orderItem1 = new OrderItem()
            {
                Part = _part,
                Price = _part.Price,
                Quantity = 1,
                LineTotal = _part.Price * 1
            };

            var orderItem2 = new OrderItem()
            {
                Part = _part,
                Price = _part.Price,
                Quantity = 3,
                LineTotal = _part.Price * 3
            };

            var orderItem3 = new OrderItem()
            {
                Part = _part,
                Price = _part.Price,
                Quantity = 2,
                LineTotal = _part.Price * 2
            };

            _order.AddOrderItem(orderItem1);
            _order.AddOrderItem(orderItem2);
            _order.AddOrderItem(orderItem3);

            //Act
            foreach (var item in _order.Items)
            {
                Assert.IsInstanceOfType(item, typeof(OrderItem));
                Assert.IsInstanceOfType(item.Part, typeof(Part));
                Assert.IsTrue(item.Price > 0);
                Assert.IsTrue(item.Quantity > 0);
                Assert.IsTrue(item.LineTotal > 0);

            }

            Assert.AreEqual(_order.Items.Count, 3);
            Assert.AreEqual(_order.Subtotal, 300.0);
            Assert.AreEqual(_order.OrderItemCount, 6);
        }

        [TestMethod]
        public void Items_ContainingNoOrders_DoesNotThrowException()
        {
            //Act
            foreach (var item in _order.Items)
            {
                Assert.Fail("There should never be any items in the items collection");
            }

            //Assert
            Assert.AreEqual(_order.Items.Count, 0);
            Assert.AreEqual(_order.Subtotal, 0);
            Assert.AreEqual(_order.OrderItemCount, 0);

        }
    }
}
