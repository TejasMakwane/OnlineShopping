using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore1
{
    public class OrderHistory
    {
        [Display(Name = "Order ID")]
        public int ShippingDetailId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Amount")]
        public Nullable<decimal> AmountPaid { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

       
    }
}