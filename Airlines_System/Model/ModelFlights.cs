using System;
using System.Collections.Generic;
using System.Text;

namespace MidTerm_AjayDesai.Model
{
    public class ModelFlights
    {
        public int ID { get; set; }
        public int airlineID { get; set; }
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureDate { get; set; }
        public double FlightTime { get; set; }

        public List<ModelFlights> lstFlights = new List<ModelFlights>();
        public ModelFlights()
        {
        }

        public ModelFlights(int iD, int airlineID, string departureCity, string destinationCity, string departureDate, double flightTime)
        {
            ID = iD;
            this.airlineID = airlineID;
            DepartureCity = departureCity;
            DestinationCity = destinationCity;
            DepartureDate = departureDate;
            FlightTime = flightTime;
        }
    }
}
