using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookingApp.Models
{
    public class DBContext : IdentityDbContext<BAIdentityUser>
    {   
       

        public DBContext() : base("name=DdName")
        {            
        }

        public static DBContext Create()
        {
            return new DBContext();
        }
        public System.Data.Entity.DbSet<BookingApp.Models.Accommodation> Accommodation { get; set; }

        public System.Data.Entity.DbSet<BookingApp.Models.AccommodationType> AccommodationTypes { get; set; }

        public System.Data.Entity.DbSet<BookingApp.Models.Place> Places { get; set; }

        public System.Data.Entity.DbSet<BookingApp.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<BookingApp.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<BookingApp.Models.Region> Regions { get; set; }

        public System.Data.Entity.DbSet<BookingApp.Models.Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<BookingApp.Models.RoomReservations> RoomReservations { get; set; }
     

        public System.Data.Entity.DbSet<BookingApp.Models.AppUser> AppUsers { get; set; }
    }
}
