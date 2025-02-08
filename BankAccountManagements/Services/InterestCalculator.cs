using BankAccountManagements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAccountManagements.Services
{
    public static class InterestCalculator
    {
        private static List<InterestRate> interestRates = new()
        {
            new InterestRate { CreditMin = 20, CreditMax = 50, Duration = 1, Rate = 20 },
            new InterestRate { CreditMin = 20, CreditMax = 50, Duration = 3, Rate = 15 },
            new InterestRate { CreditMin = 20, CreditMax = 50, Duration = 5, Rate = 10 },
            new InterestRate { CreditMin = 50, CreditMax = 100, Duration = 1, Rate = 12 },
            new InterestRate { CreditMin = 50, CreditMax = 100, Duration = 3, Rate = 8 },
            new InterestRate { CreditMin = 50, CreditMax = 100, Duration = 5, Rate = 5 }
        };

        public static decimal GetInterestRate(int creditRating, int duration)
        {
            return interestRates.FirstOrDefault(rate => creditRating >= rate.CreditMin && creditRating <= rate.CreditMax && rate.Duration == duration)?.Rate ?? 0;
        }

        public static List<InterestRate> GetInterestRates() => interestRates;

        public static void UpdateInterestRates(List<InterestRate> updatedRates)
        {
            interestRates = updatedRates;
        }
    }
}