using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class StoreLocator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }

        //[Required]       
        [StringLength(14, MinimumLength = 10, ErrorMessage = "Mobile No. must be in 10 digit")]
        public string Mobile { get; set; }

        [StringLength(14, MinimumLength = 10, ErrorMessage = "Mobile No. must be in 10 digit")]
        [Display(Name = "Alternate Mobile")]
        public string Mobile2 { get; set; }

        [MaxLength(14)]
        public string Phone1 { get; set; }

        [MaxLength(14)]
        public string Phone2 { get; set; }


        public string Email { get; set; }


        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string State { get; set; }

        
        [Display(Name = "GPS Coordinates")]
        public string Longitude { get; set; }      
        public string Latitude { get; set; }


        //public string WorkingDaysFrom { get; set; }

        //public string WorkingDaysTo { get; set; }


        [Display(Name = "Working Time")]
        public string WorkingTimeStart { get; set; }

        public string WorkingTimeEnd { get; set; }

        [Display(Name = "Closed On")]
        public string ClosedOn { get; set; }

        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public DateTime DateAdded { get; set; }

        //public int StateId { get; set; }
        //public virtual State State { get; set; }

        //public Days DaysName { get; set; }
    }

    // this enum are working
    //public enum Days
    //{
    //    Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Satuarday
    //}
}