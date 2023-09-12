using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_system_development
{
    internal class user_registration
    {
        public string user_name { get; set; } 
        public string user_email { get; set; }
        public string user_password { get; set; }

        public user_registration(string user_name, string user_email, string user_password)
        {
            this.user_name = user_name;
            this.user_email = user_email;
            this.user_password = user_password;
        }

        public override string ToString()
        {
            return $"user name: {user_name} user email: {user_email} user password {user_password}";
        }
    }
}
