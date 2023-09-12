using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_system_development
{
    internal class createAccount
    {
        public string name { get; set; }
        public string email { get; set; }
        public long accountNumber { get; set; }
        public double balance { get; set; }

        public createAccount(string name, string email, long accountNumber, double balance)
        {
            this.name = name;
            this.email = email;
            this.accountNumber = accountNumber;
            this.balance = balance;
        }


        public override string ToString()
        {
            return $"holder name: {name} email: {email} account Number: {accountNumber} current balance: {balance}";
        }
    }

}
