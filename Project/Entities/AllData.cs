using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Project.Entities
{
    public class AllData 
    {
        
        public int Id { get; set; }

        public int NumberOfGuests { get; set; }

        [MaxLength(10)]
        public string Room_type { get; set; }

        [MaxLength(10)]
        public string Room_floor { get; set; }

        [MaxLength(10)]
        public string Room_number { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Arrival_time { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Leaving_time { get; set; }

        public Boolean Check_in { get; set; }

        public Boolean Supply_status { get; set; }

      

        [MaxLength(50)]
        public string First_name { get; set; }

        [MaxLength(50)]
        public string Last_name { get; set; }

        [MaxLength(50)]
        public string Birth_day { get; set; }

        [MaxLength(50)]
        public string Gender { get; set; }

        [MaxLength(50)]
        public string Phone_number { get; set; }

        public string Email_address { get; set; }

        public string Street_address { get; set; }

        [MaxLength(50)]
        public string Apt_suite { get; set; }

        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [MaxLength(10)]
        public string Payment_type { get; set; }

        [MaxLength(10)]
        public string Card_type { get; set; }

        [MaxLength(50)]
        public string Card_number { get; set; }

        [MaxLength(50)]
        public string Card_exp { get; set; }

        [MaxLength(10)]
        public string Card_cvc { get; set; }

        public float Total_bill { get; set; }

        public int Breakfast { get; set; }
        public int Lunch { get; set; }
        public int Dinner { get; set; }

        public Boolean Cleaning { get; set; }
        public Boolean Towel { get; set; }
        public Boolean S_surprise { get; set; }
        public int Food_bill { get; set; }


    }
}
