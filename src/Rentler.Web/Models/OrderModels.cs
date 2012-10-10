﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Models
{
    public class OrderCheckoutModel
    {
        public Order Order { get; set; }

        public OrderCheckoutInputModel Input { get; set; }
    }

    public class OrderCheckoutInputModel
    {
        public OrderCheckoutInputModel()
        {
            this.SelectedPaymentMethod = new UserCreditCard()
            {
                Alias = -1,
                ExpirationMonth = 0,
                ExpirationYear = 0,
                CreateDate = DateTime.UtcNow,
                CreatedBy = "web"
            };
        }

        public int OrderId { get; set; }

        public long BuildingId { get; set; }

        public UserCreditCard SelectedPaymentMethod { get; set; }

        public bool SaveCard { get; set; }
    }
}