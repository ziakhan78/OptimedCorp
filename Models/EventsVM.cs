using OptimedCorporation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class EventsVM
    {

        public List<Events> Events { get; set; }
        public List<EventImages> EventImages { get; set; }

    }

}
