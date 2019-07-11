using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Timers;
using System.Web;

namespace BookingApp.Models
{
    public class RoomReservations
    {
        //
        public int RoomReservationsId { get; set; }

        [Required, DataType(DataType.DateTime)]
        public string endDate { get; set; }

        [Required, DataType(DataType.DateTime)]
        public string startDate { get; set; }

      //  [Timestamp]
        public DateTime timestamp { get; set; }
        public IList<AppUser> l_User { get; set; }
        public IList<Room> l_Room { get; set; }
        // 1
        [Required,ForeignKey("Room")]
        public int RoomId { get; set; }

        public Room Room { get; set; }
        // 2

        [Required,ForeignKey("AppUser")]
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public RoomReservations()
        {

        }

        ~RoomReservations()
        {

        }


    

    }//end RoomReservations
}
