using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Backend.Context;
using Backend.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HotelReservationContext context;

        public static HotelReservationContext CreateCTS()
        {
            context = new HotelReservationContext();
          
            context.Database.EnsureCreated();
           
            


            return context;
        }
        public static HotelReservationContext getContext()
        {
            return context;
        }




    }
}
