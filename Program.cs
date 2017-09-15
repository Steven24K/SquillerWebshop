using System;
using SquillerWebshop.Models.Opperations;

namespace SquillerWebshop
{
    class Program
    {
        static void Main(string[] args)
        {
            //DbOpperations.AddCustomer("John","Schoenmaker",Gender.MAN,"jhon@asdasd.nl","password1","Straatweg 46","4443DE","Schiedam");
            foreach(var customer in DbOpperations.SelectAllCustomers()){
                System.Console.WriteLine(customer.Name);
            }
        }
    }
}
