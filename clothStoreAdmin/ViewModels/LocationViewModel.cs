using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothStoreAdmin.ViewModels
{
    public class LocationViewModel
    {
    }
    public class CreateStateViewModel
    {
        public string stateName { get; set; }
    }
    public class GetStateViewModel
    {
        public string stateName { get; set; }
    }
    public class CreateCityViewModel
    {
        public string state { get; set; }
        public string cityName { get; set; }
    }
    public class GetCityViewModel
    {
        public string state { get; set; }
        public string cityName { get; set; }
    }
}