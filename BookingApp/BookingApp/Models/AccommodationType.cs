using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class AccommodationType
    {
      
        public int AccommodationTypeId { get; set; }

        [Required, StringLength(50), Index(IsUnique =true)]
        public string name { get; set; }
        public List<Accommodation> l_Accommodation { get; set; }
        //

        public AccommodationType()
        {

        }

        ~AccommodationType()
        {

        }

       

       
    }//end AccommodationType
}
