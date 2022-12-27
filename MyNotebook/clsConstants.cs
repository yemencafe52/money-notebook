using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MyNotebook
{
    internal static class Constants
    {
        //private static string dbPath = Application.StartupPath + "\\DataBase\\" + "db.accdb";
        private static string dbPath = Environment.GetEnvironmentVariable("windir") + "\\mnbdb52.db"; //Application.StartupPath + "\\DataBase\\" + "db.accdb";
        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ dbPath +";Persist Security Info=False;";

        internal static string GetDbPath
        {
            get
            {
                return dbPath;
            }
        }

        internal static string GetConnectionString
        {
            get
            {
                return connectionString;
            }
        }
    }
}
