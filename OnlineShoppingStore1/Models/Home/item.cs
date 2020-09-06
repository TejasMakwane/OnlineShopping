using OnlineShoppingStore1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore1.Models.Home
{
    public class item
    {
        public T_Product Product { get; set; }

        public int Quantity { get; set; }
    }
}