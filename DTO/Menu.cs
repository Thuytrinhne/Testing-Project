using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class Menu
    {
        private string tenMon;
        public string TenMon { get {  return tenMon; } set {  tenMon = value; } }

        private int soLuong;
        public int SoLuong { get {  return soLuong; } set {  soLuong = value; } }

        private float donGia;
        public float DonGia { get { return donGia; } set { donGia = value; } }

        private float thanhTien;
        public float ThanhTien { get { return thanhTien; } set { thanhTien = value; } }


        public Menu (DataRow dr)
        {
            this.tenMon = dr["Tên món ăn"].ToString();
            this.soLuong =(int) dr["Số lượng"];
            this.donGia = (float)(Convert.ToDouble(dr["Đơn giá"]));
            this.thanhTien = (float)(Convert.ToDouble(dr["Thành tiền"]));

        }



    }
}
