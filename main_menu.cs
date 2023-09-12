using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Xml;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Bank_system_development
{
    internal class main_menu
    {
        //private object existingData;
        Random random = new Random();
        public main_menu()
        {

        }
        public void menu()
        {
            string jsonFileFOrBank = "jsonFileFOrBank.json";
            string jsonFileForACC = "jsonFileForACC.json";

            Console.WriteLine();
            Console.WriteLine("Please choose one option:");
            Console.WriteLine();
            Console.WriteLine(" 1-New Registration");
            Console.WriteLine(" 2-Uesr Login");
            
            Console.WriteLine();

             int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1://New Registration
                    Console.WriteLine("New Registration");

                    Console.WriteLine("Enter your name: ");
                    string cusName = Console.ReadLine();
                    Console.WriteLine("Enter your Email: ");
                    string cusEmail = Console.ReadLine();
                    Console.WriteLine("Enter Password: ");
                    string cusPass = Console.ReadLine();
                    user_registration newUser = new user_registration(cusName, cusEmail, cusPass);
                    List<user_registration> newCus = new List<user_registration>();

                        if (!File.Exists(jsonFileFOrBank))
                      {
                          using (StreamWriter fileItems = new StreamWriter(jsonFileFOrBank))
                             {
                            newCus.Add(newUser);
                            Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                            jsonSerializer.Serialize(fileItems, newCus);
                            fileItems.Flush();
                            fileItems.Close();
                             }
                     }

                    else
                    {
                        string existingJson = System.IO.File.ReadAllText(jsonFileFOrBank);
                        List<user_registration> details = JsonConvert.DeserializeObject<List<user_registration>>(existingJson);
                        int Y = 0; //to check last number of list in json and then will use this variable in if statement(if in last user  list in not found)

                        foreach (user_registration checkEmail in details)
                        {
                            Y++;
                            if (cusEmail == checkEmail.user_email)
                            {
                                Console.WriteLine("please register again because this Email (" + cusEmail + ") is already used");
                                menu();
                            }
                            else if (Y==details.Count)
                            {
                                details.Add(newUser);
                                System.IO.File.WriteAllText(jsonFileFOrBank, JsonConvert.SerializeObject(details, Newtonsoft.Json.Formatting.Indented));
                                break;
                            }
                        }
                    }


                    break;

                case 2://Uesr Login
                    Console.WriteLine();
                    Console.WriteLine("Uesr Login");
                    Console.WriteLine();
                    Console.WriteLine("Please Enter your Email:");
                    string email = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Please Enter your Password:");
                    string password = Console.ReadLine();

                    string check_json = System.IO.File.ReadAllText(jsonFileFOrBank);
                    List<user_registration> check_data = JsonConvert.DeserializeObject<List<user_registration>>(check_json);

                    int x = 0; //to check last number of list in json and then will use this variable in if statement(if in last user  list in not found)
                  
                    foreach(user_registration user_Registration in check_data)
                    {
                        x++;  //to check last number of list in json and then will use this variable in if statement(if in last user in list  not found)

                        if (email==user_Registration.user_email && password == user_Registration.user_password)
                        
                        {
                            Console.WriteLine("Login Successful");
                            Console.WriteLine();
                            Console.WriteLine("Please choose one option:");
                            Console.WriteLine();
                            Console.WriteLine(" 1-Create New Account");
                            Console.WriteLine(" 2-Transactions");
                            Console.WriteLine(" 3-Account Information");
                            Console.WriteLine(" 4-Transaction History");

                            Console.WriteLine();
                            int i = int.Parse(Console.ReadLine());
                            Console.WriteLine();

                            switch (i) {
                                case 1://Create New Account
                                    string customerName = user_Registration.user_name;
                                    string customerEmail = user_Registration.user_email;
                                    long customerACC = random.Next(1000000000);
                                    Console.WriteLine($"customer Name: {customerName}, Customer Email: {customerEmail}, customer new Account: {customerACC},\n \n please Enter  initial balance.");
                                    double balance = double.Parse(Console.ReadLine());

                                    createAccount createAcc = new createAccount(customerName, customerEmail, customerACC, balance);

                                    List<createAccount> newAcc = new List<createAccount>();


                                    if (!File.Exists(jsonFileForACC))
                                    {
                                        using (StreamWriter fileItems = new StreamWriter(jsonFileForACC))
                                        {
                                            newAcc.Add(createAcc);
                                            Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                                            jsonSerializer.Serialize(fileItems, newAcc);
                                            fileItems.Flush();
                                            fileItems.Close();
                                            break;
                                        }
                                       
                                    }
                                    else
                                    {
                                        string currentAccountsInJson = System.IO.File.ReadAllText(jsonFileForACC);
                                        List<createAccount> listAccounts = JsonConvert.DeserializeObject<List<createAccount>>(currentAccountsInJson);
                                        listAccounts.Add(createAcc);
                                        System.IO.File.WriteAllText(jsonFileForACC, JsonConvert.SerializeObject(listAccounts, Newtonsoft.Json.Formatting.Indented));

                                        break;
                                    }
                                    

                                case 2://Transactions

                                    string currentAccountsInJson1 = System.IO.File.ReadAllText(jsonFileForACC);
                                    List<createAccount> listAccounts1 = JsonConvert.DeserializeObject<List<createAccount>>(currentAccountsInJson1);

                                    Console.WriteLine("1-Deposite");
                                    Console.WriteLine("2-Withdrawal");
                                    Console.WriteLine("3-Transfer Money");
                                    Console.WriteLine();
                                    Console.WriteLine("Please choose transaction number: ");
                                    int a = int.Parse(Console.ReadLine());
                                    switch (a)
                                    {

                                        case 1: //Deposite
                                            Console.WriteLine("which account want to make transsaction on it:");
                                            int accNum = 0;

                                            foreach (createAccount checkAcc in listAccounts1)
                                            {
                                                if (user_Registration.user_email == checkAcc.email)
                                                {
                                                    accNum++;
                                                      Console.WriteLine(accNum+"-"+checkAcc.accountNumber);

                                                        
                                                }
                                                
                                            }
                                            Console.Write("please Write account number here: ");
                                            long acc = long.Parse(Console.ReadLine());

                                            foreach(createAccount account in listAccounts1)
                                            {
                                                if (account.accountNumber == acc)
                                                {
                                                    Console.WriteLine("how much want to deposite: ");
                                                    double depositeMoney = double.Parse(Console.ReadLine());
                                                    account.balance = account.balance + depositeMoney;
                                                    //listAccounts1.Add(account);
                                                    System.IO.File.WriteAllText(jsonFileForACC, JsonConvert.SerializeObject(listAccounts1, Newtonsoft.Json.Formatting.Indented));

                                                }
                                            }

                                            break;

                                        case 2://Withdrawal
                                           // string currentAccounts = System.IO.File.ReadAllText(jsonFileForACC);
                                            //List<createAccount> listAccounts2 = JsonConvert.DeserializeObject<List<createAccount>>(currentAccounts);
                                            
                                            int NumACC = 0;
                                            Console.WriteLine("your Accounts:");
                                            Console.WriteLine();
                                            foreach(createAccount withdrawalAcc in listAccounts1)
                                            {
                                                if(user_Registration.user_email == withdrawalAcc.email)
                                                {
                                                    NumACC++;
                                                    Console.WriteLine(NumACC + "-" + withdrawalAcc.accountNumber);

                                                }
                                            }
                                            Console.WriteLine();
                                            Console.Write("which account do want to take from: ");
                                            long AccWithrawal = long.Parse(Console.ReadLine());
                                            Console.WriteLine();
                                            Console.Write("How much want to take: ");
                                            double takenmoney = double.Parse(Console.ReadLine());
                                            foreach(createAccount account in listAccounts1)
                                            {
                                                if (account.accountNumber == AccWithrawal)
                                                {
                                                    if (account.balance >= takenmoney)
                                                    {
                                                        account.balance = account.balance - takenmoney;
                                                        System.IO.File.WriteAllText(jsonFileForACC, JsonConvert.SerializeObject(listAccounts1, Newtonsoft.Json.Formatting.Indented));
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Sorry you don't have enough balace");
                                                    }
                                                    
                                                }
                                            }

                                            break;

                                        case 3://Transfer Money
                                            int NumACCou = 0;
                                            Console.WriteLine("your Accounts:");
                                            Console.WriteLine();
                                            foreach (createAccount withdrawalAcc in listAccounts1)
                                            {
                                                if (user_Registration.user_email == withdrawalAcc.email)
                                                {
                                                    NumACCou++;
                                                    Console.WriteLine(NumACCou + "-" + withdrawalAcc.accountNumber);

                                                }
                                            }
                                            Console.WriteLine();
                                            Console.Write("which account do want to take from: ");
                                            long AccTransferFrom = long.Parse(Console.ReadLine());
                                            Console.WriteLine();

                                            Console.Write("Enter Beneficiary account Number: ");
                                            long Beneficiary = long.Parse(Console.ReadLine());
                                            Console.WriteLine();

                                            
                                            foreach(createAccount BeneficiaryACC in listAccounts1)
                                            {
                                                if (BeneficiaryACC.accountNumber == Beneficiary)
                                                {
                                                    Console.Write("Enter amount to transfer: ");
                                                    double tranferAmount = double.Parse(Console.ReadLine());
                                                    Console.WriteLine();
                                                    //BeneficiaryACC.balance = BeneficiaryACC.balance + tranferAmount;
                                                    foreach (createAccount account in listAccounts1)
                                                    {
                                                        if(account.accountNumber== AccTransferFrom)
                                                        {
                                                            if(account.balance>= tranferAmount)
                                                            {
                                                                account.balance = account.balance - tranferAmount;
                                                                BeneficiaryACC.balance = BeneficiaryACC.balance + tranferAmount;
                                                                System.IO.File.WriteAllText(jsonFileForACC, JsonConvert.SerializeObject(listAccounts1, Newtonsoft.Json.Formatting.Indented));
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Your amount not enough to tranfer.");
                                                                break;
                                                            }
                                                            
                                                        }
                                                    }
                                                    break;
                                                }
                                               
                                            }

                                            break;
                                    }

                                    break;

                                case 3://Account Information
                                    Console.WriteLine("4-Account Information");

                                    break;
                                case 4://Transaction History
                                    Console.WriteLine("5-Transaction History");
                                    break;
                            }
                            break;
                        }
                         else if ((x == check_data.Count) && (email != user_Registration.user_email || password != user_Registration.user_password))
                            {
                            Console.WriteLine("Login not Successful");
                            menu();

                           }

                    }
                    break;
                

                default:
                    Console.WriteLine("you have choosed wrong choice");
                    menu();
                    break;

                        
            }



        }
    }
}
