using ServerEternalRelics.DAL;
using ServerEternalRelics.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerEternalRelics.Controllers.api
{
    public class CategoryController : ApiController
    {
        private WebshopContext db = new WebshopContext();

        //POST /api/Category/19/4
        public HttpResponseMessage PostCategory(int productId, int categoryId)
        {
            Product product = db.Product.Find(productId);
            if (product != null)
            {
                if (product.Categories == null)
                    product.Categories = new List<Category>();

                Category cat = db.Category.Find(categoryId);

                if (cat != null)
                {

                    product.Categories.Add(cat);

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created, new { ProductID = productId, CategorieID = categoryId });
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, ModelState);
        }

        //DELETE /api/Categories/1/1
        public HttpResponseMessage DeleteProductCategory(int productId, int categoryId)
        {

            Product product = db.Product.Find(productId);

            if (product != null)
            {

                if (product.Categories == null)
                    product.Categories = new List<Category>();

                Category cat = db.Category.Find(categoryId);

                if (cat != null)
                {
                    if (product.Categories.Contains(cat))
                    {
                        product.Categories.Remove(cat);

                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, new { ProductID = productId, CategorieId = categoryId });
                    }
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
        }
    }
}
