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
    public class CategorySubcategoryController : Controller
    {
        // GET: CategorySubcategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCategorySubcategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddSuperCategory(CreateSuperCategoryViewModel cscvm)
        {
            var data = "Super Category Added Failed !";
            var status = false;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var superCategoryList = await firebase.Child("superCategory").OnceAsync<GetSuperCategoryViewModel>();
                foreach (var superCategoryDetail in superCategoryList)
                {
                    if (superCategoryDetail.Object.superCategoryName.Equals(cscvm.superCategoryName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        data = "Super Category Name Already Exist !";
                        return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
                    }
                }
                cscvm.superCategoryName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(cscvm.superCategoryName.ToLower().Trim());
                var superCategoryAdded = await firebase.Child("superCategory").PostAsync(cscvm, true);
                data = "Super Category Added Successfully !";
                status = true;
            }
            catch (Exception superCategoryAddException)
            {
                var superCategoryError = superCategoryAddException.GetType().GetProperty("Reason").GetValue(superCategoryAddException, null);
            }
            return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddCategory(CreateCategoryViewModel ccvm)
        {
            var data = "Category Added Failed !";
            var status = false;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var categoryList = await firebase.Child("category").Child(ccvm.superCategory).OnceAsync<GetCategoryViewModel>();
                foreach (var categoryDetail in categoryList)
                {
                    if (categoryDetail.Object.categoryName.Equals(ccvm.categoryName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        data = "Category Name Already Exist !";
                        return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
                    }
                }
                ccvm.categoryName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ccvm.categoryName.ToLower().Trim());
                var categoryAdded = await firebase.Child("category").Child(ccvm.superCategory).PostAsync(ccvm, true);
                var category_Added = firebase.Child("categoryFetch").Child(categoryAdded.Key).PutAsync(ccvm);
                data = "Category Added Successfully !";
                status = true;
            }
            catch (Exception categoryAddException)
            {
                var categoryError = categoryAddException.GetType().GetProperty("Reason").GetValue(categoryAddException, null);
            }
            return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddSubcategory(CreateSubcategoryViewModel csvm)
        {
            var data = "Subcategory Added Failed !";
            var status = false;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var categoryList = await firebase.Child("subCategory").Child(csvm.category).OnceAsync<GetSubcategoryViewModel>();
                foreach (var categoryDetail in categoryList)
                {
                    if (categoryDetail.Object.subCategoryName.Equals(csvm.subCategoryName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        data = "Subcategory Name Already Exist !";
                        return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
                    }
                }
                csvm.subCategoryName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(csvm.subCategoryName.ToLower().Trim());
                var subCategoryAdded = await firebase.Child("subCategory").Child(csvm.category).PostAsync(csvm, true);
                data = "Subcategory Added Successfully !";
                status = true;
            }
            catch (Exception subCategoryAddException)
            {
                var subCatgeoryError = subCategoryAddException.GetType().GetProperty("Reason").GetValue(subCategoryAddException, null);
            }
            return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> LoadSuperCategory()
        {
            var data = "Super Category Load Failed !";
            var status = false;
            var superCategoryList = (dynamic)null;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                superCategoryList = await firebase.Child("superCategory").OnceAsync<GetSuperCategoryViewModel>();
                data = "Super Category Loaded Successfully !";
                status = true;
            }
            catch (Exception superCategoryLoadException)
            {
                var superCategoryError = superCategoryLoadException.GetType().GetProperty("Reason").GetValue(superCategoryLoadException, null);
            }
            return Json(new { data = data, status = status, superCategory = superCategoryList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> LoadCategory(string superCategoryKey)
        {
            var data = "Category Load Failed !";
            var status = false;
            var categoryList = (dynamic)null;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                switch(superCategoryKey)
                {
                    case null:
                        categoryList =await firebase.Child("categoryFetch").OnceAsync<GetCategoryViewModel>();
                        break;
                    default:
                        categoryList = await firebase.Child("category").Child(superCategoryKey).OnceAsync<GetCategoryViewModel>();
                        break;
                }
                data = "Category Loaded Successfully !";
                status = true;
            }
            catch (Exception categoryLoadException)
            {
                var categoryError = categoryLoadException.GetType().GetProperty("Reason").GetValue(categoryLoadException, null);
            }
            return Json(new { data = data, status = status, category = categoryList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> LoadSubcategory(string categoryKey)
        {
            var data = "Subcategory Load Failed !";
            var status = false;
            var subCategoryList = (dynamic)null;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                subCategoryList = await firebase.Child("subCategory").Child(categoryKey).OnceAsync<GetSubcategoryViewModel>();
                data = "Subcategory Loaded Successfully !";
                status = true;
            }
            catch (Exception subCategoryLoadException)
            {
                var subCategoryError = subCategoryLoadException.GetType().GetProperty("Reason").GetValue(subCategoryLoadException, null);
            }
            return Json(new { data = data, status = status, subcategory = subCategoryList }, JsonRequestBehavior.AllowGet);
        }
    }
}