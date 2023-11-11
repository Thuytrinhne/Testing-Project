using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.PeerToPeer;
using System.IO.Ports;

namespace DAL.DataProviders
{
    public class LoginDataProvider : DataProvider
    {
        protected LoginDataProvider()
        {
            // Khởi tạo logic
        }
        private static LoginDataProvider instance = null;
        public static LoginDataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginDataProvider();
                }
                return instance;
            }
            private set
            {
                LoginDataProvider.instance = value;
            }
        }
        public virtual DataTable executeLoginQuery(string q, string username, string password)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@user", SqlDbType.VarChar),
             new SqlParameter("@pass", SqlDbType.VarChar)
            };
            parameters[0].Value = username;
            parameters[1].Value = password;

            return base.excecuteQuerry(q, parameters);

        }
        public virtual bool executeInsertQuery(string q,Account account)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", SqlDbType.VarChar),
                new SqlParameter("@display", SqlDbType.NVarChar),
                new SqlParameter("@pass", SqlDbType.VarChar),
                new SqlParameter("@type", SqlDbType.Int)

        };

            parameters[0].Value = account.UserName;
            parameters[1].Value = account.DisplayName;
            parameters[2].Value = account.Password;
            parameters[3].Value = account.TypeAccount;
            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual bool executeUpdateQuery(string q, Account account)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@id", SqlDbType.Int),
             new SqlParameter("@name", SqlDbType.VarChar),
             new SqlParameter("@dis", SqlDbType.NVarChar),
             new SqlParameter("@type", SqlDbType.Int)

            };

            parameters[0].Value = account.ID;
            parameters[1].Value = account.UserName;
            parameters[2].Value = account.DisplayName;
            parameters[3].Value = account.TypeAccount;
            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual bool executeUpdateQuery(string q,string username, string pass)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {

              new SqlParameter("@pass", SqlDbType.VarChar),
              new SqlParameter("@username", SqlDbType.VarChar)

             };

            parameters[0].Value = pass;
            parameters[1].Value = username;
           
            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual DataTable executeSearchStoreProcedure(string keyword)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@name", SqlDbType.NVarChar),

            };
            parameters[0].Value = keyword;
            return base.executeStoreProcedure("TimKiemMonAn", parameters);
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
