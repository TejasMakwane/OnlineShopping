using OnlineShoppingStore1.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore1
{
    public class JoinClass
    {
        public int MemberId { get; set; }
        
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "EmailId")]
        public string EmailId { get; set; }

        [Display(Name = "Address")]
        public string Adress { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name ="State")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Amount")]
        public Nullable<decimal> AmountPaid { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }





    }
}