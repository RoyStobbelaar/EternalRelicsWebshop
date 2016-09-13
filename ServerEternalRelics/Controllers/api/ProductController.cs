using ServerEternalRelics.DAL;
using ServerEternalRelics.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerEternalRelics.Controllers.api
{
    public class ProductController : ApiController
    {
        private WebshopContext db = new WebshopContext();

        //POST api/Products
        public HttpResponseMessage PostProduct(Product product)
        {
            /* Check modelstate */
            if (ModelState.IsValid)
            {
                /* Set categories/offers to null */
                product.Categories = null;
                product.Offers = null;

                /* Save new product */
                db.Product.Add(product);
                db.SaveChanges();

                /* Add new product to new category */
                product.Categories = new List<Category>{
                    db.Category.First(c => c.Name.Equals("New"))
                };

                db.SaveChanges();

                /* Return response */
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, product);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = product.ProductID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        //Put api/Products?productId=20  Note:should include productId in the product object (unlike in post product) 
        //PUT api/Products/2
        public HttpResponseMessage PutProduct(int productId, Product product)
        {
            /* Check modelstate */
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            /* Check for valid product id */
            if (productId != product.ProductID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //DELETE api/Product?productId=17
        public HttpResponseMessage DeleteProduct(int productId)
        {
            Product product = db.Product.Find(productId);

            /* Check if product exists */
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Product.Remove(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
    }
}