using OnlineShoppingStore1.DAL;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore1.Controllers
{
    public class CreditCardController : Controller
    {
        OnlineShoppingEntities dalobj = new OnlineShoppingEntities();

        // GET: CreditCard


        [HttpGet]
        public ActionResult Charge()
        {
            if (Session["uid"] == null)
            {
                return Redirect("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Charge(string stripeToken, string stripeEmail)
        {
            T_Members m = new T_Members();



            StripeConfiguration.ApiKey = "sk_test_LxFXRsxnoaNeykNC7ucJ3ztg006XowH6fy";



            var myCharge = new ChargeCreateOptions();
            {
                // always set these properties


                myCharge.Amount = (int)Session["total"];

                myCharge.Currency = "INR";
                myCharge.ReceiptEmail = stripeEmail;
                myCharge.Description = "Sample Charge";
                myCharge.Source = stripeToken;
                myCharge.Capture = true;


                myCharge.Metadata = new Dictionary<string, string>
                    {

                        { "MemberId", "6735" },
                    };
            }

            var chargeService = new Stripe.ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);
            return View();
        }
    }
}