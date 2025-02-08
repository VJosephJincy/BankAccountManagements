# Bank Account Management - ASP.NET MVC  

## Overview  
This is a simple ASP.NET MVC web application with framework 4.7 for managing bank accounts, supporting money transfers and loan applications. The application is designed for customers, not bank employees, and does not include authentication. 

Sample users in the system 
- Bob
- Jim
- Anne

## Features  
- Users can log with the above usernames.  
- Users can have multiple accounts (Current, Savings, Loan).  
- Users can view their account balances.  
- Users can transfer money between their accounts.  
- Users can request a loan (subject to credit rating rules). 
- As of now created the page to configure interest rates within the user profile. This functionality to be added in admin UI(currently in user UI). 
 

## Loan Rules  
- Loan durations: **1, 3, or 5 years**.  
- Maximum loan amount: **$10,000**.  
- Users with a **credit rating below 20** cannot request a loan.  
- Interest rates are calculated based on credit rating and duration.  

## Interest Rate Table  

| Credit Rating | Duration (Years) | Interest Rate (%) |  
|--------------|----------------|----------------|  
| 20-50       | 1              | 20             |  
| 20-50       | 3              | 15             |  
| 20-50       | 5              | 10             |  
| 50-100      | 1              | 12             |  
| 50-100      | 3              | 8              |  
| 50-100      | 5              | 5              |  

## MVC Structure
    ├── BankAccountManagements/
    │ ├── Controllers/
    │ │ ├── HomeController.cs
    │ │ ├── BankController.cs
    │ ├── Models/
    │ │ ├── User.cs
    │ │ ├── Account.cs
    │ │ ├── InterestRate.cs
    │ ├── Services/
    │ │ ├── BankService.cs
    │ │ ├── InterestCalculator.cs
    │ ├── Views/
    │ │ ├── Home/
    │ │ │ ├── Index.cshtml
    │ │ ├── Bank/
    | | | ├── Dashboard.cshtml
    │ │ │ ├── ConfigureInterest.cshtml
    │ ├── Shared/
    │ │ ├── _Layout.cs
    │ │ ├── _LayoutLogin.cs	
    │ ├── Tests/
    │ │ ├── BankServiceTests.cs
    │ ├── README.md
### Prerequisites  
Install the Nunit test packages from Nuget Packages

- NUnit
- NUnit3TestAdapter
- Microsoft.NET.Test.Sdk 

### Repository
https://github.com/VJosephJincy/BankAccountManagements.git

### Enhancement

- Backend database implementation is required, details are given in Database design section in this file.
- As of now, user information and interest rate are hardcoded in the application, which needs to be implemented from the database.
- Have to implement exception log.
- Need to implement the page not found error page.
- Separate admin UI need to create to handle the configure interest rates. As of now the page is created within the user profile.
- Form authentication can be implemented. 
- User authentication can be implemented from database.
- User information can be stored in session and cookies.  
- Hard coded values should be fetched from database.
- Error handling should be implemented.  

### Database design

- User Table - Storing the user login detaails.
- Account type Table - A master table for storing different type of accounts (Current, Savings, Loan etc)
- Account Table - Saves account details of user along with the account balance.
- Loan details Table - Stores loan requested and approved with details.
- Interest Configure Table - Saves all the details for interest with credit range.
- Transaction Table - Stores all account transfers and loan disbursements.

#### Relationships in the Database

- Users → Accounts: A user can have multiple accounts.
- Users → Loans: A user can apply for multiple loans.
- Loans → Interest Configure: Loan interest is determined based on the user's credit rating.
- Accounts → Transactions: Transactions track money transfers between accounts.




