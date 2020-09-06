using OnlineShoppingStore1.Models.Home;
using OnlineShoppingStore1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using System.Text;
using System.Net.Mail;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using OnlineShoppingStore1.Repository;

namespace OnlineShoppingStore1.Controllers
{
    public class HomeController : Controller
    {
        OnlineShoppingEntities dalobj = new OnlineShoppingEntities();


        public GenericUnitOfWork _unitywrok = new GenericUnitOfWork();


        public ActionResult Index(string search, int? page)
        {
            if (Session["uid"] == null)
            {
                return Redirect("Login");
            }
            else
            {
                HomeIndexView model = new HomeIndexView();
                return View(model.CreateModel(search, 8, page));
            }

            

        }

        public ActionResult Index1()
        {


            return View();
        }






        [HttpGet]
        public ActionResult Login()
        {


            return View();
        }


        [HttpPost]
        public ActionResult Login(T_Members m)
        {
            T_Members mi = dalobj.T_Members.Where(x => x.EmailId == m.EmailId && x.Password == m.Password).SingleOrDefault();

           

            if (mi != null)
            {

                Session["uid"] = mi.MemberId;


         
                Session["name"] = mi.FirstName;

                if (mi.EmailId == "khushi@gmail.com" && mi.Password == "khushi")
                {
                    return RedirectToAction("DashBoard", "Admin");
                }
                else
                {

                    return RedirectToAction("Index");
                }


            }
            else
            {
                ViewBag.error = "Invalid Email Or Password......!";
            }
            return View();

        }

        
        public ActionResult ChangePassword(int memberId)
        {

            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View(_unitywrok.GetRepositoryInstance<T_Members>().GetFirstorDefault(memberId));
        }


        [HttpPost]
        public ActionResult ChangePassword(T_Members members)
        {

            _unitywrok.GetRepositoryInstance<T_Members>().Update(members);
            return RedirectToAction("index");
        }




        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index1");

        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Register(T_Members member)
        //{
        //    member.CreatedDate = DateTime.Now;
        //    member.ModifiedDate = DateTime.Now;

        //    _unitywrok.GetRepositoryInstance<T_Members>().Add(member);

        //    return RedirectToAction("Login");
        //}

        [HttpPost]
        public ActionResult Register(T_Members m)
        {

            try
            {
                List<T_Members> li = dalobj.T_Members.ToList();

                if (ModelState.IsValid)
                {
                    var ExistsLastName = dalobj.T_Members.Any(x => x.LastName == m.LastName);
                    var ExistsEmail = dalobj.T_Members.Any(x => x.EmailId == m.EmailId);
                    if (ExistsLastName)
                    {
                        ModelState.AddModelError("LastName", "Last Name Already Exists");
                        return View(m);
                    }

                    else if (ExistsEmail)
                    {
                        ModelState.AddModelError("EmailId", "Email Already Exists");
                        return View(m);
                    }



                    T_Members member = new T_Members();
                    member.FirstName = m.FirstName;
                    member.LastName = m.LastName;
                    member.EmailId = m.EmailId;
                    member.Password = m.Password;
                    member.IsActive = m.IsActive;
                    member.IsDelete = m.IsDelete;
                    member.CreatedDate = DateTime.Now;
                    member.ModifiedDate = DateTime.Now;

                    dalobj.T_Members.Add(member);
                    dalobj.SaveChanges();
                }

                return RedirectToAction("Login");





            }

            catch (Exception)
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult OrderHistory()
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
                   int userid =(int) Session["uid"];
            //var data = dalobj.Database.SqlQuery<OrderHistory>("OrderHistory", @userid).ToList();
            var data = dalobj.Database.SqlQuery<OrderHistory>("OrderHistory @userid",new SqlParameter("userid",userid)).ToList();

            return View(data);
           
        }


        //public JsonResult Register(T_Members mem)
        //{
        //    dalobj.T_Members.Add(mem);
        //    dalobj.SaveChanges();
        //    BuildEmailTemplate(mem.MemberId);
        //    return Json("Regisration sucessfully", JsonRequestBehavior.AllowGet);
        //}

        //public void BuildEmailTemplate(int Id)
        //{
        //    string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");


        //}
        //public void BuildEmailTemplate(string subjectText,string bodyText, string sendTo)
        //{
        //    string from, to, bcc, cc, subject, body;
        //    from = "tejasmakwane1997@gmail.com";
        //    to = sendTo.Trim();
        //    bcc = "";
        //    cc = "";
        //    subject = subjectText;
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(bodyText);
        //    body = sb.ToString();
        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress(from);
        //    mail.To.Add(new MailAddress(to));
        //    if(!string.IsNullOrEmpty(bcc))
        //    {
        //        mail.Bcc.Add(new MailAddress(bcc));

        //    }
        //    if (!string.IsNullOrEmpty(bcc))
        //    {
        //        mail.CC.Add(new MailAddress(cc));

        //    }
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.IsBodyHtml = true;
        //    SendEmail(mail);


        //}

        //public void SendEmail(MailMessage mail)
        //{
        //    SmtpClient client = new SmtpClient();
        //    client.Host = "smtp.gmail.com";
        //    client.Port = 587;
        //    client.EnableSsl = true;
        //    client.UseDefaultCredentials = false;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.Credentials = new System.Net.NetworkCredential("tejasmakwane1997@gmail.com", "tejas12345@#$diksha");
        //    try
        //    {

