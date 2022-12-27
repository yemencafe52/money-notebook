using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook
{
    class Person
    {
        private int number;
        private string name;
        private double tdebit;
        private double tcredit;

        internal Person(int number)
        {
            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "SELECT tblPeople.p_no, tblPeople.p_name, ((IIf(Sum(tblTransctions.t_debit)-Sum(tblTransctions.t_credit)>0,Sum(tblTransctions.t_debit)-Sum(tblTransctions.t_credit),0))) AS tdebit, ((IIf(Sum(tblTransctions.t_credit)-Sum(tblTransctions.t_debit)>0,Sum(tblTransctions.t_credit)-Sum(tblTransctions.t_debit),0))) AS tcredit FROM tblPeople LEFT JOIN tblTransctions ON tblPeople.p_no = tblTransctions.p_no where tblPeople.p_no=" + number +" GROUP BY tblPeople.p_no, tblPeople.p_name;";

                if (db.ExcuteQuery(sql))
                {
                    if (db.GetDataReader.Read())
                    {
                        this.number = Convert.ToInt32(db.GetDataReader["p_no"].ToString());
                        this.name = db.GetDataReader["p_name"].ToString();
                        this.tdebit = Convert.ToDouble(db.GetDataReader["tdebit"]);
                        this.tcredit = Convert.ToDouble(db.GetDataReader["tcredit"]);
                    }
                }
                else
                {
                    this.number = -1;
                }

                db.CloseConnecton();

            }
            catch
            {
                this.number = -1;
            }

        }

        internal Person(int number,string name,double debit,double credit)
        {
            this.number = number;
            this.name = name;
            this.tdebit = debit;
            this.tcredit = credit;
        }

        internal int GetNumber
        {
            get
            {
                return this.number;
            }
        }

        internal string GetName
        {
            get
            {
                return this.name;
            }
        }

        internal double Debit
        {
            get
            {
                return this.tdebit;
            }
        }

        internal double Credit
        {
            get
            {
                return this.tcredit;
            }
        }


    }

    class PeopleManager
    {

        private static int pno = 0;
        internal static bool Add(Person person)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "insert into tblPeople (p_no,p_name) values("+ person.GetNumber  +",'"+  person.GetName +"')";
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

        internal static List<Person> Search(string txt)
        {
            List<Person> res = new List<Person>();

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "SELECT tblPeople.p_no, tblPeople.p_name, ((IIf(Sum(tblTransctions.t_debit)-Sum(tblTransctions.t_credit)>0,Sum(tblTransctions.t_debit)-Sum(tblTransctions.t_credit),0))) AS tdebit, ((IIf(Sum(tblTransctions.t_credit)-Sum(tblTransctions.t_debit)>0,Sum(tblTransctions.t_credit)-Sum(tblTransctions.t_debit),0))) AS tcredit FROM tblPeople LEFT JOIN tblTransctions ON tblPeople.p_no = tblTransctions.p_no where tblPeople.p_name like('%"+txt+"%') GROUP BY tblPeople.p_no, tblPeople.p_name;";

                if (db.ExcuteQuery(sql))
                {
                    while (db.GetDataReader.Read())
                    {
                        res.Add(new Person(Convert.ToInt32(db.GetDataReader["p_no"].ToString()), (db.GetDataReader["p_name"].ToString()), Convert.ToDouble(db.GetDataReader["tdebit"]), Convert.ToDouble(db.GetDataReader["tcredit"])));
                    }
                }

                db.CloseConnecton();

            }
            catch
            {

            }

            return res;
        }

        internal static List<Person> GetALL()
        {
            List<Person> res = new List<Person>();

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "SELECT tblPeople.p_no, tblPeople.p_name, ((IIf(Sum(tblTransctions.t_debit)-Sum(tblTransctions.t_credit)>0,Sum(tblTransctions.t_debit)-Sum(tblTransctions.t_credit),0))) AS tdebit, ((IIf(Sum(tblTransctions.t_credit)-Sum(tblTransctions.t_debit)>0,Sum(tblTransctions.t_credit)-Sum(tblTransctions.t_debit),0))) AS tcredit FROM tblPeople LEFT JOIN tblTransctions ON tblPeople.p_no = tblTransctions.p_no GROUP BY tblPeople.p_no, tblPeople.p_name;";

                if(db.ExcuteQuery(sql))
                {
                    while(db.GetDataReader.Read())
                    {
                        res.Add(new Person(Convert.ToInt32(db.GetDataReader["p_no"].ToString()), (db.GetDataReader["p_name"].ToString()),Convert.ToDouble(db.GetDataReader["tdebit"]), Convert.ToDouble(db.GetDataReader["tcredit"])));
                    }
                }

                db.CloseConnecton();

            }
            catch
            {

            }

            return res;
        }

        internal static int GenerateNewPersonNumber()
        {
            //if (pno > 0)
            //{
            //    return ++pno;
            //}

            AccessDB db = new AccessDB(Constants.GetConnectionString);
            string sql = "select max(p_no) as res from tblPeople";

            if (db.ExcuteQuery(sql))
            {
                if (db.GetDataReader.Read())
                {
                    string r = db.GetDataReader["res"].ToString();
                    if (string.IsNullOrEmpty(r))
                    {
                        pno = 1;
                    }
                    else
                    {
                        pno = int.Parse(r);
                        pno++;
                    }
                }

            }

            db.CloseConnecton();
            return pno;

        }
    }

}
