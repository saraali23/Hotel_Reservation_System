using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        
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



       
    }
}
