using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vimas.ViewModels
{
    [MetadataType(typeof(NghiepDoanViewModelMetaData))]
    public partial class NghiepDoanViewModel
    {
    }

    public class NghiepDoanViewModelMetaData
    {

        [IsNumeric(ErrorMessage = "Vui lòng chỉ nhập số")]
        public Nullable<decimal> LuongCoBan { get; set; }

        [IsNumeric(ErrorMessage = "Vui lòng chỉ nhập số")]
        public Nullable<decimal> PhiDichVu { get; set; }

        [IsNumeric(ErrorMessage = "Vui lòng chỉ nhập số")]
        public Nullable<decimal> PhiUTDT { get; set; }


        //public int Id { get; set; }
        //public string MaNghiepDoan { get; set; }
        //public string TenNghiepDoan { get; set; }
        //public string TenVietTat { get; set; }
        //public string NguoiDaiDien { get; set; }
        //public string ChucDanh { get; set; }
        //public string DiaChi { get; set; }
        //public string DienThoai { get; set; }
        //public string Fax { get; set; }
        //public Nullable<System.DateTime> NgayKyHopDong { get; set; }
        //public string WebsiteUrl { get; set; }
        //public bool Active { get; set; }
    }
}