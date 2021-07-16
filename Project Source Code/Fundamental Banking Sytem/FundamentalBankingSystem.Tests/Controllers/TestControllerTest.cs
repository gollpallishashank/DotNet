using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundamentalBankingSystem.Controllers;
using FundamentalBankingSystem.Models;
using System;

namespace FundamentalBankingSystem.Tests.Controllers
{
    [TestClass]
    public class TestControllerTest
    {
        [TestMethod]
        public void ViewHome()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.Home() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ViewAboutUs()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.AboutUs() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ViewFAQ()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.FAQ() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ViewManager()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.Manager() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ViewCashier()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.Cashier() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManagerLoginwithCorrectCredentials()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            tbl_Login_details ob = new tbl_Login_details();
            ob.username = "Manager";

            // Act
            RedirectToRouteResult result = controller.Login(ob, "bb@mg") as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result); 
            Assert.AreEqual("Manager", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void CashierLoginwithCorrectCredentials()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            tbl_Login_details ob = new tbl_Login_details();
            ob.username = "Cashier";

            // Act
            RedirectToRouteResult result = controller.Login(ob, "bb@ch") as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Cashier", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void LoginwithWrongUsernameCredentials()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            tbl_Login_details ob = new tbl_Login_details();
            ob.username = "abcd";

            // Act
            ViewResult result = controller.Login(ob, "bb@ch") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Invalid Credentials", result.TempData["Message"]);
            Assert.AreEqual("Login", result.ViewName);
        }

        [TestMethod]
        public void LoginwithWrongPasswordCredentials()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            tbl_Login_details ob = new tbl_Login_details();
            ob.username = "Cashier";

