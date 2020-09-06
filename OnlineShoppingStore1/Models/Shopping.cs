using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore1.Models
{
    public class ShippingDetails
    {
        public int ShippingDetailId { get; set; }

        [Required]
        public Nullable<int> MemberId { get; set; }

        [Required(ErrorMessage ="Please Enter Your Address...!")]
        [Display(Name ="Address")]
        public string Adress { get; set; }


        [Required(ErrorMessage = "Please Enter Your City...!")]
        [Display(Name = "City")]
        public string City { get; set; }


        [Required(ErrorMessage = "Please Enter Your State...!")]
        [Display(Name = "State")]
        public string State { get; set; }


        [Required(ErrorMessage = "Please Enter Your Country...!")]
        [Display(Name = "Country")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Please Enter Your Zipcode...!")]
        [Display(Name = "Zipcode")]
        public string ZipCode { get; set; }

        public Nullable<int> OrderId { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }

        [Required(ErrorMessage = "Please Enter Your Pymenttype...!")]
        [Display(Name = "Payment Method")]
        public string PaymentType { get; set; }
    }
}