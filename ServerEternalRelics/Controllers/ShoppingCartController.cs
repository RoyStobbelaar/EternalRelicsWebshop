using ServerEternalRelics.DAL;
using ServerEternalRelics.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ServerEternalRelics.GiroReference;

namespace ServerEternalRelics.Controllers
{
    public class ShoppingCartController : Controller
    {
        private WebshopContext db = new WebshopContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<OrderLine> orderlines = new List<OrderLine>();

            if(Session["ShoppingCart"] != null)
                orderlines = (List<OrderLine>)Session["ShoppingCart"];
            else
                RedirectToAction("Index", "Offer");

            return View(orderlines);
        }

        /* Submit order -> empty view */
        /*
        public ActionResult Submit()
        {
            if (Session["ShoppingCart"] == null)
                RedirectToAction("Index", "Offer");

            Customer customer = null;

            return View(customer);
        }
        */
        /* Submit order -> customer created -> open confirmation screen */
        /*
        [HttpPost]
        public ActionResult Submit(Customer customer)
        {
            if (Session["ShoppingCart"] == null)
                RedirectToAction("Index", "Offer");

            if (ModelState.IsValid)
            {
                Session["Customer"] = customer;

                return RedirectToAction("Confirmation", "ShoppingCart");
            }
            return View(customer);
        }
         */

        /* Retry confirmation */
        /*
        public ActionResult Confirmation(string err="")
        {
            if (Session["ShoppingCart"] == null)
                RedirectToAction("Index", "Offer");

            Customer customer = (Customer)Session["Customer"];
            Order newOrder = new Order { Customer = customer, CustomerID = customer.CustomerID, OrderLines = (List<OrderLine>)Session["ShoppingCart"] };

            if (err != null && err.Equals("agree"))
                ModelState.AddModelError("agree", "You'll have to agree with the terms and conditions.");

            return View(newOrder);
        }
        */
        
        //[HttpPost]
        //public ActionResult Confirmation(bool agreed)
        //{
        //    var shoppingCart = (List<OrderLine>)Session["ShoppingCart"];
        //    var customer = (Customer)Session["Customer"];

        //    /* No cart or products */
        //    if (shoppingCart == null || !shoppingCart.Any())
        //        return RedirectToAction("Index", "Offer");

        //    /* No customer */
        //    if (customer == null)
        //        return RedirectToAction("Index", "Offer");

        //    /* Agreed */
        //    if (agreed)
        //    {
        //        /* Save customer */
        //        db.Customer.Add(customer);

        //        /* Create order with current customer and orderlines */
        //        var order = new Order
        //        {
        //            Customer = customer,
        //            OrderLines = new List<OrderLine>()
        //            //OrderLines = shoppingCart
        //        };

        //        /* Save order */
        //        db.Order.Add(order);

        //        /* Save orderlines in db */
        //        foreach (var orderline in shoppingCart)
        //        {
        //            var orderLine = new OrderLine()
        //            {
        //                Order = order,
        //                Amount = orderline.Amount,
        //                Product = db.Product.First(p => p.ProductID == orderline.Product.ProductID)
        //            };
        //            db.OrderLine.Add(orderLine);
        //        }

        //        db.SaveChanges();
        //        decimal totalPrice = (decimal)Session["TotalPrice"];
                
        //        Task.Factory.StartNew(() =>
        //        {
        //            var giro = new Giro();

        //            giro.GetGiroNumberAsync(customer.FullName, customer.Address, customer.Zipcode, customer.Residence, totalPrice, true);
        //            giro.GetGiroNumberCompleted += (sender, e) =>
        //            {
        //                var giroNumber = e.GetGiroNumberResult;

        //                order.GiroNumber = giroNumber;

        //                db.Entry(order).State = EntityState.Modified;
        //                db.SaveChanges();

        //                System.Diagnostics.Debug.WriteLine("Giro received from customer " + customer.FullName + " with number " + giroNumber);
        //            };
        //        });
                 

        //        /* Reset session data */
        //        Session["ShoppingCart"] = null;
        //        Session["Customer"] = null;
        //        Session["TotalPrice"] = null;
        //        Session["Success"] = true;

        //        return RedirectToAction("Success");
        //    }

        //    return RedirectToAction("Confirmation", new { err = "agree" });
        //}

        //public ActionResult Success()
        //{
        //    var success = (bool)Session["Success"];

        //    if (!success)
        //        return RedirectToAction("Index", "Offer");

        //    Session["Success"] = null;

        //    return View();
        //}

        //[Route("ShoppingCart/AddToCart/{productId:int}/{amount:int}")]
        [ActionName("AddToCart")]
        [HttpPost]
        public JsonResult AddToCart(int productId, int amount)
        {
            Product product = db.Product.Find(productId);
            OrderLine newOrderLine = new OrderLine { Amount = amount, Product = product };

            /* Check if cart is empty */
            if (Session["ShoppingCart"] == null)
            {
                List<OrderLine> orderLines = new List<OrderLine>();
                orderLines.Add(newOrderLine);

                Session["ShoppingCart"] = orderLines;
            }
            else /* Else add to cart */
            {
                List<OrderLine> orderLines = (List<OrderLine>)Session["ShoppingCart"];
                bool itemExists = false;
                /* Check if product if already in cart */
                foreach(OrderLine line in orderLines)
                {
                    if (line.Product.ProductID == productId)
                    {
                        itemExists = true;
                        line.Amount = line.Amount + amount;
                    }
                }
                
                if (!itemExists) /* Product does not exist */
                    orderLines.Add(newOrderLine);
            }

            if (Session["TotalPrice"] == null)
                Session["TotalPrice"] = product.CurrentPrice * amount;
            else
            {
                decimal totalPrice = (decimal)Session["TotalPrice"];
                totalPrice += product.CurrentPrice * amount;
                Session["TotalPrice"] = totalPrice;
            }

            return Json(Session["TotalPrice"], JsonRequestBehavior.AllowGet);
        }

        //[Route("ShoppingCart/RemoveFromCart/{productId:int}/{amount:int}")]
        [ActionName("RemoveFromCart")]
        [HttpPost]
        public JsonResult RemoveFromCart(int productId, int amount)
        {
            Product product = db.Product.Find(productId);
            bool amountIsZero=false;
            if (product != null)
            {
                List<OrderLine> orderLines = new List<OrderLine>();
                orderLines = (List<OrderLine>)Session["ShoppingCart"];

                foreach (OrderLine line in orderLines)
                {
                    if (line.Product.ProductID == product.ProductID)
                    {
                        line.Amount = line.Amount - amount;

                        if (line.Amount == 0)
                        {
                            amountIsZero = true;
                            //orderLines.Remove(line);
                        }

                        Session["TotalPrice"] = (decimal)Session["TotalPrice"] - product.CurrentPrice * amount;

                        Session["ShoppingCart"] = orderLines;
                    }
                }
                if (amountIsZero)
                {
                    orderLines.RemoveAll(item => item.Product.ProductID == productId);
                    if ((decimal)Session["TotalPrice"] == 0)
                        Session["TotalPrice"] = null;
                }
            }
            return Json(Session["TotalPrice"], JsonRequestBehavior.AllowGet);
        }
    }
}