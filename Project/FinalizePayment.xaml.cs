using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    /// Interaction logic for FinalizePayment.xaml
    /// </summary>
    public partial class FinalizePayment : Window
    {

        Payment payment;
        int TotalBill;
        int FoodBill;
        public  bool validPay;
        public FinalizePayment(int totalBill, int foodBill,Payment pay)
        {
            InitializeComponent();

            TotalBill = totalBill;
            FoodBill = foodBill;
            payment = pay;
            validPay = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                payment.Payment_type = cmb_PayType.Text;

                //card number must be 16 number
                long getphn = Convert.ToInt64(txt_cardNo.Text.Replace("-",""));
                string formatString = String.Format("{0:0000-0000-0000-0000}", getphn);
                txt_cardNo.Text = formatString;
                payment.Card_number = formatString;


                payment.Card_exp = cmb_cardMonth.SelectionBoxItem.ToString() + "/" + cmb_cardYear.SelectionBoxItem.ToString();
                payment.Card_cvc= txt_cardCVC.Text;

                if (payment.Card_cvc.Length < 1)
                    throw new Exception();

                payment.Card_type = "Unknown";

                payment.Total_bill = (int)decimal.Parse(lbl_Total.Content.ToString());
                validPay = true;

                this.Close();
            }
            catch (Exception )
            {
                MessageBox.Show("Entered Info Is Invalid Please Try Again");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_CurrBill.Content = TotalBill.ToString();
            lbl_FoodBill.Content=FoodBill.ToString();
            lbl_Tax.Content = (TotalBill * 0.07).ToString();
            lbl_Total.Content = (TotalBill + (TotalBill * 0.07)).ToString();
            txt_cardNo.Text = payment.Card_number;
            txt_cardCVC.Text = payment.Card_cvc;

            if (payment.Card_exp != null)
            {
                cmb_cardMonth.SelectedItem = payment.Card_exp.Split("/")[0];
                cmb_cardYear.SelectedItem = payment.Card_exp.Split("/")[1];
            }
            else
            {
                cmb_cardMonth.SelectedIndex = 0;
                cmb_cardYear.SelectedIndex = 0;
            }


           

        }
    }
}
