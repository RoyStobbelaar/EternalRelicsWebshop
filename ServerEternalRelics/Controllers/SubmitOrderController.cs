using ServerEternalRelics.DAL;
using ServerEternalRelics.GiroReference;
using ServerEternalRelics.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServerEternalRelics.Controllers
{
    public class SubmitOrderController : Controller
    {
        private WebshopContext db = new WebshopContext();

        // GET: SubmitOrder
        /* Open submit order page */
        public ActionResult Index()
        {
            if (Session["ShoppingCart"] == null)
                RedirectToAction("Index", "Offer");

            Customer customer = null;

            return View(customer);
        }

        [HttpPost]
        public ActionResult Index(Customer customer)
        {
            if (Session["ShoppingCart"] == null)
                RedirectToAction("Index", "Offer");

            if (ModelState.IsValid)
            {
                Session["Customer"] = customer;

                /* If information is valid, go to confirmation */
                return RedirectToAction("Confirmation");
            }
            return View(customer);
        }

        /* Open confirmation page */
        public ActionResult Confirmation(string err = "")
        {
            if (Session["ShoppingCart"] == null)
                RedirectToAction("Index", "Offer");

            Customer customer = (Customer)Session["Customer"];
            Order newOrder = new Order { Customer = customer, CustomerID = customer.CustomerID, OrderLines = (List<OrderLine>)Session["ShoppingCart"] };

            if (err != null && err.Equals("agree"))
                ModelState.AddModelError("agree", "You'll have to agree with the terms and conditions.");

            return View(newOrder);
        }

        [HttpPost]
        public ActionResult Confirmation(bool agreed)
        {
            var shoppingCart = (List<OrderLine>)Session["ShoppingCart"];
            var customer = (Customer)Session["Customer"];

            /* No cart or products */
            if (shoppingCart == null || !shoppingCart.Any())
                return RedirectToAction("Index", "Offer");

            /* No customer */
            if (customer == null)
                return RedirectToAction("Index", "Offer");

            /* Agreed */
            if (agreed)
            {
                /* Save customer */
                db.Customer.Add(customer);

                /* Create order with current customer and orderlines */
                var order = new Order
                {
                    Customer = customer,
                    OrderLines = new List<OrderLine>()
                };

                /* Save order */
                db.Order.Add(order);

                /* Save orderlines in db */
                foreach (var orderline in shoppingCart)
                {
                    var orderLine = new OrderLine()
                    {
                        Order = order,
                        Amount = orderline.Amount,
                        Product = db.Product.First(p => p.ProductID == orderline.Product.ProductID)
                    };
                    db.OrderLine.Add(orderLine);
                }

                db.SaveChanges();
                decimal totalPrice = (decimal)Session["TotalPrice"];

                Task.Factory.StartNew(() =>
                {
                    var giro = new Giro();

                    giro.GetGiroNumberAsync(customer.FullName, customer.Address, customer.Zipcode, customer.Residence, totalPrice, true);
                    giro.GetGiroNumberCompleted += (sender, e) =>
                    {
                        var giroNumber = e.GetGiroNumberResult;

                        order.GiroNumber = giroNumber;

                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();

                        System.Diagnostics.Debug.WriteLine("Giro received from customer " + customer.FullName + " with number " + giroNumber);
                    };
                });


                /* Reset session data */
                Session["ShoppingCart"] = null;
                Session["Customer"] = null;
                Session["TotalPrice"] = null;
                Session["Success"] = true;

                /* Redirect to success page */
                return RedirectToAction("Success");
            }

            /* Redirect to confirmation with 'you have to agree' msg */
            return RedirectToAction("Confirmation", new { err = "agree" });
        }

        /* Open success page */
        public ActionResult Success()
        {
            var success = (bool)Session["Success"];

            if (!success)
                return RedirectToAction("Index", "Offer");

            Session["Success"] = null;

            return View();
        }
    }
}