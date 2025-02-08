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

        /// <summary>
        /// To displays the login page.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Authorized user will readirct to the dasboard.
        /// </summary>
        [HttpPost]
        public ActionResult Index(string userName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    var user = _bankService.GetUserByName(userName.Trim());
                    if (user != null)
                    {
                        return View("../Bank/Dashboard", user); //use a relative path to specify views in different directories 
                    }
                }

               
            }
            catch (Exception ex)
            {
                //log the error message 
               // return View("Error"); return to error page 
            }
            finally
            {
                //dispose() method 
            }
            ViewBag.ErrorMessage = "User not found.";
            return View("Index");
        }

        /// <summary>
        /// User will redirect to the  the login page.
        /// </summary>
        public ActionResult Logout()
        {
            // Sign out the user
            // FormsAuthentication.SignOut();

            // Clear the session 
            Session.Clear();

            // Redirect to the login page after logout
            return RedirectToAction("Index", "Home");
        }

    }
}