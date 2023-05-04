using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Backend.Context;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Windows.Controls.Primitives;
using Dapper;

namespace Project
{
    /// <summary>
    /// Interaction logic for Kitchen.xaml
    /// </summary>
    public partial class Kitchen : Window
    {
        int roombill;
        FoodMenu foodMenu;
        Services services;
        Entities.Reservation res;
        public static HotelReservationContext context;
        IDbConnection dbConnection;
        List<Entities.Reservation> DB;
        public Kitchen()
        {
            InitializeComponent();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            services=new Services();
            res=new Entities.Reservation();
            context = new HotelReservationContext();
            dbConnection = context.Database.GetDbConnection();
            //DAPPER
            DB = dbConnection.Query<Entities.Reservation, Customer, Services, Payment, Entities.Reservation>("Select  * from Reservations r, Customers c,Services s,Payments p where c.Id=r.CustomerId and s.Id=r.ServicesId and p.Id=r.PaymentId",
                (reser, cust, serv, pay) =>
                {
                    reser.Customer = cust;
                    reser.Services = serv;
                    reser.Payment = pay;
                    return reser;
                }, splitOn: "Id,Id,Id,Id").ToList();
            LoadForDataGridView();
           LoadlistBoxFromDataBase();
        }

        private void LoadForDataGridView()
        {           
           var list = (from r in DB
                       where r.Check_in== true && r.Supply_status==false
                        select new
                        {
                            r.Id,
                            r.Customer.First_name,
                            r.Customer.Last_name,
                            r.Customer.Phone_number,
                            r.Room_type,
                            r.Room_floor,
                            r.Room_number,
                            r.Services.Breakfast,
                            r.Services.Lunch,
                            r.Services.Dinner,
                            r.Services.Cleaning,
                            r.Services.Towel,
                            r.Services.S_surprise,
                            r.Supply_status,
                            r.Services.Food_bill
                        }
                          ).ToList();

            grid_overview.ItemsSource = null;
            grid_overview.ItemsSource = list;
        }
        private void LoadlistBoxFromDataBase()
        {
            var list = (from r in context.Reservations.Include(r => r.Customer).Include(r => r.Payment).Include(r => r.Services)
                        where r.Check_in == true && r.Supply_status == false
                        select new { r.Id, name = r.Customer.First_name + " " + r.Customer.Last_name, r.Customer.Phone_number }
                          ).ToList();

            list_onTheLine.ItemsSource = null;
            list_onTheLine.ItemsSource = list;
            list_onTheLine.SelectedValuePath = "Id";
            
        }

        private void list_onTheLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (list_onTheLine.SelectedItem == null)
            {
                if (list_onTheLine.Items.Count > 0)
                {
                    list_onTheLine.SelectedIndex = 0;
                    UpdateFormData(res);
                    roombill = (int)res.Payment.Total_bill - res.Services.Food_bill;
                }
               
            }
            else
            {
                res = context.Reservations.Where(x => x.Id == (int)list_onTheLine.SelectedValue).Include(x => x.Customer)
                    .Include(x => x.Services).Include(x => x.Payment).First();

                UpdateFormData(res);
                roombill = (int)res.Payment.Total_bill - res.Services.Food_bill;
            }




        }
        private void UpdateFormData(Entities.Reservation selected)
        {
            txt_name.Text = selected.Customer.First_name + " " + selected.Customer.Last_name;
            txt_phone.Text=selected.Customer.Phone_number;
            txt_roomType.Text = selected.Room_type;
            txt_roomNo.Text = selected.Room_number;
            txt_floorNo.Text = selected.Room_floor;

            txt_breakfast.Text = selected.Services.Breakfast.ToString();
            txt_dinner.Text = selected.Services.Dinner.ToString();
            txt_lunch.Text = selected.Services.Lunch.ToString();

            chk_sweetsur.IsChecked = selected.Services.S_surprise;
            chk_towels.IsChecked = selected.Services.Towel;
            chk_clean.IsChecked = selected.Services.Cleaning;

            //services
            services = selected.Services;


        }

        private void btn_changeSelection_Click(object sender, RoutedEventArgs e)
        {
           
            foodMenu = new FoodMenu(services);
            foodMenu.Show();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if(chk_foodsupply.IsChecked == true)
            {
                res.Supply_status= true;
            }

            res.Payment.Total_bill +=(float) res.Services.Food_bill;
            context.Payments.Update(res.Payment);
            context.Services.Update(res.Services);
            context.Reservations.Update(res);
            context.SaveChanges();


            MessageBox.Show("Info of "+ res.Id+" Updated");
           
            LoadlistBoxFromDataBase();
        }

        private void chk_foodsupply_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadForDataGridView();
        }

        private void TabItem_MouseEnter(object sender, MouseEventArgs e)
        {
            LoadForDataGridView();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
