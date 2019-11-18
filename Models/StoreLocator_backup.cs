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

        [Required]
        public string Contact { get; set; }

      
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string State { get; set; }
       
        public string Longitude { get; set; }

      
        public string Latitude { get; set; }

        [Display(Name = "Working Days")]
        public string WorkingDays { get; set; }

        [Display(Name = "Working Time")]
        public string WorkingTime { get; set; }

        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public DateTime DateAdded { get; set; }

        //public int StateId { get; set; }
        //public virtual State State { get; set; }
    }
}