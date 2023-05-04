using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.Entities
{
    public class Reservation
    {
        //9
        [Key]
        public int Id { get; set; }

        public int NumberOfGuests { get; set; }

        [MaxLength(10)]
        public string Room_type { get; set; }

        [MaxLength(10)]
        public string Room_floor { get; set; }

        [MaxLength(10)]
        public string Room_number { get; set; }

        [Column(TypeName ="Date")]
        public DateTime Arrival_time { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Leaving_time { get; set; }

        public Boolean Check_in { get; set; }

        public Boolean Supply_status { get; set; }

        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public Services Services { get; set; }

        public override string ToString()
        {
            return $"Id:[{Id}] Name:[{Customer.First_name+" "+Customer.Last_name}]Phone Number:[{ Customer.Phone_number}]";
        }

        [NotMapped]
        public static List<string> rooms;

        public static List<string> AllRooms()
        {
           rooms = new() { "101" ,
                          "102" ,
                          "103" ,
                          "104" ,
                          "105" ,
                          "106" ,
                          "107" ,
                          "108" ,
                          "109" ,
                           "110" ,
                           "201",
                          "202" ,
                          "203" ,
                          "204" ,
                          "205" ,
                          "206" ,
                          "207" ,
                          "208" ,
                          "209" ,
                          "210" ,
                           "301",
                          "302",
                          "303",
                          "304",
                          "305",
                          "306",
                          "307",
                          "308",
                          "309",
                          "310",
                           "401",
                          "402" ,
                          "403" ,
                          "404" ,
                          "405" ,
                          "406" ,
                          "407" ,
                          "408" ,
                          "409" ,
                          "410" ,
                           "501" ,
                          "502" ,
                          "503" ,
                          "504" ,
                          "505" ,
                          "506" ,
                          "507" ,
                          "508" ,
                          "509" ,
                          "510" ,


            };
            return rooms;


        }





    }
}
