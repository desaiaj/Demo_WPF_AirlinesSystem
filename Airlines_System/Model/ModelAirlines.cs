using System;
using System.Collections.Generic;
using System.Text;

namespace MidTerm_AjayDesai.Model
{
    public class ModelAirlines
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Airplane { get; set; }
        public int SeatsAvailable { get; set; }
        public string MealAvailable { get; set; }

        public Queue<ModelAirlines> quAirLines = new Queue<ModelAirlines>();
        public ModelAirlines()
        {
        }

        public ModelAirlines(int iD, string name, string airplane, int seatsAvailable, string mealAvailable)
        {
            ID = iD;
            Name = name;
            Airplane = airplane;
            SeatsAvailable = seatsAvailable;
            MealAvailable = mealAvailable;
        }
    }
}
