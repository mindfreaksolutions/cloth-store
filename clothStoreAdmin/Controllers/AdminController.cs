using System;
using System.Web.Mvc;
using clothStoreAdmin.ViewModels;
using Firebase.Database.Query;
using System.Threading.Tasks;
using clothStoreAdmin.Models;
using System.Web;
using System.Text;

namespace clothStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.previousURL=HttpContext.Request.UrlReferrer;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel lvm)
        {
            var sb = new StringBuilder();
            var fa = FirebaseConnection.FirebaseAuthentication();
            try
            {
                var userAuth = await fa.SignInWithEmailAndPasswordAsync(lvm.userName, lvm.password);
                var firebase = FirebaseConnection.FirebaseDatabase();
                var userDetails =await firebase.Child("adminUserRegistration").Child(userAuth.User.LocalId).OnceAsync<GetRegistrationViewModel>();
                foreach(var ud in userDetails)
                {
                    SetSession.SetUserSession(userAuth.User.LocalId, ud.Object.designation,sb.Append(ud.Object.firstName).Append(" ").Append(ud.Object.lastName).ToString());
                }
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception userAuthException)
            {
                var userError = userAuthException.GetType().GetProperty("Reason").GetValue(userAuthException, null);
                ModelState.AddModelError("error", userError.ToString());
                return View();
            }
        }

        [HttpGet]
        public  ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(CreateRegistrationViewModel crvm, HttpPostedFileBase uploadProfile)
        {
            //createRegistrationViewModel crvm_ = new createRegistrationViewModel();
            var data ="Registration Failed !";
            try
            {
                var fa = FirebaseConnection.FirebaseAuthentication();
                var userVerification = await fa.CreateUserWithEmailAndPasswordAsync(crvm.emailId, crvm.password,crvm.firstName+" "+crvm.lastName,true);
                //if (crvm.uploadProfile != null)
                //{
                //    crvm.uploadProfile.SaveAs(Path.Combine(Server.MapPath("/images/"), crvm.uploadProfile.FileName));
                //}
                if (uploadProfile != null)
                {
                    var profileImage = FirebaseConnection.FirebaseStorageConnection();
                    var imagePath = await profileImage.Child("Registration").Child(uploadProfile.FileName).PutAsync(uploadProfile.InputStream);
                    crvm.uploadProfile = imagePath;
                }
                var firebase = FirebaseConnection.FirebaseDatabase();
                var regComplete =await firebase.Child("adminUserRegistration").Child(userVerification.User.LocalId).PostAsync(crvm, true);
                data = "Registration Successfully !";
            }
            catch (Exception userAuthException)
            {
                var userError = userAuthException.GetType().GetProperty("Reason").GetValue(userAuthException, null);
                ModelState.AddModelError("error", userError.ToString());
                return View();
            }
            return RedirectToAction("Login","Admin",new {Status=data });
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

    }
}