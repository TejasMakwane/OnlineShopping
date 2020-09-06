using Newtonsoft.Json;
using OnlineShoppingStore1;
using OnlineShoppingStore1.Models;
using OnlineShoppingStore1.Repository;
using OnlineShoppingStore1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OnlineShoppingStore1.Controllers
{
    public class AdminController : Controller
    {
        OnlineShoppingEntities db = new OnlineShoppingEntities();

        public GenericUnitOfWork _unitywrok = new GenericUnitOfWork();

        // GET: Admin
        public ActionResult DashBoard()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

          
            return View();
        }

        public ActionResult Categories()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<T_Category> allCategories = _unitywrok.GetRepositoryInstance<T_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allCategories);
        }

        public ActionResult AddCategories()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return UpdateCategories(0);
        }



        public ActionResult UpdateCategories(int categoryId)
        {
         

            CategoryDetails cd;

            if(categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetails>(JsonConvert.SerializeObject(_unitywrok.GetRepositoryInstance<T_Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetails();
            }
            return View("UpdateCategories", cd);
        }


        public ActionResult EditCategories(int categorytId)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(_unitywrok.GetRepositoryInstance<T_Category>().GetFirstorDefault(categorytId));
        }

        [HttpPost]
        public ActionResult EditCategories(T_Category product)
        {

            _unitywrok.GetRepositoryInstance<T_Category>().Update(product);
            return RedirectToAction("Categories");
        }








        public ActionResult Product()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(_unitywrok.GetRepositoryInstance<T_Product>().GetProduct());
        }


        public ActionResult EditProduct(int productId)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.CategoryList = GetCategory();
            return View(_unitywrok.GetRepositoryInstance<T_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult EditProduct(T_Product product, HttpPostedFileBase file)
        {

            string picture = null;
            if (file != null)
            {
                picture = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), picture);
                file.SaveAs(path);
            }
            product.ProductImage = file != null ? picture : product.ProductImage;
            //if we want to this  file Dont to change soo above line to be used as rafered same code in EditProuct to Starting method



            product.ModifiedDate = DateTime.Now;
            _unitywrok.GetRepositoryInstance<T_Product>().Update(product);
            return RedirectToAction("Product");
        }



        public ActionResult AddProduct()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(T_Product product, HttpPostedFileBase file)
        {
            string picture = null;
            if (file != null)
            {
                picture = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), picture);
                file.SaveAs(path);
            }
            product.ProductImage = picture;


            product.CreatedDate = DateTime.Now;
            _unitywrok.GetRepositoryInstance<T_Product>().Add(product);
            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult AddCategory()
        {

            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        [HttpPost]
        public ActionResult AddCategory(T_Category category)
        {
            _unitywrok.GetRepositoryInstance<T_Category>().Add(category);
              return RedirectToAction("Categories");
        }


        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitywrok.GetRepositoryInstance<T_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public ActionResult ListMessages()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(_unitywrok.GetRepositoryInstance<T_Message>().GetProduct());
        }

        public ActionResult UserList()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(_unitywrok.GetRepositoryInstance<T_Members>().GetProduct());
        }

        public ActionResult RemoveUser(int memberId)
        {


            T_Members mem = db.T_Members.Find(memberId);
            db.T_Members.Remove(mem);
            db.SaveChanges();

            return RedirectToAction("UserList");


        }



        public ActionResult OrderList()
        {
            //var result = (from member in db.T_Members
            //              join shipping in db.T_ShippingDetails on member.MemberId equals shipping.MemberId
            //              select new
            //              {
            //                  member.FirstName,

            //                  shipping.Country,
            //                  shipping.State,
            //                  shipping.City,
            //                  shipping.AmountPaid

            //              }).ToList();




            //return View(result);


            //List<T_Members> li = db.T_Members.ToList();
            //List<T_ShippingDetails> shi = db.T_ShippingDetails.ToList();

            //var result = from l in li
            //             join sh in shi on l.MemberId equals sh.MemberId into table1
            //             from sh in table1.DefaultIfEmpty()
            //             select new JoinClass { getmember = l, getShipping = sh };

            //return View(result);

            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var data = db.Database.SqlQuery<JoinClass>("Orderlist").ToList();

            return View(data);

        }
    }
}