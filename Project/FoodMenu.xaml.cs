using Microsoft.Identity.Client;
using Project.Entities;
using System;
using System.Collections.Generic;
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

namespace Project
{
    /// <summary>
    /// Interaction logic for FoodMenu.xaml
    /// </summary>
    public partial class FoodMenu : Window
    {
      
        Services services;
        public FoodMenu(Services ser)
        {
            InitializeComponent();
        
            this.services = ser;
            
           
            txt_breakfastQuan.IsEnabled= false;
            txt_dinnerQuan.IsEnabled= false;
            txt_lunchQuan.IsEnabled= false;
            
        }

        private void FoodCheck(object sender, RoutedEventArgs e)
        {
            var obj=(CheckBox)sender;

            if (obj?.IsChecked != null)
            {
                if (sender.Equals( chk_breakfast))
                {
                    txt_breakfastQuan.IsEnabled = (bool)obj.IsChecked;
                }
                else if (sender.Equals(chk_lunch))
                {
                    txt_lunchQuan.IsEnabled = (bool)obj.IsChecked;
                }
                else
                {
                    txt_dinnerQuan.IsEnabled = (bool)obj.IsChecked;
                }
            }
            
            
               

        }

        private void btn_MenuNext_Click(object sender, RoutedEventArgs e)
        {
            int bill = 0;
            bool valid = true; 
            
            if (chk_breakfast.IsChecked==true)
            {
                if (int.TryParse(txt_breakfastQuan.Text, out int breakfast))
                {
                    bill += breakfast * 7;
                    services.Breakfast = breakfast;
                }
                else valid = false;


            }
            if (chk_lunch.IsChecked == true)
            {
                if (int.TryParse(txt_lunchQuan.Text, out int lunch))
                {
                    bill += lunch * 15;
                    services.Lunch = lunch;
                }
                else valid = false;
            }
            if (chk_dinner.IsChecked == true)
            {
                if (int.TryParse(txt_dinnerQuan.Text, out int dinner))
                {
                    bill += dinner * 15;
                    services.Dinner = dinner;
                }
                else valid = false;
            }

            if(!valid)
            {
                MessageBox.Show("Please Enter Valid Quantites");
                //reset
                services.Breakfast = 0; 
                services.Dinner = 0;
                services.Lunch= 0;
            }
            else
            {
                services.S_surprise=(bool)chk_surprise.IsChecked;
                services.Towel = (bool)chk_towel.IsChecked;
                services.Cleaning = (bool)chk_clean.IsChecked;

                services.Food_bill= bill;
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (services.Breakfast > 0)
            {
                chk_breakfast.IsChecked = true;
                txt_breakfastQuan.Text=services.Breakfast.ToString();
            }
            if (services.Lunch > 0)
            {
                chk_lunch.IsChecked = true;
                txt_lunchQuan.Text = services.Lunch.ToString();
            }
            if (services.Dinner > 0)
            {
                chk_dinner.IsChecked = true;
                txt_dinnerQuan.Text = services.Dinner.ToString();
            }
            if (services.Towel == true)
            {
                chk_towel.IsChecked = true;               
            }
            if (services.Cleaning == true)
            {
                chk_clean.IsChecked = true;
            }
            if (services.S_surprise == true)
            {
                chk_surprise.IsChecked = true;
            }


        }
    }
}
