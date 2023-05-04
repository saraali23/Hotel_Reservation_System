using Backend.Context;
using Backend.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dapper;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Project.Entities;


namespace Project
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HotelReservationContext HotelReservationContext;
        IDbConnection dbConnection;
        
        public MainWindow()
        {
            InitializeComponent();
            HotelReservationContext = App.CreateCTS();
            dbConnection = HotelReservationContext.Database.GetDbConnection();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name=txt_username.Text;
            string pass = txt_password.Password.ToString();

            List<LogIn> GetALL() => dbConnection.Query<LogIn>($"Select * from UserAccounts where user_name='{name}' and pass_word='{pass}'").ToList();

          //  var x = HotelReservationContext.UserAccounts.Select(p => p).Where(x => x.Username == name && x.Password == pass);

            if (GetALL().Count>0)
            {
                if (name == "kitchen")
                {
                    Kitchen kitchen= new Kitchen();
                    this.Close();
                    kitchen.Show();

                }
                else
                {
                    Reservation reservation = new Reservation();
                    this.Close();
                    reservation.Show();
                }
                
            }

        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
                   }
    }
}
