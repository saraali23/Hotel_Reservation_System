using Backend.Context;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dapper;
using System.Data;

namespace Project
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        Entities.Reservation res;
        Services services;
        Payment payment;
        Customer customer;
        int totalBillWithoutTax;
        FinalizePayment finalizePayment;
        FoodMenu food_menu;
        public static HotelReservationContext context;
        IDbConnection dbConnection;
        List<Entities.Reservation> DB;
        public Reservation()
        {

            InitializeComponent();
           


        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //FoodAndMenuButton
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            food_menu = new FoodMenu(services);      
            food_menu.Show();
            food_menu.Closing += (sender, eventArgs) =>
            {

            };




        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context = new HotelReservationContext();

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            dbConnection = context.Database.GetDbConnection();
           //DAPPER
            DB = dbConnection.Query<Entities.Reservation,Customer,Services,Payment, Entities.Reservation>("Select  * from Reservations r, Customers c,Services s,Payments p where c.Id=r.CustomerId and s.Id=r.ServicesId and p.Id=r.PaymentId",
                (reser, cust, serv, pay) =>
                {
                    reser.Customer = cust;
                    reser.Services = serv;
                    reser.Payment = pay;
                    return reser;
                },splitOn:"Id,Id,Id,Id").ToList();
          
            res = new Entities.Reservation();
            services = new Services();
            services.Food_bill = 0;
            payment = new Payment();
            customer = new Customer();
            totalBillWithoutTax = 0;

            btn_Update.IsEnabled = false;
            btn_Delete.IsEnabled = false;
            cmb_SavedReserv.IsEnabled = false;
            btn_Submit.IsEnabled = false;
            grid_AllRes.CancelEdit();
            Entities.Reservation.AllRooms();

            cmb_RoomNum.ItemsSource = GetAvailableRooms();
        }

        //FinalizePaymentButton
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            totalBillWithoutTax = services.Food_bill + GetRoomBill();
            finalizePayment = new FinalizePayment(totalBillWithoutTax, services.Food_bill, payment);
            finalizePayment.Show();
            finalizePayment.Closing += (sender, eventArgs) =>
            {
                if (finalizePayment.validPay == true) //valid payment
                {
                    btn_Submit.IsEnabled = true;
                }
            };

        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
          
            btn_Update.IsEnabled = true;
            btn_Delete.IsEnabled = true;
            cmb_SavedReserv.IsEnabled = true;
            btn_FinalizeBill.IsEnabled = true;

            btn_Submit.IsEnabled = false;

            //get all reservations (id/name/ph-number)
            //insert them into combobox
            cmb_SavedReserv.ItemsSource = GetAllReservationst();
            cmb_SavedReserv.SelectedValuePath = "Id";




           





        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //data entered is valid
            if (ValidateData())
            {
                //customer filled successfully
                if (FillCustomerObject())
                {

                    try
                    {


                        res.NumberOfGuests = int.Parse(cmb_GuestNo.SelectionBoxItem.ToString());
                        res.Room_type = cmb_RoomType.SelectionBoxItem.ToString();
                        res.Room_number = cmb_RoomNum.SelectionBoxItem.ToString();
                        res.Room_floor = cmb_Floor.SelectionBoxItem.ToString();
                        res.Arrival_time = (DateTime)date_Entry.SelectedDate;
                        res.Leaving_time = (DateTime)date_Depart.SelectedDate;
                        res.Check_in = chk_CheckIn.IsChecked ?? false;
                        res.Supply_status = chk_FoodStat.IsChecked ?? false;

                        res.Customer = customer;
                        res.Payment = payment;
                        res.Services = services;

                        GetFormData();

                        context.Payments.Add(payment);
                        context.Services.Add(services);
                        context.Customers.Add(customer);
                        context.SaveChanges();
                        context.Reservations.Add(res);
                        if (context.SaveChanges() > 0)
                        {
                            MessageBox.Show("Rseservation Made Succesfully");
                            Entities.Reservation.rooms.Remove(cmb_RoomNum.Text);
                            cmb_RoomNum.ItemsSource = null;
                            cmb_RoomType.ItemsSource = Entities.Reservation.rooms;

                            customer = new Customer();
                            services = new Services();
                            payment = new Payment();
                            res = new Entities.Reservation();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Invalid Info");

                    }


                }


            }

           

        }
        private void GetFormData()
        {
            res.NumberOfGuests = int.Parse(cmb_GuestNo.SelectionBoxItem.ToString());
            res.Room_type = cmb_RoomType.SelectionBoxItem.ToString();
            res.Room_number = cmb_RoomNum.SelectionBoxItem.ToString();
            res.Room_floor = cmb_Floor.SelectionBoxItem.ToString();
            res.Arrival_time = (DateTime)date_Entry.SelectedDate;
            res.Leaving_time = (DateTime)date_Depart.SelectedDate;
            res.Check_in = chk_CheckIn.IsChecked ?? false;
            res.Supply_status = chk_FoodStat.IsChecked ?? false;

            res.Customer = customer;
            res.Payment = payment;
            res.Services = services;

        }

        //double check
        private static List<Entities.Reservation> GetAllReservationst()
        {
          
            var list = (from r in context.Reservations.Include(r=>r.Customer).Include(r=>r.Payment).Include(r=>r.Services)
                        select r
                          ).ToList();
            return list;
        }

        private bool ValidateData()
        {
            bool Valid = false;

            if (txt_Fname.Text.Trim().Length > 50 || txt_Lname.Text.Trim().Length > 50 || txt_Fname.Text.Trim().Length == 0 || txt_Lname.Text.Trim().Length == 0)
            {
                MessageBox.Show("Length Of Name Mustn't exceed 50 Characters or be less than 1 character");
            }
            else if (date_Birth.ToString() == "")
            {
                MessageBox.Show("Enter a Valid Date");
            }
            else if (int.TryParse(txt_Phone.Text,out int test)==false){
                MessageBox.Show("Enter a Valid Phone Number");
            }
            else if (txt_Email.Text.Trim().Length < 5)
            {
                MessageBox.Show("Enter a Valid Email");
            }
            //validate address data
            else if (txt_Street.Text.Length==0 ||txt_Apt.Text.Length==0||txt_City.Text.Length==0|| txt_Zip.Text.Length == 0) 
            {
                MessageBox.Show("Enter Valid Address Info");
            }
            else if (date_Depart.DisplayDate.Date<date_Entry.DisplayDate.Date)
            {
                MessageBox.Show("Departure Date is before than Entry Date!");
            }
            else
            {
                Valid=true;
            }
            return Valid;

        }
        private bool FillCustomerObject()
        {
            try
            {
                customer.First_name = txt_Fname.Text.Trim();
                customer.Last_name = txt_Lname.Text.Trim();
                customer.Birth_day = date_Birth.ToString();
                customer.Phone_number = txt_Phone.Text.Trim();
                customer.Email_address = txt_Email.Text.Trim();
                customer.Street_address = txt_Street.Text.Trim();
                customer.Apt_suite = txt_Apt.Text.Trim();
                customer.City=txt_City.Text.Trim();
                customer.ZipCode=txt_Zip.Text.Trim();
                customer.Gender = cmb_Gend.SelectionBoxItem.ToString();
                customer.State = cmb_State.SelectionBoxItem.ToString();
                return true;
            }
            catch { return false; }


        }
        private int GetRoomBill()
        {
            int roomBill=0;
            if (cmb_RoomType.SelectedIndex==0)
            {
                roomBill = 149;
                cmb_Floor.SelectedIndex =0;
            }
            else if (cmb_RoomType.SelectedIndex == 1)
            {
                roomBill = 299;
                cmb_Floor.SelectedIndex = 1;
            }
            else if(cmb_RoomType.SelectedIndex == 2)
            {
                roomBill = 349;
                cmb_Floor.SelectedIndex = 2;
            }
            else if (cmb_RoomType.SelectedIndex == 3)
            {
                roomBill = 399;
                cmb_Floor.SelectedIndex = 3;
            }
            else if (cmb_RoomType.SelectedIndex == 4)
            {
                roomBill = 499;
                cmb_Floor.SelectedIndex = 4;
            }
            return roomBill;
        }

        private void btn_NewRes_Click(object sender, RoutedEventArgs e)
        {
            btn_Update.IsEnabled = false;
            btn_Delete.IsEnabled = false;
            cmb_SavedReserv.IsEnabled = false;
            btn_Submit.IsEnabled = false;
            btn_FinalizeBill.IsEnabled = true;

            customer = new Customer();
            services = new Services();
            payment=new Payment();
            res=new Entities.Reservation();

        }

       
        private void UpdateFormData(Entities.Reservation selected)
        {
            if(!Entities.Reservation.rooms.Contains(selected.Room_number))
                Entities.Reservation.rooms.Add(selected.Room_number);
            cmb_RoomNum.ItemsSource = null;
            cmb_RoomNum.ItemsSource = Entities.Reservation.rooms;

            txt_Fname.Text = selected.Customer.First_name;
            txt_Lname.Text=selected.Customer.Last_name;
            if(DateTime.TryParse(selected.Customer.Birth_day,out DateTime dateOnly))
            {
                date_Birth.SelectedDate = dateOnly;
            }
            if(selected.Customer.Gender[0]=='F')
                cmb_Gend.SelectedIndex = 0;
            else 
                cmb_Gend.SelectedIndex = 1;

            txt_Phone.Text = selected.Customer.Phone_number;
            txt_Email.Text = selected.Customer.Email_address;
            txt_Street.Text = selected.Customer.Street_address;
            txt_Apt.Text = selected.Customer.Apt_suite;
            txt_City.Text = selected.Customer.City;
            txt_Zip.Text = selected.Customer.ZipCode;

            cmb_State.SelectedValue = selected.Customer.State;
            cmb_GuestNo.SelectedValue = selected.NumberOfGuests.ToString();
            cmb_Floor.SelectedValue = selected.Room_floor.ToString(); ;
            cmb_RoomNum.SelectedItem = selected.Room_number.ToString();
            cmb_RoomType.SelectedValue = selected.Room_type.ToString();

            date_Entry.SelectedDate = selected.Arrival_time;
            date_Depart.SelectedDate = selected.Leaving_time;

            chk_CheckIn.IsChecked = selected.Check_in;

           

            //Payment
            payment = selected.Payment;
            //services
            services = selected.Services;



        }

        private void cmb_SavedReserv_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Entities.Reservation.rooms.Remove(res.Room_number);

            if (cmb_SavedReserv.SelectedItem == null) 
                res = context.Reservations.First();
            else
                res = context.Reservations.Where(x => x.Id == ((Entities.Reservation)cmb_SavedReserv.SelectedItem).Id).First();
            UpdateFormData(res);

        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            GetFormData();
            FillCustomerObject();

            context.Payments.Update(res.Payment);
            context.Services.Update(res.Services);
            context.Customers.Update(res.Customer);
            context.Reservations.Update(res);

            context.SaveChanges();
            

            MessageBox.Show("Reservation Updated");
            Entities.Reservation.rooms.Remove(res.Room_number);
            cmb_SavedReserv.ItemsSource = GetAllReservationst();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            Entities.Reservation.rooms.Add(res.Room_number);
            context.Reservations.Remove(res);
            context.Customers.Remove(res.Customer);
            context.Services.Remove(res.Services);
            context.Payments.Remove(res.Payment);
            context.SaveChanges();
            MessageBox.Show("Reservation Deleted");
            
            cmb_SavedReserv.ItemsSource = GetAllReservationst();

        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            //SEARCH QUERY

            var all = dbConnection.Query<Entities.Reservation>("exec Search '" + txt_Search.Text + "'");

            var allReservs = all.ToList();// context.Set<AllData>().FromSqlRaw("exec Search '"+ txt_Search.Text + "'").AsNoTracking().ToList();
            if (allReservs.Count == 0)
            {
                MessageBox.Show("No Data Found, Sorry!");
            }
            else
            {
                grid_search.ItemsSource = null;
                grid_search.ItemsSource= allReservs;
                grid_search.Visibility = Visibility.Visible;
                
            }
            

        }

        private void roomOccupiedListBox_Loaded(object sender, RoutedEventArgs e)
        {
            //var occupiedRes = (from r in context.Reservations.Include(r => r.Customer).Include(r => r.Payment).Include(r => r.Services)
            //            where r.Check_in== true
            //            select new { r.Room_number, r.Room_type, r.Customer.First_name, r.Customer.Phone_number }).
            //            ToList();
            roomOccupiedListBox.ItemsSource = DB.Where(x => x.Check_in == true).Select(x => (x.Room_number, x.Room_type, x.Customer.First_name, x.Customer.Phone_number));
        }

        private void roomReservedListBox_Loaded(object sender, RoutedEventArgs e)
        {
            
            //var reservedRes = (from r in context.Reservations.Include(r => r.Customer).Include(r => r.Payment).Include(r => r.Services)
            //                   where r.Check_in == false
            //                    select new { r.Room_number, r.Room_type, r.Customer.First_name, r.Customer.Phone_number }).
            //           ToList();
            roomReservedListBox.ItemsSource =DB.Where(x=>x.Check_in== false).Select(x=>(x.Room_number,x.Room_type,x.Customer.First_name,x.Customer.Phone_number));
        }

        private void grid_AllRes_Loaded(object sender, RoutedEventArgs e)
        {

            var getAll =( from r in DB
                       select new
                       {
                           r.Id,
                           r.NumberOfGuests,
                           r.Room_floor,
                           r.Room_number,
                           r.Room_type,
                           r.Arrival_time,
                           r.Leaving_time,
                           r.Check_in,
                           r.Supply_status,
                           r.Customer.First_name,
                           r.Customer.Last_name,
                           r.Customer.Birth_day,
                           r.Customer.Gender,
                           r.Customer.Phone_number,
                           r.Customer.Email_address,
                           r.Customer.Street_address,
                           r.Customer.Apt_suite,
                           r.Customer.City,
                           r.Customer.State,
                           r.Customer.ZipCode,
                           r.Payment.Payment_type,
                           r.Payment.Card_type,
                           r.Payment.Card_number,
                           r.Payment.Card_exp,
                           r.Payment.Card_cvc,
                           r.Payment.Total_bill,
                           r.Services.Breakfast,
                           r.Services.Lunch,
                           r.Services.Dinner,
                           r.Services.Cleaning,
                           r.Services.Towel,
                           r.Services.S_surprise,
                           r.Services.Food_bill
                       });



            var allReservs = getAll.ToList();//context.Set<AllData>().FromSqlRaw("exec GetAllData").AsNoTracking().ToList();
            if (allReservs.Count == 0)
            {
                MessageBox.Show("No Data Found, Sorry!");
            }
            else
            {
                grid_AllRes.ItemsSource = null;
                grid_AllRes.ItemsSource = allReservs;
                grid_AllRes.Visibility = Visibility.Visible;

            }
          




        }
        private List<string> GetAvailableRooms()
        {
            //list of reserved/occupied rooms
            var list = (from r in DB
                        select r.Room_number).
                       ToList();

            var free= Entities.Reservation.rooms.Except(list).ToList();

            return free;
        }

        private void cmb_RoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Floor != null)
             cmb_Floor.SelectedIndex=cmb_RoomType.SelectedIndex;
        }

        private void grid_AllRes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
