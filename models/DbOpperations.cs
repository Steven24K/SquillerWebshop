using System.Linq;
using System.Collections.Generic;

using SquillerWebshop.DesignPatterns;

namespace SquillerWebshop.Models.Opperations
{
    public class DbOpperations
    {
        public static void AddCustomer(string name, string surname, Gender gender, string email, string password, string adress, string postalcode, string city)
        {
            using(var db = new WebshopContext())
            {

                Customer c = new Customer{
                    Name = name,
                    Surname = surname,
                    Gender = gender,
                    Email = email,
                    Password = password,
                    Adress = adress,
                    PostalCode = postalcode,
                    City = city
                 };
             db.Customer.Add(c);
             db.SaveChanges();
            }
        }

        public static Customer[] SelectAllCustomers()
        {
            using(var db = new WebshopContext()){
            
               var q0 = from customer in db.Customer
                        select customer;
            
             return q0.ToArray();
            }
            
        }
    }
}