using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace IT_Comp.Models
{
    [MetadataType(typeof(MetaEmployees))]
    public partial class employee
    {
        [Display(Name = "Confirm Email")]
        [Compare("email", ErrorMessage = "Email mismatch")]
        public string confEmail { set; get; }
        [Display(Name = "Confirm Password")]
        public string confPassword { set; get; }
        [Display(Name = "Active User")]
        public bool active { set; get; }

    }
    public class MetaEmployees
    {
        [Display(Name = "Number")]
        [DisplayFormat(DataFormatString = "{0:D3}", ApplyFormatInEditMode = false)]
        public int eno { get; set; }




        [Display(Name = "Name")]
        [Required(ErrorMessage = "please enter Employee name")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 10 characters")]
        public string ename { get; set; }



        [Display(Name = "Gender")]
        public string gender { get; set; }




        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Select a city")]
        public string city { get; set; }




        [Display(Name = "BirthYear")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:Y}", ApplyFormatInEditMode = false)]
        public Nullable<System.DateTime> byear { get; set; }




        [Display(Name = "Salary")]
        [DisplayFormat(DataFormatString = "{0:N3}", ApplyFormatInEditMode = false)]
        [Range(200, 5000, ErrorMessage = "Salary must be between 200 and 5000")]
        public Nullable<decimal> salary { get; set; }




        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter Your Email!! ")]
        [EmailAddress(ErrorMessage = "Enter a vaild email address")]
        public string email { get; set; }





        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter passaword!!")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{4,10})", ErrorMessage = "password must contain a letter, digit and symbol")]
        public string password { get; set; }



        [Display(Name = "Picture")]
        public string picture { get; set; }



        [Display(Name = "Dept NO")]
        public Nullable<int> dno { get; set; }

    }
}