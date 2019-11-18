using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Heading { get; set; }

        [Required(ErrorMessage = "News Date is required and date format is 'dd-mm-yyyy'.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "News Date")]
        public DateTime NewsDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

                       
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Thumbnail")]      
        public string Thumbnail { get; set; }

        [NotMapped]       
        public HttpPostedFileBase NewsThumbnail { get; set; }


      
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]       
        public string FilePath { get; set; }

        [NotMapped]       
        public HttpPostedFileBase NewsFile { get; set; }

      
        public bool Status { get; set; }
        public string Ipaddress { get; set; }
        public DateTime DateAdded { get; set; }
    }
}