        //        client.Send(mail);

        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;

        //    }


        //}

        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                var cart = new List<item>();

                var product = dalobj.T_Product.Find(productId);
                cart.Add(new item()
                {
                    Product = product,
                    Quantity = 1
                });

                Session["cart"] = cart;
            }
            else
            {
                List<item> cart = (List<item>)Session["cart"];
                var count = cart.Count();

                var product = dalobj.T_Product.Find(productId);


                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int periousQuantity = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new item()
                        {
                            Product = product,
                            Quantity = periousQuantity + 1
                        });
                        break;
                    }
                    else
                    {
                        var newrecord = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (newrecord == null)
                        {
                            cart.Add(new item()
                            {
                                Product = product,
                                Quantity = 1
                            });

                        }
                    }
                }

                Session["cart"] = cart;
            }


            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult ProductDetails(int productId)
        {
          
            var productinfo = dalobj.T_Product.Find(productId);
            ViewBag.productid = productinfo.ProductId;
            ViewBag.productimage=productinfo.ProductImage;
            ViewBag.productname= productinfo.ProductName;
            ViewBag.productdesc=productinfo.Description;
            ViewBag.productdate=productinfo.CreatedDate;
            ViewBag.productprice=productinfo.Price;
            

            return View();
        }

        public ActionResult IncreaseQuantity(int productId)
        {
            if (Session["cart"] == null)
            {
                var cart = new List<item>();

                var product = dalobj.T_Product.Find(productId);
                cart.Add(new item()
                {
                    Product = product,
                    Quantity = 1
                });

                Session["cart"] = cart;
            }
            else
            {
                List<item> cart = (List<item>)Session["cart"];
                var count = cart.Count();

                var product = dalobj.T_Product.Find(productId);


                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int periousQuantity = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new item()
                        {
                            Product = product,
                            Quantity = periousQuantity + 1
                        });
                        break;
                    }
                    else
                    {
                        var newrecord = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (newrecord == null)
                        {
                            cart.Add(new item()
                            {
                                Product = product,
                                Quantity = 1
                            });

                        }
                    }
                }

                Session["cart"] = cart;
            }


            return Redirect("Checkout");
        }



        public ActionResult DecreaseQuantity(int productId)
        {
            if (Session["cart"] != null)
            {
                List<item> cart = (List<item>)Session["cart"];
                var product = dalobj.T_Product.Find(productId);
                foreach (var let in cart)
                {
                    if (let.Product.ProductId == productId)
                    {
                        int perviousQua = let.Quantity;
                        if (perviousQua > 0)
                        {
                            cart.Remove(let);
                            cart.Add(new item()
                            {
                                Product = product,
                                Quantity = perviousQua - 1

                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;

            }
            return Redirect("Checkout");


        }







        public ActionResult RemoveToCart(int productId)
        {
            List<item> cart = (List<item>)Session["cart"];
            //var product = dalobj.T_Product.Find(productId);
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }

            Session["cart"] = cart;

            return Redirect("Index");
        }


        public ActionResult RemoveToCartCheckout(int productId)
        {
            List<item> cart = (List<item>)Session["cart"];
            //var product = dalobj.T_Product.Find(productId);
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }

            Session["cart"] = cart;

            return Redirect("Checkout");
        }

        [HttpGet]
        public ActionResult CheckoutDetails()
        {
            if (Session["uid"] == null)
            {
                return Redirect("Login");
            }
            return View();


        }

        [HttpGet]
        public ActionResult Checkout()
        {

            if (Session["uid"] == null)
            {
                return Redirect("Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ShippingDetails()
        {
            if (Session["uid"] == null)
            {
                return Redirect("Login");
            }
            return View();

        }


        [HttpPost]
        public ActionResult ShippingDetails(T_ShippingDetails s)
        {

            try
            {


                T_Members mem = new T_Members();
                T_ShippingDetails shipping = new T_ShippingDetails();

                int data = (int)Session["uid"];


                int data1 = (int)Session["total"];
                string data2 =(string)Session["productname"];

                shipping.MemberId = data;
                shipping.Country = s.Country;
                shipping.State = s.State;
                shipping.City = s.City;
                shipping.Adress = s.Adress;
                shipping.ZipCode = s.ZipCode;

                shipping.AmountPaid = data1;
                shipping.PaymentType = s.PaymentType;
                shipping.ProductName = data2;

                dalobj.T_ShippingDetails.Add(shipping);
                dalobj.SaveChanges();


                return RedirectToAction("PaymentPage");

            }

            catch (Exception)
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult PaymentPage()
        {
            if (Session["uid"] == null)
            {
                return Redirect("Login");
            }

            return View();
        }



        [HttpGet]
        public ActionResult Message()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Message(T_Message m)
        {

            try
            {

                List<T_Message> li = dalobj.T_Message.ToList();

                T_Message message = new T_Message();

                message.MessageId = m.MessageId;
                message.Name = m.Name;
                message.Email = m.Email;
                message.Message = m.Message;

                dalobj.T_Message.Add(message);
                dalobj.SaveChanges();

               

                return RedirectToAction("Index1","Home");

            }
            catch (Exception)
            {

            }
            return View();
        }

        


    }
}