using clothStoreAdmin.Models;
using clothStoreAdmin.ViewModels;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace clothStoreAdmin.Controllers
{
    public class ProductVarientController : Controller
    {
        // GET: ProductVarient
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddProductVarient()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddProductSize(CreateProductVarientViewModel cpvvm)
        {
            var data = "Product Size Added Failed !";
            var status = false;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var sizeList = await firebase.Child("size").OnceAsync<GetProductVarientViewModel>();
                foreach (var sizeDetail in sizeList)
                {
                    if (sizeDetail.Object.sizeName.Equals(cpvvm.sizeName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        data = "Product Size Name Already Exist !";
                        return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
                    }
                }
                cpvvm.sizeName = cpvvm.sizeName.ToUpper().Trim();
                var sizeAdded = await firebase.Child("size").PostAsync(cpvvm, true);
                data = "Product Size Added Successfully !";
                status = true;
            }
            catch (Exception sizeAddException)
            {
                var sizeError = sizeAddException.GetType().GetProperty("Reason").GetValue(sizeAddException, null);
            }
            return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> LoadProductSize()
        {
            var data = "Product Size Load Failed !";
            var status = false;
            var sizeList = (dynamic)null;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                sizeList = await firebase.Child("size").OnceAsync<GetProductVarientViewModel>();
                data = "Product Size Loaded Successfully !";
                status = true;
            }
            catch (Exception sizeLoadException)
            {
                var sizeError = sizeLoadException.GetType().GetProperty("Reason").GetValue(sizeLoadException, null);
            }
            return Json(new { data = data, status = status, size = sizeList }, JsonRequestBehavior.AllowGet);
        }
    }
}