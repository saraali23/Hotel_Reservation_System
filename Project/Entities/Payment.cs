using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities
{
    public class Payment
    {
        public int Id { get; set; }

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
    }
}
