using System;
using System.Web.Mvc;
using clothStoreAdmin.ViewModels;
using Firebase.Database.Query;
using System.Threading.Tasks;
using clothStoreAdmin.Models;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
                var imageName = new StringBuilder();
                if (pivm.imageUploadFirst!=null)
                {
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadFirst.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadFirst.InputStream);
                    cpvm.imageUploadFirst = imagePath;
                    cpvm.imageUploadFirstName = imageName.ToString();
                }
                if (pivm.imageUploadSecond != null)
                {
                    imageName = new StringBuilder();
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadSecond.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadSecond.InputStream);
                    cpvm.imageUploadSecond = imagePath;
                    cpvm.imageUploadSecondName = imageName.ToString();
                }
                if (pivm.imageUploadThird != null)
                {
                    imageName = new StringBuilder();
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadThird.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadThird.InputStream);
                    cpvm.imageUploadThird = imagePath;
                    cpvm.imageUploadThirdName = imageName.ToString();
                }
                if (pivm.imageUploadFourth != null)
                {
                    imageName = new StringBuilder();
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadFourth.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadFourth.InputStream);
                    cpvm.imageUploadFourth = imagePath;
                    cpvm.imageUploadFourthName = imageName.ToString();
                }
                UserSession us = new UserSession();
                cpvm.userId = us.userId;
                var prodAdded = await firebase.Child("productMaster").Child(us.userId).PostAsync(cpvm, true);
                data = "Product Added Successfully !";
                ModelState.AddModelError("error", data);
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
        public async Task<ActionResult> EditProduct(string productId, EditProductViewModel epvm, string productStatus, ProductImageViewModel pivm)
        {
            var data = "Product Update Failed !";
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var productImage = FirebaseConnection.FirebaseStorageConnection();
                var imageName = new StringBuilder();
                if (pivm.imageUploadFirst != null)
                {
                    if(!string.IsNullOrEmpty(epvm.imageUploadFirstName))
                    {
                        await productImage.Child("Product").Child(epvm.imageUploadFirstName.Split(',')[1]).DeleteAsync();
                    }
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadFirst.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadFirst.InputStream);
                    epvm.imageUploadFirst = imagePath;
                    epvm.imageUploadFirstName = imageName.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(epvm.imageUploadFirstName))
                    {
                        epvm.imageUploadFirst = null;
                        epvm.imageUploadFirstName = null;
                    }
                    else
                    {
                        var splitImgUrl = epvm.imageUploadFirstName.Split(',');
                        if(splitImgUrl.Length.Equals(2))
                        {
                            epvm.imageUploadFirst = splitImgUrl[0];
                            epvm.imageUploadFirstName = splitImgUrl[1];
                        }
                        else
                        {
                            epvm.imageUploadFirst = null;
                            epvm.imageUploadFirstName = null;
                            await productImage.Child("Product").Child(splitImgUrl[1]).DeleteAsync();
                        }
                    }
                }
                if (pivm.imageUploadSecond != null)
                {
                    if (!string.IsNullOrEmpty(epvm.imageUploadSecondName))
                    {
                        await productImage.Child("Product").Child(epvm.imageUploadSecondName.Split(',')[1]).DeleteAsync();
                    }
                    imageName = new StringBuilder();
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadSecond.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadSecond.InputStream);
                    epvm.imageUploadSecond = imagePath;
                    epvm.imageUploadSecondName = imageName.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(epvm.imageUploadSecondName))
                    {
                        epvm.imageUploadSecond = null;
                        epvm.imageUploadSecondName = null;
                    }
                    else
                    {
                        var splitImgUrl = epvm.imageUploadSecondName.Split(',');
                        if (splitImgUrl.Length.Equals(2))
                        {
                            epvm.imageUploadSecond = splitImgUrl[0];
                            epvm.imageUploadSecondName = splitImgUrl[1];
                        }
                        else
                        {
                            epvm.imageUploadSecond = null;
                            epvm.imageUploadSecondName = null;
                            await productImage.Child("Product").Child(splitImgUrl[1]).DeleteAsync();
                        }
                    }
                }
                if (pivm.imageUploadThird != null)
                {
                    if (!string.IsNullOrEmpty(epvm.imageUploadThirdName))
                    {
                        await productImage.Child("Product").Child(epvm.imageUploadThirdName.Split(',')[1]).DeleteAsync();
                    }
                    imageName = new StringBuilder();
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadThird.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadThird.InputStream);
                    epvm.imageUploadThird = imagePath;
                    epvm.imageUploadThirdName = imageName.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(epvm.imageUploadThirdName))
                    {
                        epvm.imageUploadThird = null;
                        epvm.imageUploadThirdName = null;
                    }
                    else
                    {
                        var splitImgUrl = epvm.imageUploadThirdName.Split(',');
                        if (splitImgUrl.Length.Equals(2))
                        {
                            epvm.imageUploadThird = splitImgUrl[0];
                            epvm.imageUploadThirdName = splitImgUrl[1];
                        }
                        else
                        {
                            epvm.imageUploadThird = null;
                            epvm.imageUploadThirdName = null;
                            await productImage.Child("Product").Child(splitImgUrl[1]).DeleteAsync();
                        }
                    }
                }
                if (pivm.imageUploadFourth != null)
                {
                    if (!string.IsNullOrEmpty(epvm.imageUploadFourthName))
                    {
                        await productImage.Child("Product").Child(epvm.imageUploadFourthName.Split(',')[1]).DeleteAsync();
                    }
                    imageName = new StringBuilder();
                    imageName.Append(DateTime.Now.ToString("dd_MM_yyyy_hhmmss")).Append(Path.GetExtension(pivm.imageUploadFourth.FileName));
                    var imagePath = await productImage.Child("Product").Child(imageName.ToString()).PutAsync(pivm.imageUploadFourth.InputStream);
                    epvm.imageUploadFourth = imagePath;
                    epvm.imageUploadFourthName = imageName.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(epvm.imageUploadFourthName))
                    {
                        epvm.imageUploadFourth = null;
                        epvm.imageUploadFourthName = null;
                    }
                    else
                    {
                        var splitImgUrl = epvm.imageUploadFourthName.Split(',');
                        if (splitImgUrl.Length.Equals(2))
                        {
                            epvm.imageUploadFourth = splitImgUrl[0];
                            epvm.imageUploadFourthName = splitImgUrl[1];
                        }
                        else
                        {
                            epvm.imageUploadFourth = null;
                            epvm.imageUploadFourthName = null;
                            await productImage.Child("Product").Child(splitImgUrl[1]).DeleteAsync();
                        }
                    }
                }
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