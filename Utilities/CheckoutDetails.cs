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
    }
}