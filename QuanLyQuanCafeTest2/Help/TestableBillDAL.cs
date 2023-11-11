using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafeTest.Help
{
    public class TestableBillDAL : BillDAL
    {
     
        public int ReturnValueForGetUncheckBillByTable { get; set; }

        public override int getUncheckBillByTable(int id)
        {
            return ReturnValueForGetUncheckBillByTable;
        }
    }
}
