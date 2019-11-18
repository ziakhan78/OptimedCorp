using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event Date is required and date format is 'dd-mm-yyyy'.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime EventDate { get; set; }

        public string Location { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Thumbnail")]
        public string Thumbnail { get; set; }

        [NotMapped]
        public HttpPostedFileBase EventThumbnail { get; set; }


        //[DataType(DataType.Upload)]
        //[Display(Name = "Upload Images")]
        //public string Images { get; set; }

        //[NotMapped]
        //public HttpPostedFileBase EventImages { get; set; }

        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public DateTime DateAdded { get; set; }
    }
}