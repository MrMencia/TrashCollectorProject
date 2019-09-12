using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


//FOR MORE INFORMATION ON THE DATATYPE AND THE FORMAT ETC, REFER TO WEBSITE BELOW:
//https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application


//The DataType attribute is used to specify a data type that is more specific than the database intrinsic type. In this case we only want to keep track of the date, not the 
//date and time. The DataType Enumeration provides for many data types, such as Date, Time, PhoneNumber, Currency, EmailAddress and more. The DataType attribute can also enable 
//the application to automatically provide type-specific features. For example, a mailto: link can be created for DataType.EmailAddress, and a date selector can be provided for DataType.Date in browsers that support HTML5. 
//The DataType attributes emits HTML 5 data- (pronounced data dash) attributes that HTML 5 browsers can understand. The DataType attributes do not provide any validation.


//The ApplyFormatInEditMode setting specifies that the specified formatting should also be applied when the value is displayed in a text box for editing. (You might not want that for some fields — for example, 
//for currency values, you might not want the currency symbol in the text box for editing.)

//The DataType attribute can enable MVC to choose the right field template to render the data (the DisplayFormat uses the string template). For more information, see Brad Wilson's ASP.NET MVC 2 Templates.


namespace TrashCollector.Models
{
    public class Customer
    {
     

        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }



        [Display(Name = "State")]
        public string State { get; set; }


        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }


        [Display(Name = "Enter the Amount of funds you would like to add to your account:")]
        public double CustomerBalance { get; set; }


        [Display(Name = "What day of the week would you like us to collect your trash?")]
        public PickUpDay DaysOfWeek { get; set; }

        public enum PickUpDay
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
        }

        [DisplayName("Extra Pickup Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExtraPickupDay { get; set; }



        [DisplayName("Extra Pickup Confirmed")]
        public bool ExtraPickupConfirmed { get; set; }


        [Display(Name = "Want to Suspend your PickUp Temporarily? Click Here")]
        public bool SuspendedPickup { get; set; }
        [DisplayName("Temporary Suspension Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}", ApplyFormatInEditMode = true)]


        public DateTime? TemporarySuspensionStart { get; set; }
        [DisplayName("Temporary Suspension End")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}", ApplyFormatInEditMode = true)]

        public DateTime? TemporarySuspensionEnd { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}", ApplyFormatInEditMode = true)]


        [DisplayName("Regular Pickup Day")]
        public DayOfWeek RegularPickupDay { get; set; }




        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
       
        
        //public int RegularPickupDay { get; internal set; }


        //[DisplayName("Pickup")]
        //[ForeignKey("Pickup")]
        //public int PickupId { get; set; }
        //public Pickup Pickup { get; set; }
        //public IEnumerable<Pickup> Pickups { get; set; }
    }
}