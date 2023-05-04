using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class LogIn
    {
        [Key]
        [Column("user_name")]
        [StringLength(50)]
        public string Username { get; set; }

        [Column("pass_word")]
        [StringLength(50)]
        public string Password { get; set; }

        public LogIn()
        {

        }
        public LogIn(string user,string pass)
        {
            Username= user;
            Password= pass;
        }
    }
}
