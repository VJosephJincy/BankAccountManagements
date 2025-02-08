using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankAccountManagements.Models;

namespace BankAccountManagements.Services
{
    public class BankService
    {
        private static readonly List<User> Users = new()
        {
            new User
            {
                Name = "Eric",
                CreditRating = 15,
                Accounts = new List<Account>
                  {
                new Account { Id=1, Type = "Current", Balance = 2000 },
                new Account { Id=2, Type = "Savings", Balance = 500 } }
            },
            new User
            {
                Name = "Jincy",
                CreditRating = 45,
                Accounts = new List<Account>
                {
                new Account { Id=1, Type = "Current", Balance = 1000 },
                new Account { Id=2, Type = "Savings", Balance = 5000 } }
            },
            new User
            {
                Name = "Kim",
                CreditRating = 80,
                Accounts = new List<Account>
                 {
                new Account { Id=1, Type = "Current", Balance = 600 },
                new Account { Id=2, Type = "Savings", Balance = 2000 } }
            },
        };


        public User GetUserByName(string name) => Users.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());

        public bool Transfer(string userName, int fromAccountId, int toAccountId, decimal amount)
        {
            var user = GetUserByName(userName);
            if (user == null) return false;

            var fromAccount = user.Accounts.FirstOrDefault(a => a.Id == fromAccountId);
            var toAccount = user.Accounts.FirstOrDefault(a => a.Id == toAccountId);

            if (fromAccount != null && toAccount != null && fromAccount.Balance >= amount)
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                return true;
            }
            return false;
        }

        public bool RequestLoan(string userName, decimal amount, int duration)
        {
            var user = GetUserByName(userName);
            if (user == null || user.CreditRating < 20) return false;

            decimal interestRate = InterestCalculator.GetInterestRate(user.CreditRating, duration);
            decimal totalLoan = amount + (amount * interestRate / 100);

            var loanAccount = new Account { Id = new Random().Next(1000), Balance = totalLoan, Type = "Loan" };
            user.Accounts.Add(loanAccount);
            return true;
        }
    }
}