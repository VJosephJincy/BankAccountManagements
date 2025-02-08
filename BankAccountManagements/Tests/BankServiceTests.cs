using BankAccountManagements.Models;
using BankAccountManagements.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAccountManagements.Tests
{
    [TestFixture]
    public class BankServiceTests
    {

        private BankService _bankService;
        /// <summary>
        /// Setup method to initialize BankService before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _bankService = new BankService();
        }

        /// Tests if GetUserByName returns the correct user when the user exists.
        [Test]
        public void GetUserByName_WhenUserExists_ShouldReturnUser()
        {
            // Act
            User user = _bankService.GetUserByName("Jim");

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Name, Is.EqualTo("Jim"));
        }

        /// Tests if GetUserByName returns null when the user does not exist.
        [Test]
        public void GetUserByName_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Act
            User user = _bankService.GetUserByName("jincy");

            // Assert
            Assert.That(user, Is.Null);
        }

        /// Tests if Transfer succeeds when there is a sufficient balance.
        [Test]
        public void Transfer_WhenSufficientBalance_ShouldTransferMoney()
        {
            // Arrange
            User user = _bankService.GetUserByName("Anne");
            var account1 = new Account { Id = 1, Balance = 1000, Type = "Current" };
            var account2 = new Account { Id = 2, Balance = 500, Type = "Savings" };
            user.Accounts = new List<Account> { account1, account2 };

            // Act
            bool result = _bankService.Transfer("Anne", 1, 2, 200);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(account1.Balance, Is.EqualTo(800));
            Assert.That(account2.Balance, Is.EqualTo(700));
        }

        /// Tests if Transfer fails when there is an insufficient balance.
        [Test]
        public void Transfer_WhenInsufficientBalance_ShouldFail()
        {
            // Arrange
            User user = _bankService.GetUserByName("Anne");
            var account1 = new Account { Id = 1, Balance = 100, Type = "Current" };
            var account2 = new Account { Id = 2, Balance = 500, Type = "Savings" };
            user.Accounts = new List<Account> { account1, account2 };

            // Act
            bool result = _bankService.Transfer("Anne", 1, 2, 200); // Not enough balance

            // Assert
            Assert.That(result, Is.False);
            Assert.That(account1.Balance, Is.EqualTo(100)); // Should remain unchanged
            Assert.That(account2.Balance, Is.EqualTo(500)); // Should remain unchanged
        }

        /// Tests if RequestLoan denies a loan when the credit rating is too low.
        [Test]
        public void RequestLoan_WhenCreditRatingIsLow_ShouldDenyLoan()
        {
            // Act
            bool result = _bankService.RequestLoan("Bob", 5000, 3); // Bob has Credit Rating 15 (<20)

            // Assert
            Assert.That(result, Is.False);
        }

        /// Tests if RequestLoan approves a loan when the credit rating is sufficient.
        [Test]
        public void RequestLoan_WhenEligible_ShouldApproveLoan()
        {
            // Arrange
            User user = _bankService.GetUserByName("Jim"); // Jim has Credit Rating 45
            decimal initialBalance = user.Accounts.Count > 0 ? user.Accounts[0].Balance : 0;

            // Act
            bool result = _bankService.RequestLoan("Jim", 5000, 3);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(user.Accounts.Exists(a => a.Type == "Loan"), Is.True);
        }

        /// Tests if GetInterestRate returns the correct interest rate for a given credit rating and duration.
        [Test]
        public void GetInterestRate_WhenValidCreditAndDuration_ShouldReturnCorrectRate()
        {
            // Act
            decimal interestRate = InterestCalculator.GetInterestRate(45, 3); // Should return 15%

            // Assert
            Assert.That(interestRate, Is.EqualTo(15));
        }

        /// Tests if GetInterestRate returns zero when no matching rate is found.
        [Test]
        public void GetInterestRate_WhenInvalidCredit_ShouldReturnZero()
        {
            // Act
            decimal interestRate = InterestCalculator.GetInterestRate(10, 3); // Credit < 20, should return 0%

            // Assert
            Assert.That(interestRate, Is.EqualTo(0));
        }
    }

}