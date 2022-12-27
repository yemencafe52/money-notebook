using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace MyNotebook
{
    internal class AccessDB
    {
        private OleDbConnection con = null;
        private OleDbCommand cmd = null;
        private OleDbDataReader dr = null;
        private string connection_string = "";
        internal AccessDB(string cn)
        {
            this.connection_string = cn;
        }

        internal int ExcuteNonQuery(string sql)
        {
            int res = 0;

            try
            {
                this.con = new OleDbConnection(this.connection_string);
                this.con.Open();
                this.cmd = new OleDbCommand(sql, this.con);
                res = this.cmd.ExecuteNonQuery();
                this.con.Close();
            }
            catch
            {

            }

            return res;
        }

        internal bool ExcuteQuery(string sql)
        {
            bool res = false;

            try
            {
                this.con = new OleDbConnection(this.connection_string);
                this.con.Open();
                this.cmd = new OleDbCommand(sql, this.con);
                this.dr = this.cmd.ExecuteReader();

                if (this.dr.HasRows)
                {
                    res = true;
                }

            }
            catch
            {

            }


            return res;
        }

        internal void CloseConnecton()
        {
            if (con != null)
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        con.Close();
                    }
                    catch
                    {

                    }
                    
                }

            }

            GC.Collect();
        }

        internal OleDbDataReader GetDataReader
        {
            get
            {
                return this.dr;
            }
        }

        ~AccessDB()
        {
            CloseConnecton();
        }

    }
}
