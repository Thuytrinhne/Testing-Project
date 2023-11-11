using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.IO.Ports;
using System.Net.PeerToPeer;

namespace DAL.DataProviders
{
    public class TableDataProvider :DataProvider
    {
        protected TableDataProvider()
        {
            // Khởi tạo logic
        }
        private static TableDataProvider instance = null;
        public static TableDataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableDataProvider();
                }
                return instance;
            }
            private set
            {
                TableDataProvider.instance = value;
            }
        }
        public virtual bool executeInsertQuery(string q, TableDTO tableDTO)
        {

            SqlParameter[] parameters = new SqlParameter[]
                   {
                    new SqlParameter("@name", SqlDbType.NVarChar),
                    new SqlParameter("@status", SqlDbType.NVarChar)
                   };
            parameters[0].Value = tableDTO.Name;
            parameters[1].Value = tableDTO.Status;
            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual bool executeDeleteQuery(string q, int ma) {
            SqlParameter[] parameters = new SqlParameter[]
                   {
                    new SqlParameter("@ma", SqlDbType.NVarChar),
            };
            parameters[0].Value = ma;
            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual bool executeUpdateQuery(string q, TableDTO tableDTO)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                    new SqlParameter("@id", SqlDbType.Int),
                    new SqlParameter("@name", SqlDbType.NVarChar),
                    new SqlParameter("@status", SqlDbType.NVarChar)

           };
            parameters[0].Value = tableDTO.Id;
            parameters[1].Value = tableDTO.Name;
            parameters[2].Value = tableDTO.Status;
            return base.executeQueryWithParameter(q, parameters);

        }
    }
}
