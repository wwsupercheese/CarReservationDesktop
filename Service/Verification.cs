using Reposytory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Verification
    {
        public bool Status;
        DataBase data;
        public Dictionary<string, string> info;
        public Verification(string login, string password)
        {
            data = new DataBase();
            info = new Dictionary<string, string>();
            Status = CheckVerification(login, password);
            
        }
        public bool CheckVerification(string login, string password)
        {
            string query = "SELECT * FROM \"Seller\" WHERE email = '" + login + "' AND password = '" + password + "'";
            var admins = data.GetTable(query);
            if (admins.Count == 0)
            {
                return false;
            }
            info = admins[0];
            return true;
        }


    }
}
