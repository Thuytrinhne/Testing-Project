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
    public class BillDataProvider : DataProvider
    {
        protected BillDataProvider()
        {
            // Khởi tạo logic
        }
        private static BillDataProvider instance = null;
        public static BillDataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDataProvider();
                }
                return instance;
            }
            private set
            {
                BillDataProvider.instance = value;
            }
        }
        public virtual bool executeInsertQuery(int maBan)
        {

            SqlParameter[] parameters = new SqlParameter[]
                   {
                    new SqlParameter("@idTable", SqlDbType.Int),
                   };
            parameters[0].Value =maBan;
            return base.executeStoreProcedureNoReturnTable ("themBill", parameters);

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
        public virtual DataTable executeSearchStoreProcedure(int ma)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@ma", SqlDbType.NVarChar),

            };
            parameters[0].Value = ma;
            return base.executeStoreProcedure("getUncheckBillByTable", parameters);
        }
        public virtual bool executeCheckoutStoreProcedure(int ma)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@maBill", SqlDbType.NVarChar),

            };
            parameters[0].Value = ma;
            return base.executeStoreProcedureNoReturnTable("thucHienCheckOut", parameters);
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

        public virtual bool executeMoveTableQuery(int maBill, int maBanNew)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@idBill", SqlDbType.Int),
                new SqlParameter("@idNew", SqlDbType.Int),
            };
            parameters[0].Value = maBill;
            parameters[1].Value = maBanNew;

            return base.executeStoreProcedureNoReturnTable("chuyenBan", parameters);

        }
        public virtual DataTable executeReportPaginateQuery(int page, DateTime dateStart, DateTime dateEnd)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("@dateStart", SqlDbType.VarChar),
              new SqlParameter("@dateEnd", SqlDbType.VarChar),
              new SqlParameter("@page_num", SqlDbType.Int),
            };

            parameters[0].Value = dateStart.ToString("yyyy-MM-dd");
            parameters[1].Value = dateEnd.ToString("yyyy-MM-dd") + " 23:59:59";
            parameters[2].Value = page;
            return base.executeStoreProcedure("HienThiDoanhThuPhanTrang", parameters);

        }
        public virtual DataTable executeTotalReport( DateTime dateStart, DateTime dateEnd)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@dateStart", SqlDbType.Date),
             new SqlParameter("@dateEnd", SqlDbType.Date),
            };

            parameters[0].Value = dateStart;
            parameters[1].Value = dateEnd;
            return base.executeStoreProcedure("HienThiTongDoanhThu", parameters);
        }

    }
}
