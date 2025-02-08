using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankAccountManagements.Models;

namespace BankAccountManagements.Services
{
    public class BankService
    {
        //It should be retrieved from the database. 
        private static readonly List<User> Users = new()
        {
            new User
            {
                Name = "Bob",
                CreditRating = 15,
                Accounts = new List<Account>
                  {
                new Account { Id=1, Type = "Current", Balance = 2000 },
                new Account { Id=2, Type = "Savings", Balance = 500 } }
            },
            new User
            {
                Name = "Jim",
                CreditRating = 45,
                Accounts = new List<Account>
                {
                new Account { Id=1, Type = "Current", Balance = 1000 },
                new Account { Id=2, Type = "Savings", Balance = 5000 } }
            },
            new User
            {
                Name = "Anne",
                CreditRating = 80,
                Accounts = new List<Account>
                 {
                new Account { Id=1, Type = "Current", Balance = 600 },
                new Account { Id=2, Type = "Savings", Balance = 2000 } }
            },
        };

        // Get the user details
        public User GetUserByName(string name) => Users.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());

        //User can transfers money between user accounts.
        public bool Transfer(string userName, int fromAccountId, int toAccountId, decimal amount)
        {
            var user = GetUserByName(userName);
            if (user == null) return false;

            var fromAccount = user.Accounts.FirstOrDefault(a => a.Id == fromAccountId);
            var toAccount = user.Accounts.FirstOrDefault(a => a.Id == toAccountId);
            if (fromAccountId != toAccountId)  // Check the user has selected the same account
            {
                if (fromAccount != null && toAccount != null && fromAccount.Balance >= amount)
                {
                    fromAccount.Balance -= amount;
                    toAccount.Balance += amount;
                    return true;
                }
                else
                {
                    ////Error -Requested money is not available in the account 
                }
            }
            else
            {
                //Error - Cannot transfer to the same account.
            }
            return false;
        }


        /// <summary>
        /// Updates RequestLoan to check if a loan account already exists.
        /// If it exists, add the new loan amount to the existing loan balance.
        /// Otherwise, create a new loan account.
        /// </summary>
        public bool RequestLoan(string userName, decimal amount, int duration)
        {
            var user = GetUserByName(userName);
            if (user == null || user.CreditRating < 20) return false; // Loan denied due to low credit rating.

            decimal interestRate = InterestCalculator.GetInterestRate(user.CreditRating, duration);
            decimal totalLoan = amount + (amount * interestRate / 100);

            var loanAccount = user.Accounts.Find(a => a.Type == "Loan");
            if (loanAccount != null)
            {
                loanAccount.Balance += amount; // Add loan amount to existing balance
            }
            else
            {
                //Create new loan account
                var newLoanAccount = new Account { Id = new Random().Next(1000), Balance = totalLoan, Type = "Loan" };
                user.Accounts.Add(newLoanAccount);
            }
            return true;
        }
    }
}