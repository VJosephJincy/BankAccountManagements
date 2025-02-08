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

        [HttpPost]
        public ActionResult Transfer(string userName, int fromAccountId, int toAccountId, decimal amount)
        {
            if (!_bankService.Transfer(userName, fromAccountId, toAccountId, amount))
            {
                return HttpNotFound();
            }
            return View("Dashboard", _bankService.GetUserByName(userName));
        }

        public ActionResult RequestLoan(string userName, decimal amount, int duration)
        {
            if (!_bankService.RequestLoan(userName, amount, duration))
            {
                return View("LoanDenied");
            }
            return View("Dashboard", _bankService.GetUserByName(userName));
        }

        public ActionResult ConfigureInterest()
        {
            return View(InterestCalculator.GetInterestRates());
        }

        [HttpPost]
        public ActionResult UpdateInterestRates(List<InterestRate> interestRates)
        {
            InterestCalculator.UpdateInterestRates(interestRates);
            return RedirectToAction("ConfigureInterest");
        }
    }
}