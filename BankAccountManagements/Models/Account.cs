using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAccountManagements.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; } // Current, Savings, Loan
    }
}