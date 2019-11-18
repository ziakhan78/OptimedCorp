using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Roles { get; set; }

        //[Display(Name ="Add Permission")]
        //public bool AddPer { get; set; }

        //[Display(Name = "Delete Permission")]
        //public bool DeletePer { get; set; }
        //[Display(Name = "View Permission")]
        //public bool ViewPer { get; set; }

        //[Display(Name = "Edit Permission")]
        //public bool EditPer { get; set; }
        public bool Status { get; set; }

        public DateTime DateAdded { get; set; }
        public string Ipaddress { get; set; }
    }
}