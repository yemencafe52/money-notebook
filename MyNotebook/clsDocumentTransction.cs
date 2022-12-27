using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook
{
    internal class DocumentTransction
    {
        private int t_no;
        private int p_no;
        private uint doc_no;
        private string t_desc;
        private double t_debit;
        private double t_credit;

        internal DocumentTransction(
            int t_no,
            int p_no,
            uint doc_no,
            string t_desc,
            double t_debit,
            double t_credit
            )
        {
            this.t_no = t_no;
            this.p_no = p_no;
            this.doc_no = doc_no;
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

        internal uint DocumentNumber
        {
            get
            {
                return this.doc_no;
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

    internal static class DocumentTransctionManager
    {
        internal static bool Add(DocumentTransction transction)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "insert into tblTransctions (p_no,t_desc,doc_no,t_debit,t_credit,user_number) values(" + transction.GetPersonNumber + ",'" + transction.GetDescrption + "','" + transction.DocumentNumber + "'," + transction.GetDebit + "," + transction.GetCredit + ",1)";
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
       
        internal static List<DocumentTransction> GetDocumentTransctions(uint number)
        {
            List<DocumentTransction> res = new List<DocumentTransction>();

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select t_no,p_no,t_desc,doc_no,t_debit,t_credit from tblTransctions where doc_no= " + number;

                if (db.ExcuteQuery(sql))
                {
                    while (db.GetDataReader.Read())
                    {
                        res.Add(new DocumentTransction(Convert.ToInt32(db.GetDataReader["t_no"]), Convert.ToInt32(db.GetDataReader["p_no"]), Convert.ToUInt32(db.GetDataReader["doc_no"]), Convert.ToString(db.GetDataReader["t_desc"]), Convert.ToDouble(db.GetDataReader["t_debit"]), Convert.ToDouble(db.GetDataReader["t_credit"])));
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
