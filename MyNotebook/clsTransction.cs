using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook
{
    internal class Transction
    {
        private int t_no;
        private int p_no;
        private DateTime t_time;
        private string t_desc;
        private double t_debit;
        private double t_credit;

        internal Transction(
            int t_no,
            int p_no,
            DateTime t_time,
            string t_desc,
            double t_debit,
            double t_credit
            )
        {
            this.t_no = t_no;
            this.p_no = p_no;
            this.t_time = t_time;
            this.t_desc = t_desc;
            this.t_debit = t_debit;
            this.t_credit = t_credit;
        }


        internal int GetTransction
        {
            get
            {
                return this.t_no;
            }
        }

        internal int GetPersonNumber
        {
            get
            {
                return this.p_no;
            }
        }

        internal DateTime GetTime
        {
            get
            {
                return this.t_time;
            }
        }

        internal string GetDescrption
        {
            get
            {
                return this.t_desc;
            }
        }

        internal double GetDebit
        {
            get
            {
                return this.t_debit;
            }
        }

        internal double GetCredit
        {
            get
            {
                return this.t_credit;
            }
        }

    }

    internal static class TransctionManager
    {
        internal static bool Add(Transction transction)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "insert into tblTransctions (p_no,t_desc,t_time,t_debit,t_credit,user_number) values("+ transction.GetPersonNumber + ",'"+ transction.GetDescrption +"','"+ transction.GetTime.ToString() +"',"+ transction.GetDebit +","+ transction.GetCredit +",1)";
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
        internal static List<Transction> Search(DateTime from ,DateTime to)
        {
            List<Transction> res = new List<Transction>();

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select t_no,p_no,t_desc,t_time,t_debit,t_credit from tblTransctions where DateValue(Format(t_time,\"yyyy/MM/dd\")) between DateValue(Format(\""+ from.ToString("yyyy/MM/dd") +"\",\"yyyy/MM/dd\")) and DateValue(Format(\""+to.ToString("yyyy/MM/dd")+"\",\"yyyy/MM/dd\"))";

                if (db.ExcuteQuery(sql))
                {
                    while (db.GetDataReader.Read())
                    {
                        res.Add(new Transction(Convert.ToInt32(db.GetDataReader["t_no"]), Convert.ToInt32(db.GetDataReader["p_no"]), Convert.ToDateTime(db.GetDataReader["t_time"]), Convert.ToString(db.GetDataReader["t_desc"]), Convert.ToDouble(db.GetDataReader["t_debit"]), Convert.ToDouble(db.GetDataReader["t_credit"])));
                    }
                }

                db.CloseConnecton();

            }
            catch
            {
                res.Clear();
            }


            return res;
        }
        internal static List<Transction> Search(DateTime from, DateTime to,Person person)
        {
            List<Transction> res = new List<Transction>();

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select t_no,p_no,t_desc,t_time,t_debit,t_credit from tblTransctions where p_no= "+person.GetNumber+" and DateValue(Format(t_time,\"yyyy/MM/dd\")) between DateValue(Format(\"" + from.ToString("yyyy/MM/dd") + "\",\"yyyy/MM/dd\")) and DateValue(Format(\"" + to.ToString("yyyy/MM/dd") + "\",\"yyyy/MM/dd\"))";

                if (db.ExcuteQuery(sql))
                {
                    while (db.GetDataReader.Read())
                    {
                          res.Add(new Transction(Convert.ToInt32(db.GetDataReader["t_no"]), Convert.ToInt32(db.GetDataReader["p_no"]), Convert.ToDateTime(db.GetDataReader["t_time"]), Convert.ToString(db.GetDataReader["t_desc"]), Convert.ToDouble(db.GetDataReader["t_debit"]), Convert.ToDouble(db.GetDataReader["t_credit"])));
                    }
                }

                db.CloseConnecton();

            }
            catch
            {
                res.Clear();
            }

            return res;
        }
    }
}
