using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace IT_Comp.Models
{
    [MetadataType(typeof(MetaDepartments))]

    public partial class department
    {

    }
    public class MetaDepartments
    {
        [Display(Name = "Dept NO")]
        [DisplayFormat(DataFormatString = "{0:D3}", ApplyFormatInEditMode = false)]
        public int dno { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "please enter Department name")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 10 characters")]
        public string dname { get; set; }


        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        public string durl { get; set; }


        [Display(Name = "Logo")]
        public string dlogo { get; set; }
    }
   
}