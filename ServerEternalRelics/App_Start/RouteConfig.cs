using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ServerEternalRelics
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Welcome",
                url: "{controller}",
                defaults: new { controller = "Offer", action = "Index" });

            routes.MapRoute(
                name: "AddToCart",
                url: "ShoppingCart/AddToCart/{productId}/{amount}",
                defaults: new { controller = "ShoppingCart", action = "AddToCart", productId = UrlParameter.Optional, amount = UrlParameter.Optional });

            routes.MapRoute(
                name: "RemoveFromCart",
                url: "ShoppingCart/RemoveFromCart/{productId}/{amount}",
                defaults: new { controller = "ShoppingCart", action = "RemoveFromCart", productId = UrlParameter.Optional, amount = UrlParameter.Optional });

            routes.MapRoute(
                name: "Confirm",
                url: "SubmitOrder/Index",
                defaults: new { controller = "SubmitOrder", action = "Index"});

            routes.MapRoute(
                name: "Confirmation",
                url: "SubmitOrder/Confirmation/{err}",
                defaults: new { controller = "SubmitOrder", action = "Confirmation" , err=UrlParameter.Optional});

            routes.MapRoute(
                name: "Success",
                url: "SubmitOrder/Success",
                defaults: new { controller = "SubmitOrder", action = "Success"});

            routes.MapRoute(
                name: "ProductDetails",
                url: "Product/{id}",
                defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "CategoryDetails",
                url: "Category/{id}",
                defaults: new { controller = "Category", action = "Details", id = UrlParameter.Optional });

        }
    }
}
