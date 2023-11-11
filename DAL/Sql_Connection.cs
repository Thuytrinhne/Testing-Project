using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class Sql_Connection
    {
        // Design patern Singleton // ctrl R +E
        #region Design patern Singleton  ctrl R +E
        private Sql_Connection() { }
        private static Sql_Connection instance = null;
        public static Sql_Connection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Sql_Connection();
                }
                return instance;
            }
            private set
            { Sql_Connection.instance = value; }
        }

        #endregion 

       public string strCon = Properties.Settings.Default.strCon;
       public SqlConnection sqlCon = null;
        public void connect ()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection (strCon);
            }
        }
        public void openCon()
        { 
            connect();
            if (sqlCon.State != ConnectionState.Open)
            {
                sqlCon.Open ();
            }

        }
        public void closeCon()
        {
            if(sqlCon != null &&sqlCon.State == ConnectionState.Open) {
            
            sqlCon.Close ();
            }
        }
    

    }
}
