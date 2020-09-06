using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore1.Models
{
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category Name Required..!")]
        [StringLength(100,ErrorMessage ="Minimum 3 And Minimum 5 And Maximum 100 Characters Are Allow...!",MinimumLength =3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }

    public class ProductDetails
    {

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name Required...!")]
        [StringLength(100, ErrorMessage = "Minimum 3 And Minimum 5 And Maximum 100 Characters Are Allow...!", MinimumLength = 3)]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }


        [Required]
        [Range(1, 50)]
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Required(ErrorMessage = "Description is Required...!")]
        [Display(Name = "Description")]
        public Nullable<System.DateTime> Description { get; set; }

        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }

        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quentity...!")]
        [Display(Name = "Quantity")]
        public Nullable<int> Quantity { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "200000", ErrorMessage = "Invalid Price....!")]
        [Display(Name = "Price")]
        public Nullable<decimal> Price { get; set; }
        public SelectList Categories{get; set; }  //Nnater Add kela hee // and use kele hee  using System.Web.Mvc;
    }
}