using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Comment
    {
       
        public int CommentId { get; set; }

        [Required, Range(1,5)]
        public int grade { get; set; }

        [Required, StringLength(500)]
        public string text { get; set; }
        public List<AppUser> l_User { get; set; }

        public List<Accommodation> l_Accommodation;
        //
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("Accomodation")]
        public int AccommodationId { get; set; }

        public Accommodation Accomodation { get; set; }
        public Comment()
        {

        }

        ~Comment()
        {

        }

    
        

      

    }//end Comment
}
