using System;
using System.Collections.Generic;
using System.Text;

namespace MidTerm_AjayDesai.Model
{
    class ModelLogins
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int SuperUser { get; set; }

        public static Dictionary<string, ModelLogins> dcLogins;
        public ModelLogins()
        {
        }

        public ModelLogins(int iD, string username, string password, int superUser)
        {
            ID = iD;
            Username = username;
            Password = password;
            SuperUser = superUser;
        }
    }
}
