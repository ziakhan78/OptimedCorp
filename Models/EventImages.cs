using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class EventImages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Events")]
        [Display(Name = "Event Name")]
        public int EventId { get; set; }
        public virtual Events Events { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Upload Images")]
        public string Images { get; set; }

        [NotMapped]
        public IEnumerable<HttpPostedFileBase> EventsImages { get; set; }
        public DateTime DateAdded { get; set; }
    }
}