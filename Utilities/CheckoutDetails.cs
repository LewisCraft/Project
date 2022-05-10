using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Utilities.CheckoutDetails
{
    
    //class for holding the details needed for the checkout page
    public class CheckoutDetails
    {
        public string FName;
        public string LName;
        public string Address;
        public string City;
        public string Postcode;
        public string Phone;

        //default constructor
        public CheckoutDetails()
        {
            FName = "Lewis";
            LName = "Craft";
            Address = "123 Fake Street";
            City = "Springfield";
            Postcode = "SS0 9EF";
            Phone = "01702 666985";
        }

        //constructer that reads in the data from a text file taking a filepath as input
        public CheckoutDetails(string filepath)
        {
            string[] inputs = System.IO.File.ReadAllLines(filepath);
            inputs = inputs[0].Split(',');

            FName = inputs[0];
            LName = inputs[1];
            Address = inputs[2];
            City = inputs[3];
            Postcode = inputs[4];
            Phone = inputs[5];
        }
    }
}