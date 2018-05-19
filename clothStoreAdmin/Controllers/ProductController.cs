using System;
using System.Web.Mvc;
using clothStoreAdmin.ViewModels;
using Firebase.Database.Query;
using System.Threading.Tasks;
using clothStoreAdmin.Models;
using System.Collections.Generic;
using System.Text;

namespace clothStoreAdmin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(CreateProductViewModel cpvm, ProductImageViewModel pivm)
        {
            var data = "Product Added Failed !";
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var productImage = FirebaseConnection.FirebaseStorageConnection();
                if (pivm.imageUploadFirst!=null)
                {
                    var imagePath = await productImage.Child("Product").Child(pivm.imageUploadFirst.FileName).PutAsync(pivm.imageUploadFirst.InputStream);
                    cpvm.imageUploadFirst = imagePath;
                    cpvm.imageUploadFirstName = pivm.imageUploadFirst.FileName;
                }
                if (pivm.imageUploadSecond != null)
                {
                    var imagePath = await productImage.Child("Product").Child(pivm.imageUploadSecond.FileName).PutAsync(pivm.imageUploadSecond.InputStream);
                    cpvm.imageUploadSecond = imagePath;
                    cpvm.imageUploadSecondName = pivm.imageUploadSecond.FileName;
                }
                if (pivm.imageUploadThird != null)
                {
                    var imagePath = await productImage.Child("Product").Child(pivm.imageUploadThird.FileName).PutAsync(pivm.imageUploadThird.InputStream);
                    cpvm.imageUploadThird = imagePath;
                    cpvm.imageUploadThirdName = pivm.imageUploadThird.FileName;
                }
                if (pivm.imageUploadFourth != null)
                {
                    var imagePath = await productImage.Child("Product").Child(pivm.imageUploadFourth.FileName).PutAsync(pivm.imageUploadFourth.InputStream);
                    cpvm.imageUploadFourth = imagePath;
                    cpvm.imageUploadFourthName = pivm.imageUploadFourth.FileName;
                }
                UserSession us = new UserSession();
                cpvm.userId = us.userId;
                var prodAdded = await firebase.Child("productMaster").Child(us.userId).PostAsync(cpvm, true);
                data = "Product Added Successfully !";
            }
            catch (Exception productAddException)
            {
                var prodError = productAddException.GetType().GetProperty("Reason").GetValue(productAddException, null);
                ModelState.AddModelError("error", prodError.ToString());
                return View();
            }
            return RedirectToAction("AddProduct", "Product", new { Status = data });
        }

        [HttpGet]
        public ActionResult EditProduct(string productId, string productStatus)
        {
            ViewBag.productId = productId;
            ViewBag.productStatus = productStatus;
            return View();
        }

        [HttpPost]
        public ActionResult EditProduct(string productId, EditProductViewModel epvm, string productStatus)
        {
            var data = "Product Update Failed !";
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                UserSession us = new UserSession();
                epvm.userId = us.userId;
                var prodUpdate = firebase.Child("productMaster").Child(us.userId).Child(productId).PutAsync(epvm);
                data = "Product Updated Successfully !";
            }
            catch (Exception productEditException)
            {
                var prodError = productEditException.GetType().GetProperty("Reason").GetValue(productEditException, null);
                ModelState.AddModelError("error", prodError.ToString());
                return View();
            }
            var sb = new StringBuilder();
            var action = sb.Append(productStatus).Append("ProductList").ToString();
            return RedirectToAction(action, "Product", new { Status = data });
        }

        [HttpPost]
        public async Task<ActionResult> DetailProduct(string productId)
        {
            UserSession us = new UserSession();
            var firebase = FirebaseConnection.FirebaseDatabase();
            var productDetails = await firebase.Child("productMaster").Child(us.userId).Child(productId).OnceSingleAsync<GetProductViewModel>();
            return Json(new { productDetails = productDetails });
        }

        [HttpGet]
        public async Task<ActionResult> ActiveInactiveProduct(string productId, string productStatus)
        {
            var sb = new StringBuilder();
            var action = sb.Append(productStatus).Append("ProductList").ToString();
            sb = new StringBuilder();
            var data = sb.Append("Product ").Append(productStatus).Append(" Failed !").ToString();
            try
            {
                UserSession us = new UserSession();
                var firebase = FirebaseConnection.FirebaseDatabase();
                var productDetails = await firebase.Child("productMaster").Child(us.userId).Child(productId).OnceSingleAsync<GetProductViewModel>();
                productDetails.productActive = true;
                var prodUpdate = firebase.Child("productMaster").Child(us.userId).Child(productId).PutAsync(productDetails);
                data = data.Replace("Failed", "Successfully");
                //data = sb.Append("Product ").Append(productStatus).Append(" Successfully !").ToString();
            }
            catch (Exception productInactiveException)
            {
                var prodError = productInactiveException.GetType().GetProperty("Reason").GetValue(productInactiveException, null);
                data = prodError.ToString();
            }
            return RedirectToAction(action, "Product", new { Status = data });
        }

        [HttpGet]
        public ActionResult ActiveProductList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult InactiveProductList()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetProductList(bool productStatus)
        {
            //var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //var start = Request.Form.GetValues("start").FirstOrDefault();
            //var length = Request.Form.GetValues("length").FirstOrDefault();
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            ////Paging Size (10,20,50,100)    
            //int pageSize = length != null ? Convert.ToInt32(length) : 0;
            //int skip = start != null ? Convert.ToInt32(start) : 0;
            //int recordsTotal = 0;
            var firebase = FirebaseConnection.FirebaseDatabase();
            UserSession us = new UserSession();
            var prodList = await firebase.Child("productMaster").Child(us.userId).OnceAsync<GetListProductViewModel>();
            var productAdd = new List<GetListProductViewModel>();
            foreach (var p in prodList)
            {
                p.Object.productId = p.Key;
                //p.Object.userId = us.userId;
                productAdd.Add(p.Object);
            }
            ////Sorting    
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    productAdd = productAdd.OrderBy(m => m.productTitle).ToList();
            //}
            ////Search    
            //if (!string.IsNullOrEmpty(searchValue))
            //{
            //    productAdd = productAdd.Where(m => m.productTitle == searchValue).ToList();
            //}
            var data = new { data = productAdd};
            //recordsTotal = productAdd.Count;
            //productAdd = productAdd.Skip(skip).Take(pageSize).ToList();
            return Json(data ,JsonRequestBehavior.AllowGet);
        }
    }
}