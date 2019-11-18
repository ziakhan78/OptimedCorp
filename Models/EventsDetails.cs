using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class EventsDetails
    {
      
        public int EventId { get; set; }
     
        public string Title { get; set; }    
       
        public DateTime EventDate { get; set; }

        public string Location { get; set; }
    
        public string Description { get; set; }
               
        public string Thumbnail { get; set; }

        public string EventsImages { get; set; }

    }
}