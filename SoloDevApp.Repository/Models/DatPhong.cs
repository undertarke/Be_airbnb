using System;

namespace SoloDevApp.Repository.Models
{
    public class DatPhong
    {
        public int Id { get; set; }
        public int MaPhong { get; set; }
        public DateTime NgayDen { get; set; }
        public DateTime NgayDi { get; set; }
        public int SoLuongKhach { get; set; }
        public int MaNguoiDung { get; set; }

    }
}