using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vimas.ViewModels
{
    public class KyTucXaHocVienViewModel
    {
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Tên phiên âm Nhật")]
        public string TenPhienAmNhat { get; set; }

        [Display(Name = "Ngày sinh")]
        public System.DateTime NgaySinh { get; set; }

        [Display(Name = "Quê quán")]
        public string QueQuan { get; set; }

        [Display(Name = "Địa chỉ thường trú")]
        public string DiaChiThuongTru { get; set; }

        [Display(Name = "Ngày vào")]
        public System.DateTime NgayVao { get; set; }

        [Display(Name = "Ngày ra")]
        public System.DateTime NgayRa { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Số phòng")]
        public string SoPhong { get; set; }

        [Display(Name = "Số hộc tủ đồ")]
        public string SoHocTuDo { get; set; }
    }
}