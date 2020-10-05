using Locusnine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace locusnine.Service.Service
{
    public class EmailValidation
    {
        private LocusnineUserDataEntities db = new LocusnineUserDataEntities();

        public int CheckEmail(string name)
        {
            //get the email address for the name
            string email = db.Users.Where(x => x.Name == name).OrderByDescending(x => x.Email).Select(x=>x.Email).FirstOrDefault();
            if (email != null)
            {
                string str = email.Split('@')[0];
                if (str.Length == name.Length)// if its the second record
                {
                    return 0;
                }
                str = str.Replace(name + "+", String.Empty);//if it has more than 2 records
                if (str.Length > 0)
                {
                    return Convert.ToInt32(str);
                }
            }
            return -1;
        }
        /// <summary>
        /// Get the proper email address
        /// </summary>
        /// <param name="name">Name of the User</param>
        /// <returns></returns>
        public string  AppendEmailSuffix(string name)
        {
            string suffixName = "@locusnine.com";
            int num=CheckEmail(name);
            if (num != -1)
            {
                return name +"+"+ (++num) + suffixName;
            }
            return name+suffixName;
        }
    }
}