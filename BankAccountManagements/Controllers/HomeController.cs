using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankAccountManagements.Models;
using BankAccountManagements.Services;

namespace BankAccountManagements.Controllers
{
    public class HomeController : Controller
    {
    
        private readonly BankService _bankService = new BankService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var user = _bankService.GetUserByName(userName.Trim());
                if (user != null)
                {
                        return View("../Bank/Dashboard", user); //use a relative path to specify views in different directories 
                }
            }

                ViewBag.ErrorMessage = "User not found.";
                return View("Index");
     
        }

        public ActionResult Logout()
        {
            // Sign out the user
            // FormsAuthentication.SignOut();

            // Clear the session 
            Session.Clear();

            // Redirect to the login page or home page after logout
            return RedirectToAction("Index", "Home");
        }

    }
}