            // Act
            ViewResult result = controller.Login(ob, "bb") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Invalid Password", result.ViewBag.Message);
            Assert.AreEqual("Login", result.ViewName);
        }

        [TestMethod]
        public void NoCredentials()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            tbl_Login_details ob = new tbl_Login_details();
            ob.username = "";

            // Act
            ViewResult result = controller.Login(ob, "") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Invalid Credentials", result.TempData["message"]);
            Assert.AreEqual("Login", result.ViewName);
        }

        [TestMethod]
        public void ViewUnauthorized()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.Unauthorized() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddCustomerPage()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.AddNewCustomer() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddCustomerDetails()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            tbl_Customer_Details ob = new tbl_Customer_Details();
            ob.CustomerName = "Test";
            ob.SSN_ID = 856475125;
            ob.DateOfBirth = Convert.ToDateTime("01/01/1999 00:00:00");
            ob.AddressLine1 = "Line1";
            ob.AddressLine2 = "Line2";
            FormCollection form = new FormCollection();
            form.Add("dropdownStates", "1");
            form.Add("dropdownCities", "1");

            // Act
            RedirectToRouteResult result = controller.AddNewCustomer(ob,form ) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListCustomer", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notice"));
            Assert.AreEqual("Customer Details Inserted Successfully", controller.TempData["notice"]);

        }

        [TestMethod]
        public void UpdateCustomerPage()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.UpdateCustomer(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "UpdateCustomer");
        }

        [TestMethod]
        public void UpdateCustomerDetails()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            tbl_Customer_Details ob = new tbl_Customer_Details();
            ob.CustomerID = 15;
            ob.CustomerName = "Test";
            ob.SSN_ID = 856475125;
            ob.DateOfBirth = Convert.ToDateTime("01/01/1999 00:00:00");
            ob.AddressLine1 = "Line1";
            ob.AddressLine2 = "Line3";
            FormCollection form = new FormCollection();
            form.Add("dropdownstate", "1");
            form.Add("dropdowncity", "1");

            // Act
            RedirectToRouteResult result = controller.UpdateCustomer(ob, form) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListCustomer", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notice"));
            Assert.AreEqual("Customer Details updated Successfully", controller.TempData["notice"]);

        }

        [TestMethod]
        public void DeleteCustomerPage()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.DeleteCustomer(15) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "DeleteCustomer");
        }

        [TestMethod]
        public void DeleteCustomerDetailsHavingNoAccts()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            sp_ViewCustomersForDeleteByID_Result ob = new sp_ViewCustomersForDeleteByID_Result();
            ob.CustomerID = 15;

            // Act
            RedirectToRouteResult result = controller.DeleteCustomer(ob) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListCustomer", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notice"));
            Assert.AreEqual("Customer with 15 is Deleted Succesfully", controller.TempData["notice"]);

        }

        [TestMethod]
        public void DeleteCustomerDetailsHavingAccts()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            sp_ViewCustomersForDeleteByID_Result ob = new sp_ViewCustomersForDeleteByID_Result();
            ob.CustomerID = 1;

            // Act
            RedirectToRouteResult result = controller.DeleteCustomer(ob) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListCustomer", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notice"));
            Assert.AreEqual("Customer Account is Active", controller.TempData["notice"]);

        }

        [TestMethod]
        public void ListCustomerPage()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Details ob = new tbl_Customer_Details();

            // Act
            ViewResult result = controller.ListCustomer(ob,"","") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListCustomer");
        }

        [TestMethod]
        public void SearchCustomeronCorrectSSN()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Details ob = new tbl_Customer_Details();

            // Act
            ViewResult result = controller.ListCustomer(ob, "SSN_ID", "123456789") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListCustomer");
        }

        [TestMethod]
        public void SearchCustomeronIncorrectSSN()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Details ob = new tbl_Customer_Details();

            // Act
            ViewResult result = controller.ListCustomer(ob, "SSN_ID", "123456780") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListCustomer");
            Assert.AreEqual("No Records Found", result.ViewBag.Message);
        }

        [TestMethod]
        public void SearchCustomeronCorrectCustomerID()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Details ob = new tbl_Customer_Details();

            // Act
            ViewResult result = controller.ListCustomer(ob, "CustomerID", "1") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListCustomer");
        }

        [TestMethod]
        public void SearchCustomeronIncorrectCustomerID()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Details ob = new tbl_Customer_Details();

            // Act
            ViewResult result = controller.ListCustomer(ob, "CustomerID", "25") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListCustomer");
            Assert.AreEqual("No Records Found", result.ViewBag.Message);
        }

        [TestMethod]
        public void AddAccountPage()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.AddAccount() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "AddAccount");
        }

        [TestMethod]
        public void AccountalreadyExists()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.CustomerId = 1;
            ob.AccountTypeID = 1;
            ob.Balance = 600;

            // Act
            ViewResult result = controller.AddAccount(ob) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "AddAccount");
            Assert.AreEqual("Account already exists", result.ViewBag.Message);
        }

        [TestMethod]
        public void CustomerDoesntExists()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.CustomerId = 25;
            ob.AccountTypeID = 1;
            ob.Balance = 600;

            // Act
            ViewResult result = controller.AddAccount(ob) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "AddAccount");
            Assert.AreEqual("Customer does not exist", result.ViewBag.Message);
        }

        [TestMethod]
        public void MinBalanceCheck()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.CustomerId = 16;
            ob.AccountTypeID = 1;
            ob.Balance = 400;

            // Act
            ViewResult result = controller.AddAccount(ob) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "AddAccount");
            Assert.AreEqual("Minimum balance for Savings A/C is $500 and Current A/C is $1000", result.ViewBag.Message);
        }

        [TestMethod]
        public void SuccessfulAccountCreationCheck()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.CustomerId = 16;
            ob.AccountTypeID = 1;
            ob.Balance = 600;

            // Act
            RedirectToRouteResult result = controller.AddAccount(ob) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListAccount", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("Message"));
            Assert.AreEqual("Account Created Succesfully", controller.TempData["Message"]);
        }

        [TestMethod]
        public void DeleteAccountPage()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.DeleteAccount("1") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "DeleteAccount");
        }

        [TestMethod]
        public void DeleteAccountHavingZeroBalance()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            sp_ViewAccountsByID_Result ob = new sp_ViewAccountsByID_Result();
            ob.AccountNumber = "1234089665";
            ob.Balance = 0;

            // Act
            RedirectToRouteResult result = controller.DeleteAccount(ob) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListAccount", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("Message"));
            Assert.AreEqual("Account deleted successfully", controller.TempData["Message"]);

        }

        [TestMethod]
        public void DeleteAccountHavingNonZeroBalance()
        {
            // Arrange
            FBSEntities objdb = new FBSEntities();
            TestController controller = new TestController();
            sp_ViewAccountsByID_Result ob = new sp_ViewAccountsByID_Result();
            ob.AccountNumber = "1234089665";
            ob.Balance = 600;

            // Act
            RedirectToRouteResult result = controller.DeleteAccount(ob) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListAccount", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("Message1"));
            Assert.AreEqual("Please check the account balance", controller.TempData["Message1"]);

        }

        [TestMethod]
        public void ListAccountPage()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccount(ob, "", "") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccount");
        }

        [TestMethod]
        public void SearchAccountonCorrectAcctNo()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccount(ob, "AccountNumber", "1234022928") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccount");
        }

        [TestMethod]
        public void SearchAccountonIncorrectAcctNo()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccount(ob, "AccountNumber", "1234022920") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccount");
            Assert.AreEqual("No Records Found", result.ViewBag.Message);
        }

        [TestMethod]
        public void SearchAccountonCorrectCustomerID()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccount(ob, "CustomerId", "1") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccount");
        }

        [TestMethod]
        public void SearchAccountonIncorrectCustomerID()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccount(ob, "CustomerId", "25") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccount");
            Assert.AreEqual("No Records Found", result.ViewBag.Message);
        }

        [TestMethod]
        public void ListAccountForCashierPage()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccountCashier(ob, "", "") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccountCashier");
        }

        [TestMethod]
        public void SearchAccountForCashieronCorrectAcctNo()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccountCashier(ob, "AccountNumber", "1234022928") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccountCashier");
        }

        [TestMethod]
        public void SearchAccountForCashieronIncorrectAcctNo()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccountCashier(ob, "AccountNumber", "1234022920") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccountCashier");
            Assert.AreEqual("No Records Found", result.ViewBag.Message);
        }

        [TestMethod]
        public void SearchAccountForCashieronCorrectCustomerID()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccountCashier(ob, "CustomerId", "1") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccountCashier");
        }

        [TestMethod]
        public void SearchAccountForCashieronIncorrectCustomerID()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();

            // Act
            ViewResult result = controller.ListAccountCashier(ob, "CustomerId", "25") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "ListAccountCashier");
            Assert.AreEqual("No Records Found", result.ViewBag.Message);
        }

        [TestMethod]
        public void DepositPage()
        {
            // Arrange
            TestController controller = new TestController();
            long id = 1234022928;
            decimal bal = 500;

            // Act
            ViewResult result = controller.Deposit(id, bal) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Deposit");
        }

        [TestMethod]
        public void DepositIncorrectAmount()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.AccountNumber = "1234022928";
            ob.Balance = 500;
            FormCollection form = new FormCollection();
            form.Add("Deposit", "-100");

            // Act
            ViewResult result = controller.Deposit(ob, form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Deposit");
            Assert.AreEqual("Sorry! Please check the Amount entered", result.TempData["Message"]);
        }

        [TestMethod]
        public void DepositCorrectAmount()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.AccountNumber = "1234022928";
            ob.Balance = 500;
            FormCollection form = new FormCollection();
            form.Add("Deposit", "100");

            // Act
            RedirectToRouteResult result = controller.Deposit(ob, form) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListAccountCashier", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("Messagesuccess"));
            Assert.AreEqual("Deposit done Successfully", controller.TempData["Messagesuccess"]);
        }

        [TestMethod]
        public void WithdrawPage()
        {
            // Arrange
            TestController controller = new TestController();
            long id = 1234393092;
            decimal bal = 1020;

            // Act
            ViewResult result = controller.Withdraw(id, bal) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Withdraw");
        }

        [TestMethod]
        public void TransferPage()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.Transfer() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Transfer");
        }

        [TestMethod]
        public void WithdrawIncorrectAmount()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.AccountNumber = "1234393092";
            ob.Balance = 1020;
            FormCollection form = new FormCollection();
            form.Add("WithdrawAmount", "-100");

            // Act
            ViewResult result = controller.Withdraw(ob, form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Withdraw");
            Assert.AreEqual("Sorry! Please check the Amount entered", result.TempData["Message"]);
        }

        [TestMethod]
        public void TransferIncorrectAmount()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_TransactionDetails ob = new tbl_TransactionDetails();
            ob.SourceAccNum = "1234393092";
            ob.DestinationAccNum = "1234856676";
            FormCollection form = new FormCollection();
            form.Add("Amount to be Transferred", "-100");

            // Act
            ViewResult result = controller.Transfer(ob, form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Transfer");
            Assert.AreEqual("Sorry! Please check the Amount entered", result.ViewBag.Message);
        }



        [TestMethod]
        public void TransferIncorrectSrcAcctNum()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_TransactionDetails ob = new tbl_TransactionDetails();
            ob.SourceAccNum = "1234393456";
            ob.DestinationAccNum = "1234856676";
            FormCollection form = new FormCollection();
            form.Add("Amount to be Transferred", "100");

            // Act
            ViewResult result = controller.Transfer(ob, form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Transfer");
            Assert.AreEqual("Source account doesn't exist", result.ViewBag.Message);
        }

        [TestMethod]
        public void TransferIncorrectDestAcctNum()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_TransactionDetails ob = new tbl_TransactionDetails();
            ob.SourceAccNum = "1234393092";
            ob.DestinationAccNum = "1234856999";
            FormCollection form = new FormCollection();
            form.Add("Amount to be Transferred", "100");

            // Act
            ViewResult result = controller.Transfer(ob, form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Transfer");
            Assert.AreEqual("Destination account doesn't exist", result.ViewBag.Message);
        }

        [TestMethod]
        public void WithdrawInsufficientFunds()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.AccountNumber = "1234393092";
            ob.Balance = 1020;
            FormCollection form = new FormCollection();
            form.Add("WithdrawAmount", "100000");

            // Act
            ViewResult result = controller.Withdraw(ob, form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Withdraw");
            Assert.AreEqual("Sorry! Insufficient funds", result.TempData["Message"]);
        }

        [TestMethod]
        public void TransferInsufficientFunds()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_TransactionDetails ob = new tbl_TransactionDetails();
            ob.SourceAccNum = "1234393092";
            ob.DestinationAccNum = "1234856676";
            FormCollection form = new FormCollection();
            form.Add("Amount to be Transferred", "1000000");

            // Act
            ViewResult result = controller.Transfer(ob, form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Transfer");
            Assert.AreEqual("Sorry! Insufficient funds", result.ViewBag.Message);
        }

        [TestMethod]
        public void WithdrawCorrectAmount()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_Customer_Account_Details ob = new tbl_Customer_Account_Details();
            ob.AccountNumber = "1234393092";
            ob.Balance = 1020;
            FormCollection form = new FormCollection();
            form.Add("WithdrawAmount", "20");

            // Act
            RedirectToRouteResult result = controller.Withdraw(ob, form) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListAccountCashier", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("Messagesuccess"));
            Assert.AreEqual("Withdraw done Successfully", controller.TempData["Messagesuccess"]);
        }

        [TestMethod]
        public void TransferCorrectAmount()
        {
            // Arrange
            TestController controller = new TestController();
            tbl_TransactionDetails ob = new tbl_TransactionDetails();
            ob.SourceAccNum = "1234393092";
            ob.DestinationAccNum = "1234856676";
            FormCollection form = new FormCollection();
            form.Add("Amount to be Transferred", "100");

            // Act
            RedirectToRouteResult result = controller.Transfer(ob, form) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ListAccountCashier", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("Messagesuccess"));
            Assert.AreEqual("Transfer done Successfully", controller.TempData["Messagesuccess"]);
        }

            [TestMethod]
        public void MinistatementPage()
        {
            // Arrange
            TestController controller = new TestController();
            string option = "";
            string Start_Date = "";
            string End_Date = "";
            string ddSectionlsts = "";
            String search = "";

            // Act
            ViewResult result = controller.Ministatement(option, Start_Date, End_Date, ddSectionlsts, search) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Ministatement");
        }

        [TestMethod]
        public void MinistatementNullDateSearch()
        {
            // Arrange
            TestController controller = new TestController();
            string option = "Date";
            string Start_Date = "";
            string End_Date = "";
            string ddSectionlsts = "";
            String search = "";

            // Act
            ViewResult result = controller.Ministatement(option, Start_Date, End_Date, ddSectionlsts, search) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Ministatement");
            Assert.AreEqual("Please select the dates!", result.ViewBag.Message);
        }

        [TestMethod]
        public void MinistatementNullTranCntSearch()
        {
            // Arrange
            TestController controller = new TestController();
            string option = "NoOfTransactions";
            string Start_Date = "";
            string End_Date = "";
            string ddSectionlsts = "";
            String search = "";

            // Act
            ViewResult result = controller.Ministatement(option, Start_Date, End_Date, ddSectionlsts, search) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Ministatement");
            Assert.AreEqual("Select number of transactions!", result.ViewBag.Message);
        }

        [TestMethod]
        public void MinistatementDateSearch()
        {
            // Arrange
            TestController controller = new TestController();
            string option = "Date";
            string Start_Date = "11/01/2020";
            string End_Date = "11/13/2020";
            string ddSectionlsts = "";
            String search = "1234393092";

            // Act
            ViewResult result = controller.Ministatement(option, Start_Date, End_Date, ddSectionlsts, search) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Ministatement");
            Assert.AreNotEqual("No records found", result.ViewBag.Message);
        }

        [TestMethod]
        public void MinistatementTranCntSearch()
        {
            // Arrange
            TestController controller = new TestController();
            string option = "NoOfTransactions";
            string Start_Date = "";
            string End_Date = "";
            string ddSectionlsts = "5";
            String search = "1234393092";

            // Act
            ViewResult result = controller.Ministatement(option, Start_Date, End_Date, ddSectionlsts, search) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Ministatement");
            Assert.AreNotEqual("No records found", result.ViewBag.Message);
        }
    }
}
