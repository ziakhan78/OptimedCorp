using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class Careers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Date of Birth is required and date format is 'dd-mm-yyyy'.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime Dob { get; set; }
     
        public int Experience { get; set; }

        public string Qualification { get; set; }

        [DataType(DataType.MultilineText)]
        public string SkillSet { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Resume")]
        public string Resume { get; set; }

        [NotMapped]
        public HttpPostedFileBase ResumeFile { get; set; }

        public string Ipaddress { get; set; }
        public DateTime DateAdded { get; set; }
    }
}