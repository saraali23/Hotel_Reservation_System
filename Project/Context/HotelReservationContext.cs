using Microsoft.EntityFrameworkCore;
using Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Backend.Context
{
    public class HotelReservationContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseSqlServer(@"");

        public virtual DbSet<LogIn> UserAccounts { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<AllData> AllResData { get; set; }




    }

}
