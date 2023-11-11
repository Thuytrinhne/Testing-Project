using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataProviders
{
    public  class CategoryDataProvider : DataProvider
    {
        protected CategoryDataProvider()
        {
            // Khởi tạo logic
        }
        private static CategoryDataProvider instance = null;
        public static CategoryDataProvider  Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDataProvider();
                }
                return instance;
            }
            private set
            {
                CategoryDataProvider.instance = value;
            }
        }
        public virtual bool executeInsertQuery(string q, CategoryDTO categoryDTO)
        {

            SqlParameter[] parameters = new SqlParameter[]
                   {
                    new SqlParameter("@name", SqlDbType.NVarChar),
                   };
            parameters[0].Value = categoryDTO.Name;
            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual bool executeUpdateQuery(string q, CategoryDTO tableDTO)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                    new SqlParameter("@id", SqlDbType.Int),
                    new SqlParameter("@name", SqlDbType.NVarChar),

           };
            parameters[0].Value = tableDTO.Id;
            parameters[1].Value = tableDTO.Name;
            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual DataTable executeSearchStoreProcedure(string name)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@name", SqlDbType.NVarChar),

            };
            parameters[0].Value = name;
            return base.executeStoreProcedure ("TimKiemLoaiMonAn", parameters);
        }

        public virtual bool executeDeleteQuery(string q, int ma)
        {
            SqlParameter[] parameters = new SqlParameter[]
                   {
                    new SqlParameter("@ma", SqlDbType.NVarChar),
            };
            parameters[0].Value = ma;
            return base.executeQueryWithParameter(q, parameters);
        }
    }
}
