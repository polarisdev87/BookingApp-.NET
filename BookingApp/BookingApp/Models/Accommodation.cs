using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Accommodation
    {
        [Required]
        public int AccommodationId { get; set; }

        [Required, StringLength(150)]
        public string address { get; set; }

        [Required]
        public bool approved { get; set; }

        [Required, Range(1, 5)]
        public float averageGrade { get; set; }

        [Required, StringLength(1000)]
        public string description { get; set; }

        [Required, DataType(DataType.ImageUrl)]
        public string imageURL { get; set; }
        [Required]
        public double latitude { get; set; }
        [Required]
        public double longitude { get; set; }
        [Required, StringLength(50)]
        public string name { get; set; }

        public List<Comment> l_Comment { get; set; }
        public List<Room> l_Room { get; set; }
        // 1
        [Required,System.ComponentModel.DataAnnotations.Schema.ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        // 2
        [Required, ForeignKey("AccommodationType")]
        public int AccommodationTypeId { get; set; }
        public AccommodationType AccommodationType { get; set; }
        
        [Required,ForeignKey("AppUser")]
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public Accommodation()
        {

        }

        ~Accommodation()
        {

        }

    }//end Accommodation
}
