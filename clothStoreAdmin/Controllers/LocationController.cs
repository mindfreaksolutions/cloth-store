using System;
using System.Web.Mvc;
using clothStoreAdmin.ViewModels;
using Firebase.Database.Query;
using System.Threading.Tasks;
using clothStoreAdmin.Models;
using System.Web;

namespace clothStoreAdmin.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddState(CreateStateViewModel csvm)
        {
            var data = "State Added Failed !";
            var status = false;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var stateList = await firebase.Child("state").OnceAsync<GetStateViewModel>();
                foreach(var stateDetail in stateList)
                {
                    if(stateDetail.Object.stateName.Equals(csvm.stateName,StringComparison.CurrentCultureIgnoreCase))
                    {
                        data = "State Name Already Exist !";
                        return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
                    }
                }
                csvm.stateName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(csvm.stateName.ToLower().Trim());
                var stateAdded = await firebase.Child("state").PostAsync(csvm, true);
                data = "State Added Successfully !";
                status = true;
            }
            catch (Exception stateAddException)
            {
                var stateError = stateAddException.GetType().GetProperty("Reason").GetValue(stateAddException, null);
            }
            return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Addcity(CreateCityViewModel ccvm)
        {
            var data = "City Added Failed !";
            var status = false;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                var cityList = await firebase.Child("city").Child(ccvm.state).OnceAsync<GetCityViewModel>();
                foreach (var cityDetail in cityList)
                {
                    if (cityDetail.Object.cityName.Equals(ccvm.cityName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        data = "City Name Already Exist !";
                        return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
                    }
                }
                ccvm.cityName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ccvm.cityName.ToLower().Trim());
                var cityAdded = await firebase.Child("city").Child(ccvm.state).PostAsync(ccvm, true);
                data = "City Added Successfully !";
                status = true;
            }
            catch (Exception cityAddException)
            {
                var cityError = cityAddException.GetType().GetProperty("Reason").GetValue(cityAddException, null);
            }
            return Json(new { data = data, status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> LoadState()
        {
            var data = "State Load Failed !";
            var status = false;
            var stateList = (dynamic)null;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                stateList = await firebase.Child("state").OnceAsync<GetStateViewModel>();
                data = "State Loaded Successfully !";
                status = true;
            }
            catch (Exception stateLoadException)
            {
                var stateError = stateLoadException.GetType().GetProperty("Reason").GetValue(stateLoadException, null);
            }
            return Json(new { data = data, status = status, state=stateList }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public async Task<JsonResult> LoadCity(string stateKey)
        {
            var data = "City Load Failed !";
            var status = false;
            var cityList = (dynamic)null;
            try
            {
                var firebase = FirebaseConnection.FirebaseDatabase();
                cityList = await firebase.Child("city").Child(stateKey).OnceAsync<GetCityViewModel>();
                data = "city Loaded Successfully !";
                status = true;
            }
            catch (Exception cityLoadException)
            {
                var cityError = cityLoadException.GetType().GetProperty("Reason").GetValue(cityLoadException, null);
            }
            return Json(new { data = data, status = status, city = cityList }, JsonRequestBehavior.AllowGet);
        }
    }
}