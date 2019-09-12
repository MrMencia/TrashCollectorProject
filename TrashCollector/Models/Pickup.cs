using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Pickup
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Regular Pickup Day")]
        public DayOfWeek RegularPickupDay { get; set; }


    }
}