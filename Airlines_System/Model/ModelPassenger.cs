using System;
using System.Collections.Generic;
using System.Text;

namespace MidTerm_AjayDesai.Model
{
    public class ModelPassenger
    {
        public int ID { get; set; }
        public int customerID { get; set; }
        public int flightID { get; set; }

        public Stack<ModelPassenger> stkPassenger = new Stack<ModelPassenger>();

        public ModelPassenger()
        {
        }

        public ModelPassenger(int iD, int customerID, int flightID)
        {
            ID = iD;
            this.customerID = customerID;
            this.flightID = flightID;
        }
    }
}
