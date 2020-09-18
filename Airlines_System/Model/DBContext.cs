using System;
using System.Collections.Generic;
using System.Text;

namespace MidTerm_AjayDesai.Model
{
    class DBContext
    {
        public ModelAirlines objAir = new ModelAirlines();
        public ModelCustomer objCust = new ModelCustomer();
        public ModelFlights objFlight = new ModelFlights();
        public ModelPassenger objPass = new ModelPassenger();
        public ModelLogins objLogin = new ModelLogins();
        public void FillLogin()
        {
            //Login Dictionary
            ModelLogins.dcLogins = new Dictionary<string, ModelLogins>
            {
                { "desaiaj", new ModelLogins(1, "desaiaj", "desaiaj", 1) }, //SuperUser
                { "amanjs", new ModelLogins(2, "amanjs", "aman123", 0) },
                { "supervisor", new ModelLogins(3, "supervisor", "Super@123", 0) },
                { "general", new ModelLogins(4, "general", "Genqwe", 0) },
                { "temp", new ModelLogins(5, "temp", "OtherS", 0) }
            };
        }
        public List<ModelCustomer> FillCust()
        {
            objCust.lstCust.Add(new ModelCustomer(1, "Cust1", "Sheridan way", "cust1.Email1@gmail.com", "123 345 6789"));
            objCust.lstCust.Add(new ModelCustomer(2, "Cust2", "Mainstreet way", "cust2.Email2@yahoo.com", "231 345 5843"));
            objCust.lstCust.Add(new ModelCustomer(3, "Cust3", "Robert way", "cust3.Email3@hotmail.com", "364 234 5464"));
            objCust.lstCust.Add(new ModelCustomer(4, "Cust4", "Milcreek way", "Cust4_email4@gmail.com", "456 435 5654"));
            objCust.lstCust.Add(new ModelCustomer(5, "Cust5", "Downtown 9th way", "cust5.rmail5@gmail.com", "543 357 7544"));
            return objCust.lstCust;
        }
        public List<ModelFlights> FillFlights()
        {
            objFlight.lstFlights.Add(new ModelFlights(1, 2, "Brampton", "London", "2 june", 1.05));
            objFlight.lstFlights.Add(new ModelFlights(2, 4, "Delhi", "Chicago", "12 Aug", 11.30));
            objFlight.lstFlights.Add(new ModelFlights(3, 5, "Toronto", "Mumbai", "15 Dec", 12.45));
            objFlight.lstFlights.Add(new ModelFlights(4, 1, "Montreal", "Reo", "30 Aug", 10.00));
            objFlight.lstFlights.Add(new ModelFlights(5, 3, "Sydney", "Toronto", "2 june", 18.16));
            objFlight.lstFlights.Add(new ModelFlights(6, 2, "Mississauga", "Calgary", "2 june", 14.16));
            return objFlight.lstFlights;
        }
        public Queue<ModelAirlines> FillAirlines()
        {
            objAir.quAirLines.Enqueue(new ModelAirlines(1, "Air Canada", "Airbus 320", 60, "Veg-salad"));
            objAir.quAirLines.Enqueue(new ModelAirlines(2, "Virgin", "Boeing 777", 35, "Chicken"));
            objAir.quAirLines.Enqueue(new ModelAirlines(3, "British Airways", "Bombardier Q", 78, "Sushi"));
            objAir.quAirLines.Enqueue(new ModelAirlines(4, "Air India", "Airbus 320", 19, "Veg-salad"));
            objAir.quAirLines.Enqueue(new ModelAirlines(5, "Delta", "Boeing 777", 120, "Sushi"));
            return objAir.quAirLines;
        }
        public Stack<ModelPassenger> FillPassenger()
        {
            objPass.stkPassenger.Push(new ModelPassenger(1, 2, 1));
            objPass.stkPassenger.Push(new ModelPassenger(2, 5, 4));
            objPass.stkPassenger.Push(new ModelPassenger(3, 1, 2));
            objPass.stkPassenger.Push(new ModelPassenger(4, 4, 5));
            objPass.stkPassenger.Push(new ModelPassenger(5, 3, 3));
            return objPass.stkPassenger;
        }
    }
}
