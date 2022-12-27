using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook
{
    class User
    {
        private byte user_number;
        private string user_username;
        private string user_password;

        internal User(byte number, string username, string password)
        {
            this.user_number = number;
            this.user_username = username;
            this.user_password = password;
        }

        internal byte GetNumber
        {
            get
            {
                return user_number;
            }
        }

        internal string GetUserName
        {
            get
            {
                return user_username;
            }
        }

        internal string GetPassword
        {
            get
            {
                return this.user_password;
            }
        }

    }
    internal static class UserManager
    {

        private static User activeUser;

        internal static bool ChangePassword(string password)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "update tblUsers set user_password='" + password + "' where user_number=1";

                if (db.ExcuteNonQuery(sql) == 1)
                {
                    res = true;
                }
            }
            catch
            {

            }

            return res;
        }

        internal static bool Login(User user)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select user_password from tblUsers where user_username='"+ user.GetUserName +"' and user_password='" + user.GetPassword + "'";

                if (db.ExcuteQuery(sql))
                {
                    if (db.GetDataReader.Read())
                    {
                        string pass = Convert.ToString(db.GetDataReader["user_password"]);
                        activeUser = new User(1, "admin", pass);
                        res = true;
                    }
                }

                db.CloseConnecton();
            }
            catch
            {

            }

            return res;
        }
        internal static User GetActiveUser
        {

            get
            {
                return activeUser;
            }

        }

    }

    
}
