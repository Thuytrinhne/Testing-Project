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
    public class FoodDataProvider : DataProvider
    {
        protected FoodDataProvider()
        {
            // Khởi tạo logic
        }
        private static FoodDataProvider instance = null;
        public static FoodDataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodDataProvider();
                }
                return instance;
            }
            private set
            {
                FoodDataProvider.instance = value;
            }
        }
        public virtual bool executeInsertQuery(string q, FoodDTO foodDTO)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@name", SqlDbType.NVarChar),
             new SqlParameter("@price", SqlDbType.Float),
             new SqlParameter("@loai", SqlDbType.Int),
              };
            parameters[0].Value = foodDTO.TenMon;
            parameters[1].Value = foodDTO.Gia;
            parameters[2].Value = foodDTO.Loai;

            return base.executeQueryWithParameter(q, parameters);

        }
        public virtual bool executeUpdateQuery(string q, FoodDTO foodDTO)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@id", SqlDbType.Int),
                 new SqlParameter("@name", SqlDbType.NVarChar),
                 new SqlParameter("@price", SqlDbType.Float),
                 new SqlParameter("@idLoai", SqlDbType.Int),

            };

            parameters[0].Value = foodDTO.Id;
            parameters[1].Value = foodDTO.TenMon;
            parameters[2].Value = (float)foodDTO.Gia;
            parameters[3].Value = foodDTO.Loai;
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
