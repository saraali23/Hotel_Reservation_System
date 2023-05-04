using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class Services
    {
        
        
        public int Id { get; set; }
        public int Breakfast { get; set; }
        public int Lunch { get; set; }
        public int Dinner { get; set; }
     
        public Boolean Cleaning { get; set; }
        public Boolean Towel { get; set; }
        public Boolean   S_surprise { get; set; }
        public int Food_bill { get; set; }

       

    }
}
