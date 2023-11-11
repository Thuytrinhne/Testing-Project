using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FoodDTO
    {
        private int id;
        public int Id { get { return id; } set { id = value; } }

        private string tenMon;
        public string TenMon { get { return tenMon; } set { tenMon = value; } }

        private double gia;
        public double Gia  { get { return gia; } set { gia = value; } }

        private int loai ;
        public int Loai { get { return loai; } set { loai = value; } }


        public FoodDTO() { }    
        public FoodDTO(int id, string ten, double gia, int loai)
        {
            this.id = id;
            this.tenMon = ten;
            this.gia = gia;
            this.loai = loai;

        }
        public FoodDTO (DataRow dr)
        {
            this.id =(int) dr["id"];
            this.tenMon = dr["name"].ToString();
            this.gia = (float) Convert.ToDouble( (dr["price"]));
            this.loai = (int)dr["idCategory"]; 

        }

    }
}
