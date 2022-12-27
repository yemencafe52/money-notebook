using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook
{
    internal class Document
    {
        private uint doc_no;
        private DateTime date;
        private double amount;
        private string desc;
        private List<DocumentTransction> t = new List<DocumentTransction>();
        internal Document(uint number)
        {

        }

        internal Document(
               uint doc_no,
               DateTime date,
               double amount,
               string desc,
               List<DocumentTransction> t
         )
        {
            this.doc_no = doc_no;
            this.date = date;
            this.amount = amount;
            this.desc = desc;
            this.t.AddRange(t);
        }

        internal uint GetNumber
        {
            get
            {
                return this.doc_no;
            }
        }

        internal DateTime GeDate
        {
            get
            {
                return this.date;
            }
        }

        internal double GetAmount
        {
            get
            {
                return this.amount;
            }
        }

        internal string GetDescrption
        {
            get
            {
                return this.desc;
            }
        }

        internal List<DocumentTransction> GetTransctions
        {
            get
            {
                return this.t;
            }
        }
    }



    internal static class DocumentManager
    {
        internal static bool Add(Document document)
        {
            bool res = false;

            try
            {
                if(!(document.GetTransctions.Count > 1))
                {
                    throw new Exception("");
                }

                double tdebit = 0;
                double tcredit = 0;

                foreach(DocumentTransction dt in document.GetTransctions)
                {
                    tdebit += dt.GetDebit;
                    tcredit += dt.GetCredit;
                }

                if(tdebit != tcredit)
                {
                    throw new Exception();
                }

                if(tdebit != document.GetAmount)
                {
                    throw new Exception();
                }

                if (!(document.GetAmount > 0))
                {
                    throw new Exception();
                }

                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "insert into tblDocuments (doc_no,doc_date,doc_amount,doc_desc) values("+ document.GetNumber +",'"+ document.GeDate.ToString() +"',"+ document.GetAmount +",'"+ document.GetDescrption +"')";
                if(db.ExcuteNonQuery(sql)==1)
                {
                    for(int i=0;i<document.GetTransctions.Count;i++)
                    {
                        if(!DocumentTransctionManager.Add(document.GetTransctions[i]))
                        {
                            //Delete(document);
                            return res;
                        }
                    }

                    res = true;
                }
            }
            catch
            {

            }

            return res;
        }

        internal static bool Delete(Document document)
        {
            bool res = false;

            try
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "delete from tblDocuments where doc_no=" + document.GetNumber;

                if(db.ExcuteNonQuery(sql) == 1)
                {
                    res = true;
                }
            }
            catch
            {

            }

            return res;
        }

        internal static uint GenerateNewDocumentNumber()
        {
            uint res = 0;


            return res;
        }
    }
}
