using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Pickup
    {
        [Key]
        public int pickupId { get; set; }
        [DisplayName("Regular Pickup Day")]


        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public Customer Customer { get; set; }

        public DayOfWeek RegularPickupDay { get; set; }


    }
}