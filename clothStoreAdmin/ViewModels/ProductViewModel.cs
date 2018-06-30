using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothStoreAdmin.ViewModels
{
    public class ProductViewModel
    {
    }
    public class CreateProductViewModel
    {
        public CreateProductViewModel()
        {
            var currentDate = DateTime.Now;
            productActive = false;
            createdon = currentDate;
            updatedon = currentDate;
            imageUploadFirst = null;
            imageUploadSecond = null;
            imageUploadThird = null;
            imageUploadFourth = null;
        }
        public string productCategory { get; set; }
        public string productCategoryName { get; set; }
        public string productSubCategory { get; set; }
        public string productSubCategoryName { get; set; }
        public string productTitle { get; set; }
        public string productDescription { get; set; }
        public string productDiscount { get; set; }
        public string productCoupan { get; set; }
        public bool productActive { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
        public List<ProductVarient> prodVarients { get; set; }
        public string userId { get; set; }
        public string imageUploadFirst { get; set; }
        public string imageUploadFirstName { get; set; }
        public string imageUploadSecond { get; set; }
        public string imageUploadSecondName { get; set; }
        public string imageUploadThird { get; set; }
        public string imageUploadThirdName { get; set; }
        public string imageUploadFourth { get; set; }
        public string imageUploadFourthName { get; set; }
    }
    public class ProductVarient
    {
        public string productPrice { get; set; }
        public int productQuantity { get; set; }
        public string productColor { get; set; }
        public string productSize { get; set; }
        public string productColorCode { get; set; }
    }
    public class GetListProductViewModel
    {
        public string userId { get; set; }
        public string productId { get; set; }
        public string productCategoryName { get; set; }
        public string productSubCategoryName { get; set; }
        public string productTitle { get; set; }
        public string productDescription { get; set; }
        public bool productActive { get; set; }
    }
    public class EditProductViewModel
    {
        public EditProductViewModel()
        {
            var currentDate = DateTime.Now;
            productActive = false;
            updatedon = currentDate;
        }
        public string productCategory { get; set; }
        public string productCategoryName { get; set; }
        public string productSubCategory { get; set; }
        public string productSubCategoryName { get; set; }
        public string productTitle { get; set; }
        public string productDescription { get; set; }
        public string productDiscount { get; set; }
        public string productCoupan { get; set; }
        public bool productActive { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
        public List<ProductVarient> prodVarients { get; set; }
        public string userId { get; set; }
        public string imageUploadFirst { get; set; }
        public string imageUploadFirstName { get; set; }
        public string imageUploadSecond { get; set; }
        public string imageUploadSecondName { get; set; }
        public string imageUploadThird { get; set; }
        public string imageUploadThirdName { get; set; }
        public string imageUploadFourth { get; set; }
        public string imageUploadFourthName { get; set; }
    }
    public class GetProductViewModel
    {
        public string productCategory { get; set; }
        public string productCategoryName { get; set; }
        public string productSubCategory { get; set; }
        public string productSubCategoryName { get; set; }
        public string productTitle { get; set; }
        public string productDescription { get; set; }
        public string productDiscount { get; set; }
        public string productCoupan { get; set; }
        public bool productActive { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
        public List<ProductVarient> prodVarients { get; set; }
        public string userId { get; set; }
        public string imageUploadFirst { get; set; }
        public string imageUploadFirstName { get; set; }
        public string imageUploadSecond { get; set; }
        public string imageUploadSecondName { get; set; }
        public string imageUploadThird { get; set; }
        public string imageUploadThirdName { get; set; }
        public string imageUploadFourth { get; set; }
        public string imageUploadFourthName { get; set; }
    }
    public class ProductImageViewModel
    {
        public HttpPostedFileBase imageUploadFirst { get; set; }
        public HttpPostedFileBase imageUploadSecond { get; set; }
        public HttpPostedFileBase imageUploadThird { get; set; }
        public HttpPostedFileBase imageUploadFourth { get; set; }
    }
}