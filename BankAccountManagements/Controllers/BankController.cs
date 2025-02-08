using BankAccountManagements.Models;
using BankAccountManagements.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankAccountManagements.Controllers
{
    public class BankController : Controller
    {
        private readonly BankService _bankService = new BankService();

        /// <summary>
        /// Transfers money between user accounts.
        /// </summary>
        [HttpPost]
        public ActionResult Transfer(string userName, int fromAccountId, int toAccountId, decimal amount)
        {
            if (!_bankService.Transfer(userName, fromAccountId, toAccountId, amount))
            {
                //Error - Cannot transfer to the same account.
                ViewBag.ErrorMessage = "User cannot transfer to the same account";
            }
            return View("Dashboard", _bankService.GetUserByName(userName));
        }

        /// <summary>
        /// Loan process will be handled.
        /// </summary>
        public ActionResult RequestLoan(string userName, decimal amount, int duration)
        {
            if (!_bankService.RequestLoan(userName, amount, duration))
            {
                ViewBag.Message = "Loan denied due to low credit rating";
            }
            return View("Dashboard", _bankService.GetUserByName(userName));
        }

        /// <summary>
        /// Get Interest rates from the database
        /// </summary>
        public ActionResult ConfigureInterest()
        {
            return View(InterestCalculator.GetInterestRates());
        }

        /// <summary>
        /// Update the Interest rates to the database
        /// </summary>
        [HttpPost]
        public ActionResult UpdateInterestRates(List<InterestRate> interestRates)
        {
            InterestCalculator.UpdateInterestRates(interestRates);
            return RedirectToAction("ConfigureInterest");
        }
    }
}