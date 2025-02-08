using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAccountManagements.Models
{
    public class InterestRate
    {
        public int CreditMin { get; set; }
        public int CreditMax { get; set; }
        public int Duration { get; set; }
        public decimal Rate { get; set; }
    }
}