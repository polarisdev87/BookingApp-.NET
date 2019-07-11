using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Room
    {

        public int RoomId { get; set; }

        [Required, Range(1,5)]
        public int bedCount { get; set; }

        [Required, StringLength(500)]
        public string description { get; set; }

        [Required, Range(20,100)]
        public int pricePerNight { get; set; }

        [Required, Range(1,100)]
        public int roomNumber { get; set; }

        public IList<RoomReservations> l_RoomReservations { get; set; }
      
        [Required,ForeignKey("Accommodation")]
        public int AccommodationId { get; set; }

        public Accommodation Accommodation { get; set; }

        public Room()
        {

        }

        ~Room()
        {

        }

       

    }//end Room
}
