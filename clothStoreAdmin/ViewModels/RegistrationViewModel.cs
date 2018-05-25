using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clothStoreAdmin.ViewModels
{
    public class RegistrationViewModel
    {

    }
    public class CreateRegistrationViewModel
    {
        public CreateRegistrationViewModel()
        {
            var currentDate = DateTime.Now;
            designation = "shop owner";
            emailVerification = false;
            userActive = false;
            createdon = currentDate;
            updatedon = currentDate;
        }
        public string emailId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string stateName { get; set; }
        public string city { get; set; }
        public string cityName { get; set; }
        public string password { get; set; }
        public string designation { get; set; }
        public string contactNo { get; set; }
        public bool emailVerification { get; set; }
        public bool userActive { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
        public string uploadProfile { get; set; }
    }
    public class GetRegistrationViewModel
    {
        public string emailId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string stateName { get; set; }
        public string city { get; set; }
        public string cityName { get; set; }
        public string password { get; set; }
        public string designation { get; set; }
        public string contactNo { get; set; }
        public bool emailVerification { get; set; }
        public bool userActive { get; set; }
        public DateTime createdon { get; set; }
        public DateTime updatedon { get; set; }
        public string uploadProfile { get; set; }
    }
}