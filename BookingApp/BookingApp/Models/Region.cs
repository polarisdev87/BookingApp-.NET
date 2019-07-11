using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Region
    {

        public int RegionId { get; set; }

        [Required, StringLength(100),Index(IsUnique =true)]
        public string name { get; set; }
        public IList<Place> l_Place { get; set; }
        
        [Required, ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
        public Region()
        {

        }

        ~Region()
        {

        }

    
       

    }//end Region

}
