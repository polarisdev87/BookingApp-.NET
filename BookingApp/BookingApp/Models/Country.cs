using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Country
    {

        public int CountryId { get; set; }
        [Required,StringLength(6)]
        public string code { get; set; }

        [Required, Index(IsUnique =true), StringLength(50)]
        public string name { get; set; }
        public IList<Region> l_Region { get; set; }
        //

        public Country()
        {

        }

        ~Country()
        {

        }

       

    }//end Country
}
