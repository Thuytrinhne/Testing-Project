using DAL.DataProviders;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace DAL 
{
    public class DataProvider 
    {
        protected DataProvider()
        {
            // Khởi tạo logic
        }
        private static DataProvider instance = null;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            private set
            { DataProvider.instance = value;
            }
        }
        public virtual DataTable excecuteQuerry(string q)
        {
            try
            {
                Sql_Connection.Instance.connect();
                Sql_Connection.Instance.openCon();

                SqlCommand sqlCommand = new SqlCommand(q, Sql_Connection.Instance.sqlCon);
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(data);

                return data;
            }
            catch
            {
                throw new Exception();
            }

        }
        public virtual DataTable excecuteQuerry(string q, SqlParameter[] parameters)
        {
            try
            {
                Sql_Connection.Instance.connect();
                Sql_Connection.Instance.openCon();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = q;
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);

                }
                cmd.Connection = Sql_Connection.Instance.sqlCon;

                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);

                return data;
            }
            catch
            {
                throw new Exception();
            }

        }
        public  bool executeQueryWithParameter(string q, SqlParameter[] parameters)
        {
           
                Sql_Connection.Instance.connect();
                Sql_Connection.Instance.openCon();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = q;

                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);

                }


                cmd.Connection = Sql_Connection.Instance.sqlCon;


                if (cmd.ExecuteNonQuery() < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
         }
         public DataTable executeStoreProcedure(string storeP, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeP;

        
            foreach (var param in parameters)
            {
                cmd.Parameters.Add(param);

            }
            cmd.Connection = Sql_Connection.Instance.sqlCon;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            return dataTable;
        }
        public bool executeStoreProcedureNoReturnTable(string storeP, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storeP;


            foreach (var param in parameters)
            {
                cmd.Parameters.Add(param);

            }
            cmd.Connection = Sql_Connection.Instance.sqlCon;
            if (cmd.ExecuteNonQuery() < 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }


    }
    
}

