using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Maintenance.Domain.Enums;

namespace ACME.Maintenance.Domain
{
    public class Order
    {
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public int OrderItemCount { get; set; }
        public double Subtotal { get; set; }

        private List<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public IReadOnlyList<OrderItem> Items
        {
            get { return OrderItems; }
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            this.OrderItemCount = 0;
            this.Subtotal = 0;

            this.OrderItems.Add(orderItem);
            foreach(var item in this.OrderItems)
            {
                this.OrderItemCount += item.Quantity;
                this.Subtotal += item.LineTotal;
            }
        }
    }
}
