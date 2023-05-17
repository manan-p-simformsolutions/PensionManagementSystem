using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagement.Shared.Models
{
    public class User
    {
        public int Userid { get; set; }
        public string Username { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Mobilenumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
