using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vimas.ViewModels
{
    [MetadataType(typeof(ThongTinPhongVanMetaData))]
    public partial class ThongTinPhongVanViewModel
    {
        public ThongTinCaNhanViewModel ThongTinCaNhan { get; set; }
        public CongTyTiepNhanViewModel CongTyTiepNhan { get; set; }
        public CongTyChungNgheViewModel CongTyChungNghe { get; set; }
    }

    public class ThongTinPhongVanMetaData
    {
        [Display(Name = "Ngày phỏng vấn")]
        public Nullable<System.DateTime> NgayPhongVan { get; set; }
        [Display(Name = "Nghề phỏng vấn")]
        public string NghePhongVan { get; set; }
        [Display(Name = "Ghi chú phỏng vấn")]
        public string GhiChuPV { get; set; }
        [Display(Name = "Ngày trúng tuyển")]
        public Nullable<System.DateTime> NgayTrungTuyen { get; set; }
        [Display(Name = "Đợt trúng tuyển")]
        public string DotTrungTuyen { get; set; }
        [Display(Name = "Nghề trúng tuyển tiếng anh")]
        public string NgheTrungTuyenTiengAnh { get; set; }
        [Display(Name = "Nghề trúng tuyển tiếng việt")]
        public string NgheTrungTuyenTiengViet { get; set; }
        [Display(Name = "Nghiệp đoàn")]
        public Nullable<int> IdNghiepDoan { get; set; }
        [Display(Name = "Công ty tiếp nhận")]
        public Nullable<int> IdCongTyTiepNhan { get; set; }
        [Display(Name = "Thời hạn hợp đồng")]
        [IsNumeric]
        public Nullable<int> ThoiHanHopDong { get; set; }
        [Display(Name = "Ghi chú sau trúng tuyển")]
        public string GhiChuSauTrungTuyen { get; set; }
        [Display(Name = "Lớp học")]
        public string LopHoc { get; set; }
        [Display(Name = "Ngày nhập học")]
        public Nullable<System.DateTime> NgayNhapHoc { get; set; }
        [Display(Name = "Ngày kết thúc khóa học")]
        public Nullable<System.DateTime> NgayKetThucKhoaHoc { get; set; }
        [Display(Name = "Ghi chú khen thưởng/kỷ luật")]
        public string GhiChuKhenThuongKyLuat { get; set; }
        [Display(Name = "Số phiếu tiếp nhận")]
        public string SoPhieuTiepNhan { get; set; }
        [Display(Name = "Ghi chú phái cử")]
        public string GhiChuPhaiCu { get; set; }
        [Display(Name = "Hợp đồng TTS")]
        public Nullable<int> HopDongTTS { get; set; }
        [Display(Name = "Ngày ký hợp đồng")]
        public Nullable<System.DateTime> NgayKyHopDong { get; set; }
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        [Display(Name = "Ngày hủy sau khi trúng tuyển")]
        public Nullable<System.DateTime> NgayHuySauKhiTrungTuyen { get; set; }
        [Display(Name = "Lý do hủy")]
        public string LyDoHuy { get; set; }
    }
}