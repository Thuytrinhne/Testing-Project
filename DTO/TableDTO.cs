using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace DTO
{
    public class TableDTO
    {
        private  int id;
        private  string name;
        private  string status;
        public TableDTO(int id, string name, string status)
        {
            this.id = id;   
            this.name = name;
            this.status = status;
        }
        public TableDTO() { }
        public TableDTO(DataRow dr)
          {
            this.id = (int)dr["Mã bàn ăn"];
            this.name =( (string)dr["Name"]).Trim();
            this.status = ((string )dr["status"]).Trim();

          }
         public int Id { get { return this.id; } set { this.id = value; } }
         public string Name { get { return this.name; } set { this.name = value; } }
         public string Status { get { return this.status; }
            set
            {
                this.status = value;
            }
         }


    }
}
