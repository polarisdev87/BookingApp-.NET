using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Place
    {

        public int PlaceId { get; set; }

        [Required, StringLength(70), Index(IsUnique =true)]
        public string name { get; set; }

        [Required,ForeignKey("Region")]
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public List<Accommodation> l_Accommodation { get; set; }
        public Place()
        {

        }

        ~Place()
        {

        }

        

       

    }//end Place
}
