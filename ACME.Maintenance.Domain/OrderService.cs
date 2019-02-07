﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Maintenance.Domain.Enums;

namespace ACME.Maintenance.Domain
{
    public class OrderService
    {
        public Order CreateOrder(Contract contract)
        {
            var order = new Order { 
                OrderId = Guid.NewGuid().ToString(), 
                Status = OrderStatus.New 
            };

            return order;
        } 
    }
}
