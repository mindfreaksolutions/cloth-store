using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothStoreAdmin.ViewModels
{
    public class CategorySubcategoryViewModel
    {
    }
    public class CreateSuperCategoryViewModel
    {
        public string superCategoryName { get; set; }
    }
    public class GetSuperCategoryViewModel
    {
        public string superCategoryName { get; set; }
    }
    public class CreateCategoryViewModel
    {
        public string superCategory { get; set; }
        public string categoryName { get; set; }
    }
    public class GetCategoryViewModel
    {
        public string superCategory { get; set; }
        public string categoryName { get; set; }
    }
    public class CreateSubcategoryViewModel
    {
        public string category { get; set; }
        public string subCategoryName { get; set; }
    }
    public class GetSubcategoryViewModel
    {
        public string category { get; set; }
        public string subCategoryName { get; set; }
    }
}