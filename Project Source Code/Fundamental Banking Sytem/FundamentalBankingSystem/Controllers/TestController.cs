using FundamentalBankingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FundamentalBankingSystem.Controllers
{
    public class TestController : Controller
    {
        //Created database entity object thorugh which we can call relations and stored procedures.
        FBSEntities objdb = new FBSEntities();
        // GET: Test
        public ActionResult Home() // HttpGet method which opens home page on startup as configured in route config.
        {
            return View();
        }

        public ActionResult FAQ() // HttpGet method which opens FAQ page
        {
            return View();
        }

        public ActionResult AboutUs() // HttpGet method which opens Aboutus page
        {
            return View();
        }

        public ActionResult Login() // HttpGet method which opens login page 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_Login_details obj, string password)
        {
            if (ModelState.IsValid) // check if any model errors have been added
                try
                {
                    //Fetches the details based on username from DB.(LINQ Query)
                    obj = objdb.tbl_Login_details.Where(a => a.username.Equals(obj.username)).FirstOrDefault();
                    if (obj != null) // check whether the object returned from DB is not equals to null
                    {
                        // convert the password into bytes
                        byte[] bytes = Encoding.Unicode.GetBytes(password);
                        //created an SHA256 encrypion object
                        SHA256Managed hashstring = new SHA256Managed();
                        // compute the hash from the password bytes
                        byte[] hash = hashstring.ComputeHash(bytes);
                        string hashString = string.Empty;
                        // hash is converted to string using foreach
                        foreach (byte x in hash)
                        {
                            hashString += string.Format("{0:x2}", x);
                        }
                        // check whether the current hashed password matches the DB hash for the username
                        int loginid = Convert.ToInt32(objdb.sp_checkLogin(hashString).FirstOrDefault());
                        if (loginid > 0)
                        {
                            // check whether roleid is 1 or 2
                            if (loginid == 1)
                            {
                                // if 1 redirects to manager
                                return RedirectToAction("Manager","Home");
                            }
                            else if (loginid == 2)
                            {
                                // if 2 redirects cashier
                                return RedirectToAction("Cashier","Home");
                            }
                            else
                            {
                                // display an error message as the current hashed password not matched the DB.
                                ViewBag.Message = "Invalid Password";
                                // redirects to login to renenter the credentials.
                                return View("Login","Home");
                            }
                        }
                        else
                        {
                            // display an error message as the current hashed password not matched the DB.
                            ViewBag.Message = "Invalid Password";
                            // redirects to login to renenter the credentials.
                            return View("Login", "Home");
                        }
                    }
                    else
                    {
                        // display error meesage as null object is returned from DB
                        TempData["Message"] = "Invalid Credentials";
                        // redirects to login to enter credentials again
                        return View("Login", "Home");
                    }
                }
                catch (Exception e)
                {
                    //in case of any excpetion redirect to the same page
                    return View("Login", "Home");
                }
            return View("Login", "Home");
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Manager() // Opens manager page
        {
            return View();
        }

        public ActionResult Cashier() // Opens cashier page
        {
            return View();
        }

        public JsonResult GetStates() // get the states for cascading dropdown
        {
            return Json(objdb.sp_getstates().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCitiesByStatesId(string Stateid) // get the cities based on selected state for cascading dropdown
        {
            int Id = Convert.ToInt32(Stateid);
            return Json(objdb.sp_getcities(Id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCustomer(tbl_Customer_Details objcust, FormCollection form)
        {
            if (ModelState.IsValid)
                try
                {
                    int res = 0;
                    if (form != null)
                    {
                        DateTime bday = Convert.ToDateTime(objcust.DateOfBirth);
                        DateTime today = DateTime.Today;
                        int age = today.Year - bday.Year; // Calculating the age

                        if (age < 18)
                        {
                            ViewBag.MessageDate = "Customer is not old enough to open an Account";
                            return View();
                        }
                        else
                        {
                            objcust.StateID = Convert.ToInt32(form["dropdownStates"].ToString());
                            objcust.CityID = Convert.ToInt32(form["dropdownCities"].ToString());
                            //Insertion of customer details to db
                            res = objdb.sp_InsertCustomerDetails(objcust.CustomerName, objcust.SSN_ID, objcust.DateOfBirth, objcust.AddressLine1, objcust.AddressLine2, objcust.StateID, objcust.CityID);
                            this.objdb.SaveChanges();
                            TempData["notice"] = "Customer Details Inserted Successfully";
                            return RedirectToAction("ListCustomer");
                        }
                    }
                }
                catch (Exception e)
                {
                    return View();
                }
            return View();
        }

        public ActionResult UpdateCustomer(int? id)
        {
            try
            {
                sp_ViewCustomersByID_Result obj = new sp_ViewCustomersByID_Result();

                if (id != null)
                {
                    obj = objdb.sp_ViewCustomersByID(id).FirstOrDefault();
                    List<sp_getstates_Result> state = new List<sp_getstates_Result>();
                    state = objdb.sp_getstates().ToList();
                    List<sp_getcities_Result> city = new List<sp_getcities_Result>();
                    city = objdb.sp_getcities(obj.StateID).ToList();
                    ViewBag.dropdownstate = new SelectList(state, "Stateid", "Statename", obj.StateID);
                    ViewBag.dropdowncity = new SelectList(city, "Cityid", "Cityname", obj.CityID);
                }
                //load the customer details
                return View(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult UpdateCustomer(tbl_Customer_Details obj, FormCollection form)
        {
            try
            {
                obj.StateID = Convert.ToInt32(form["dropdownstate"].ToString());
                obj.CityID = Convert.ToInt32(form["dropdowncity"].ToString());

                List<sp_getstates_Result> state = new List<sp_getstates_Result>();
                state = objdb.sp_getstates().ToList();
                List<sp_getcities_Result> city = new List<sp_getcities_Result>();
                city = objdb.sp_getcities(obj.StateID).ToList();
                DateTime dateTime = obj.DateOfBirth;
                DateTime dt = Convert.ToDateTime(form["DateOfBirth"]);
                string q = dateTime.ToString();
                if (q == "1/1/0001 12:00:00 AM")
                {
                    ViewBag.dropdownstate = new SelectList(state, "Stateid", "Statename", obj.StateID);
                    ViewBag.dropdowncity = new SelectList(city, "Cityid", "Cityname", obj.CityID);

                    return View();
                }
                DateTime today = DateTime.Today;
                int age = today.Year - dateTime.Year;
                if (age > 18 && age < 100)
                {
                    //update customer details to DB
                    objdb.sp_UpdateCustomerDetails(obj.CustomerID, obj.CustomerName, obj.SSN_ID, obj.DateOfBirth, obj.AddressLine1, obj.AddressLine2, obj.StateID, obj.CityID);
                    this.objdb.SaveChanges();
                    TempData["notice"] = "Customer Details updated Successfully";
                    return RedirectToAction("ListCustomer");
                }
                else
                {
                    ViewBag.dropdownstate = new SelectList(state, "Stateid", "Statename", obj.StateID);
                    ViewBag.dropdowncity = new SelectList(city, "Cityid", "Cityname", obj.CityID);
                    ViewBag.Message = "Minimum age for having an account is 18";
                    return View();
                }
            }

            catch (Exception e)
            {
                TempData["Message1"] = "Something went wrong! Please try again later";
                return RedirectToAction("ListCustomer");
            }
        }

        public ActionResult DeleteCustomer(int? id)
        {
            sp_ViewCustomersForDeleteByID_Result obj = new sp_ViewCustomersForDeleteByID_Result();

            if (id != null)
            {
                //load the details of customer to be deleted
                obj = objdb.sp_ViewCustomersForDeleteByID(id).FirstOrDefault();
            }
            return View(obj);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(sp_ViewCustomersForDeleteByID_Result obj)
        {
            try
            {
                //Deleting the customer from DB
                int res = Convert.ToInt32(objdb.sp_DeleteCustomerByID(obj.CustomerID).FirstOrDefault());
                //If the customer has accounts associated
                if (res == 1)
                {
                    TempData["notice"] = "Customer Account is Active";
                }
                //If customer has no accounts
                else if (res == 2)
                {
                    TempData["notice"] = "Customer with " + obj.CustomerID + " is Deleted Succesfully";
                }
                else
                {
                    TempData["notice"] = "Something went wrong! Please try again later";
                }
                //Save changes to DB
                this.objdb.SaveChanges();
                return RedirectToAction("ListCustomer");
            }
            catch (Exception e)
            {
                TempData["Message1"] = "Something went wrong! Please try again later";
                return RedirectToAction("ListCustomer");
            }
        }

        public ActionResult ListCustomer(tbl_Customer_Details obj, string option, string search)
        {
            try
            {
                List<sp_SearchCustomer_Result> list1 = new List<sp_SearchCustomer_Result>();
                List<sp_SearchCustomer_Result> li = new List<sp_SearchCustomer_Result>();
                //load all the customers
                li = objdb.sp_SearchCustomer(0, null, null).ToList();
                if (search == null || option == null || search == "")
                {
                    return View(li);
                }

                if (option == "SSN_ID")
                {
                    long c = Convert.ToInt64(search);
                    //load customer with specified SSN value
                    list1 = objdb.sp_SearchCustomer(2, null, c).ToList();

                    if (list1.Count() != 0)
                    {
                        option = null;
                    }
                    else
                    {
                        ViewBag.Message = "No Records Found";
                        return View(li);
                    }
                    return View(list1);
                }
                else if (option == "CustomerID")
                {
                    long b = Convert.ToInt64(search);
                    //load customer with specific customer id
                    list1 = objdb.sp_SearchCustomer(1, b, null).ToList();
                    if (list1.Count() != 0)
                    {
                    }
                    else
                    {
                        ViewBag.Message = "No Records Found";
                        return View(li);
                    }
                    return View(list1);
                }
                else
                {
                    return View(li);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Something went wrong!";
                return View();
            }

        }

        public ActionResult AddAccount()
        {
            //load account types
            ViewBag.list = objdb.tbl_Account_Types.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddAccount(tbl_Customer_Account_Details obj)
        {
            if (ModelState.IsValid)
                try
                {
                    ViewBag.list = objdb.tbl_Account_Types.ToList();
                    //generating accountnumber
                    String startWith = "1234";
                    Random generator = new Random();
                    String r = generator.Next(0, 999999).ToString("D6");
                    String AccountNumber = startWith + r;

                    //Insert account details into DB
                    int res = Convert.ToInt32(objdb.sp_InsertAccountDetails(obj.CustomerId, AccountNumber, obj.AccountTypeID, obj.Balance).FirstOrDefault());

                    if (res == 2)
                    {
                        ViewBag.Message = "Account already exists";
                        return View();
                    }

                    else if (res == 5)
                    {
                        ViewBag.Message = "Customer does not exist";
                        return View();
                    }

                    else if (res == 6)
                    {
                        ViewBag.Message = "Maximum accounts for a customer can be two";
                        return View();
                    }

                    else if (res == 7)
                    {
                        ViewBag.Message = "Minimum balance for Savings A/C is $500 and Current A/C is $1000";
                        return View();
                    }

                    else
                    {
                        this.objdb.SaveChanges();
                        TempData["Message"] = "Account Created Succesfully";
                        return RedirectToAction("ListAccount");
                    }
                }

                catch (Exception e)
                {
                    return View();
                }
            return View();
        }

        public ActionResult DeleteAccount(String id)
        {
            sp_ViewAccountsByID_Result objj = new sp_ViewAccountsByID_Result();
            if (id != null)
            {
                objj = objdb.sp_ViewAccountsByID(id).FirstOrDefault();
            }
            //load account details to be deleted
            return View(objj);
        }

        [HttpPost]
        public ActionResult DeleteAccount(sp_ViewAccountsByID_Result objj)
        {
            try
            {
                //Delete account from the DB
                int res = Convert.ToInt32(objdb.sp_DeleteAccount(objj.AccountNumber, objj.Balance).FirstOrDefault());
                //If the account balance isn't zero
                if (res == 1)
                {
                    TempData["Message1"] = "Please check the account balance";
                }
                else
                {
                    this.objdb.SaveChanges();
                    TempData["Message"] = "Account deleted successfully";
                    return RedirectToAction("ListAccount");
                }

            }
            catch (Exception e)
            {
                TempData["Message1"] = "Something went wrong! Please try again later";
                return RedirectToAction("ListAccount");
            }
            return RedirectToAction("ListAccount");
        }

        public ActionResult ListAccount(tbl_Customer_Account_Details obj, string option, string search)
        {
            try
            {
                List<sp_SearchAccount_Result> list1 = new List<sp_SearchAccount_Result>();
                List<sp_SearchAccount_Result> li = new List<sp_SearchAccount_Result>();
                //load all accounts
                li = objdb.sp_SearchAccount(0, null, null).ToList();
                if (search == null || option == null || search == "")
                {
                    return View(li);
                }

                if (option == "CustomerId")
                {
                    long c = Convert.ToInt64(search);
                    //load accounts specific to the customer id
                    list1 = objdb.sp_SearchAccount(2, c, null).ToList();

                    if (list1.Count() != 0)
                    {
                        option = null;
                    }
                    else
                    {
                        ViewBag.Message = "No Records Found";
                        return View(li);
                    }
                    return View(list1);
                }
                else if (option == "AccountNumber")
                {
                    //load accounts based on account number
                    list1 = objdb.sp_SearchAccount(1, null, search).ToList();
                    if (list1.Count() != 0)
                    {
                    }
                    else
                    {
                        ViewBag.Message = "No Records Found";
                        return View(li);
                    }
                    return View(list1);
                }
                else
                {
                    return View(li);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Something went wrong!";
                return View();
            }
        }

        public ActionResult ListAccountCashier(tbl_Customer_Account_Details obj, string option, string search)
        {
            try
            {
                List<sp_SearchAccount_Result> list1 = new List<sp_SearchAccount_Result>();
                List<sp_SearchAccount_Result> li = new List<sp_SearchAccount_Result>();
                //load all accounts
                li = objdb.sp_SearchAccount(0, null, null).ToList();
                if (search == null || option == null || search == "")
                {
                    return View(li);
                }

                if (option == "CustomerId")
                {
                    long c = Convert.ToInt64(search);
                    //load accounts specific to the customer id
                    list1 = objdb.sp_SearchAccount(2, c, null).ToList();

                    if (list1.Count() != 0)
                    {
                        option = null;
                    }
                    else
                    {
                        ViewBag.Message = "No Records Found";
                        return View(li);
                    }
                    return View(list1);
                }
                else if (option == "AccountNumber")
                {
                    //load accounts based on account number
                    list1 = objdb.sp_SearchAccount(1, null, search).ToList();
                    if (list1.Count() != 0)
                    {
                    }
                    else
                    {
                        ViewBag.Message = "No Records Found";
                        return View(li);
                    }
                    return View(list1);
                }
                else
                {
                    return View(li);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Something went wrong!";
                return View();
            }
        }

        public ActionResult Deposit(long? id, decimal? balance)
        {
            try
            {
                ViewBag.id = id;
                ViewData["balance"] = balance;
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult Deposit(tbl_Customer_Account_Details obj, FormCollection form)
        {
            try
            {

                ViewBag.id = obj.AccountNumber;
                ViewData["balance"] = obj.Balance;
                string transactiontype = "Deposit";
                decimal amt = Convert.ToDecimal(form["Deposit"]);
                ViewBag.Amt = form["Deposit"];
                if (amt <= 0)
                {
                    TempData["Message"] = "Sorry! Please check the Amount entered";
                    return View();
                }
                int res = Convert.ToInt32(objdb.sp_DepositandWithdraw(obj.AccountNumber, transactiontype, Convert.ToDecimal(form["Deposit"])).FirstOrDefault());

                if (res == 0)
                {
                    TempData["Messagesuccess"] = "Deposit done Successfully";
                    return RedirectToAction("ListAccountCashier");
                }
                else if (res == 2)
                {
                    TempData["Message"] = "Account does not exist";
                    return View();
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = "Check the details entered";
                return View();
            }
        }

        public ActionResult Withdraw(long? id, decimal? balance)
        {
            try
            {
                ViewBag.id = id;
                ViewData["balance"] = balance;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = "Something went wrong!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Withdraw(tbl_Customer_Account_Details obj, FormCollection form)
        {
            try
            {
                ViewBag.id = obj.AccountNumber;
                ViewData["balance"] = obj.Balance;
                string transactiontype = "Withdraw";
                decimal amt = Convert.ToDecimal(form["WithdrawAmount"]);
                ViewBag.Amt = form["WithdrawAmount"];
                if (amt <= 0)
                {
                    TempData["Message"] = "Sorry! Please check the Amount entered";
                    return View();
                }
                int res = Convert.ToInt32(objdb.sp_DepositandWithdraw(obj.AccountNumber, transactiontype, Convert.ToDecimal(form["WithdrawAmount"])).FirstOrDefault());

                if (res == 1)
                {
                    TempData["Messagesuccess"] = "Withdraw done Successfully";
                    return RedirectToAction("ListAccountCashier");
                }

                else if (res == 6)
                {
                    TempData["Message"] = "Sorry! Insufficient funds";
                    return View();
                }

                else if (res == 2)
                {
                    TempData["Message"] = "Account Does not exist";
                    return View();
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = "Check the details entered";
                return View();
            }
        }

        public ActionResult Ministatement(string option, string Start_Date, string End_Date, string ddSectionlsts, String search)
        {
            try
            {
                ViewBag.id = search;
                string selected = option;
                ViewBag.Showdiv = false;
                List<sp_MiniStatement_Result> list = new List<sp_MiniStatement_Result>();
                if (option == "Date")
                {
                    if (Start_Date != "" && End_Date != "")
                    {
                        DateTime startdate = Convert.ToDateTime(Start_Date);
                        DateTime enddate = Convert.ToDateTime(End_Date);
                        list = objdb.sp_MiniStatement(search, startdate, enddate, null, selected).ToList();
                        if (list.Count() != 0)
                        {
                            ViewBag.Showdiv = true;
                            return View(list);
                        }
                        else
                        {
                            ViewBag.message = "No records found";
                            //return View();
                        }
                    }
                    else
                    {
                        ViewBag.message = "Please select the dates!";
                    }

                }
                else if (option == "NoOfTransactions")
                {
                    if (ddSectionlsts != "")
                    {
                        int a = Convert.ToInt32(ddSectionlsts);

                        list = objdb.sp_MiniStatement(search, null, null, a, selected).ToList();
                        if (list.Count() != 0)
                        {
                            ViewBag.Showdiv = true;
                            return View(list);
                        }
                        else
                        {
                            ViewBag.message = "No records found";
                            //return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Select number of transactions!";
                    }

                }
                else
                {



                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = "Enter in correct format ";
                return View();
            }
        }
        public ActionResult Transfer()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Message = "Something went wrong!";
                return View();
            }
        }


        public ActionResult Transfer(tbl_TransactionDetails obj, FormCollection form)
        {
            try
            {
                string transactiontype = "transfer";
                ViewBag.SrcAcct = obj.SourceAccNum;
                ViewData["DestAcct"] = obj.DestinationAccNum;
                decimal amount = Convert.ToInt64(form["Amount to be Transferred"]);
                ViewBag.Amt = form["Amount to be Transferred"];
                if (amount <= 0)
                {
                    ViewBag.Message = "Sorry! Please check the Amount entered";
                    return View();
                }
                int res = Convert.ToInt32(objdb.sp_Transfer(obj.SourceAccNum, obj.DestinationAccNum, amount, transactiontype).FirstOrDefault());
                this.objdb.SaveChanges();

                if (res == 0)
                {
                    TempData["Messagesuccess"] = "Transfer done Successfully";
                    return RedirectToAction("ListAccountCashier");
                }

                else if (res == 2)
                {
                    ViewBag.Message = "Sorry! Insufficient funds";
                    return View();
                }
                else if (res == 3)
                {
                    ViewBag.Message = "Source account doesn't exist";
                    return View();
                }
                else if (res == 4)
                {
                    ViewBag.Message = "Destination account doesn't exist";
                    return View();
                }
                else
                {
                    return View(obj.SourceAccNum, obj.DestinationAccNum);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Check the details entered";
                return View();
            }
        }
    }
}