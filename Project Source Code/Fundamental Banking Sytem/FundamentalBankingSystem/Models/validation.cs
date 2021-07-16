using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FundamentalBankingSystem.Models
{
    [MetadataType(typeof(tbl_Login_detailsmetadata))]
    public partial class tbl_Login_details
    {

    }
    public class tbl_Login_detailsmetadata
    {
        public int loginid { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public int roleid { get; set; }
    }

    public partial class tbl_Customer_Account_Details
    {

    }
    public class tbl_Customer_Account_Detailsmetadata
    {
        [Required]
        public Nullable<long> CustomerId { get; set; }
        public long AccountNumber { get; set; }
        [Required]
        public Nullable<int> AccountTypeId { get; set; }
        [Required]
        public Nullable<decimal> Balance { get; set; }

        public virtual tbl_Account_Types Tbl_Account_Types { get; set; }
    }
    [MetadataType(typeof(tbl_Customer_Detailsmetadata))]
    public partial class tbl_Customer_Details
    {

    }
    public class tbl_Customer_Detailsmetadata
    {
        public long CustomerID { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z- ]+$", ErrorMessage = "Please check the name entered")]
        public string CustomerName { get; set; }
        [Required]
        [RegularExpression("^[1-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$", ErrorMessage = "SSN-ID should be of 9 digits ")]
        public long SSN_ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please select atleast one option")]
        public int StateID { get; set; }
        [Required(ErrorMessage = "Please select atleast one option")]
        public int CityID { get; set; }

    }
}