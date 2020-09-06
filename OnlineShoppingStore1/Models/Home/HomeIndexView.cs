using OnlineShoppingStore1.DAL;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using OnlineShoppingStore1.Repository;

namespace OnlineShoppingStore1.Models.Home
{
    public class HomeIndexView
    {
        public GenericUnitOfWork _unitywrok = new GenericUnitOfWork();
        OnlineShoppingEntities context = new OnlineShoppingEntities();
        public IPagedList<T_Product> ListProducts { get; set; }

        public HomeIndexView CreateModel(string search,int pageSize, int? page)
        {

            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@search",search??(object)DBNull.Value)

            };
            IPagedList<T_Product> data = context.Database.SqlQuery<T_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);



            return new HomeIndexView
            {


                ListProducts = data
            };
        }

    }
}