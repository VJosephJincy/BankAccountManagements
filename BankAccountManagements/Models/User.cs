using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAccountManagements.Models
{
    public class User
    {
        public string Name { get; set; }
        public int CreditRating { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
    
}