using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select menu.")]
        public string Menu { get; set; }

       
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Banner")]
        public string BannerImage { get; set; }

        [NotMapped]
        [Required]
        public HttpPostedFileBase MenuBanner { get; set; } 

        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public DateTime DateAdded { get; set; }
    }
}