using System;
using System.Collections.Generic;
using System.Text;

namespace MidTerm_AjayDesai.Model
{
    public class ModelCustomer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<ModelCustomer> lstCust = new List<ModelCustomer>();

        public ModelCustomer()
        {
        }

        public ModelCustomer(int iD, string name, string address, string email, string phone)
        {
            ID = iD;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }
    }
}
