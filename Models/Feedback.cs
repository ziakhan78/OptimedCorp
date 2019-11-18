using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OptimedCorporation.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public string Type { get; set; }

        public string Ipaddress { get; set; }
        public DateTime DateAdded { get; set; }
    }
}