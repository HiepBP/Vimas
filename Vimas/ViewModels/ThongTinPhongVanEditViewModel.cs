using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class ThongTinPhongVanEditViewModel : ThongTinPhongVanViewModel
    {
        public ThongTinPhongVanEditViewModel() : base() { }

        public ThongTinPhongVanEditViewModel(ThongTinPhongVan entity) : base(entity) { }

        public ThongTinPhongVanEditViewModel(ThongTinPhongVanViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        
        [Display(Name = "Ngày phỏng vấn")]
        public override Nullable<System.DateTime> NgayPhongVan { get; set; }
        [Display(Name = "Nghề phỏng vấn")]
        public override string NghePhongVan { get; set; }
        [Display(Name = "Ghi chú phỏng vấn")]
        public override string GhiChuPV { get; set; }
        [Display(Name = "Ngày trúng tuyển")]
        public override Nullable<System.DateTime> NgayTrungTuyen { get; set; }
        [Display(Name = "Đợt trúng tuyển")]
        public override string DotTrungTuyen { get; set; }
        [Display(Name = "Nghề trúng tuyển tiếng anh")]
        public override string NgheTrungTuyenTiengAnh { get; set; }
        [Display(Name = "Nghề trúng tuyển tiếng việt")]
        public override string NgheTrungTuyenTiengViet { get; set; }
        [Display(Name = "Công ty tiếp nhận")]
        public override Nullable<int> IdCongTyTiepNhan { get; set; }
        [Display(Name = "Thời hạn hợp đồng")]
        [IsNumeric]
        public override Nullable<int> ThoiHanHopDong { get; set; }
        [Display(Name = "Ghi chú sau trúng tuyển")]
        public override string GhiChuSauTrungTuyen { get; set; }
        [Display(Name = "Lớp học")]
        public override string LopHoc { get; set; }
        [Display(Name = "Ngày nhập học")]
        public override Nullable<System.DateTime> NgayNhapHoc { get; set; }
        [Display(Name = "Ngày kết thúc khóa học")]
        public override Nullable<System.DateTime> NgayKetThucKhoaHoc { get; set; }
        [Display(Name = "Ghi chú khen thưởng/kỷ luật")]
        public override string GhiChuKhenThuongKyLuat { get; set; }
        [Display(Name = "Số phiếu tiếp nhận")]
        public override string SoPhieuTiepNhan { get; set; }
        [Display(Name = "Ghi chú phái cử")]
        public override string GhiChuPhaiCu { get; set; }
        [Display(Name = "Hợp đồng TTS")]
        public override Nullable<int> HopDongTTS { get; set; }
        [Display(Name = "Ngày ký hợp đồng")]
        public override Nullable<System.DateTime> NgayKyHopDong { get; set; }
        [Display(Name = "Hình ảnh")]
        public override string HinhAnh { get; set; }
        [Display(Name = "Ngày hủy sau khi trúng tuyển")]
        public override Nullable<System.DateTime> NgayHuySauKhiTrungTuyen { get; set; }
        [Display(Name = "Lý do hủy")]
        public override string LyDoHuy { get; set; }

        [Display(Name = "Công ty chứng nghề")]
        public IEnumerable<SelectListItem> AvailableCongTyChungNghe { get; set; }
        [Display(Name ="Công ty tiếp nhận")]
        public IEnumerable<SelectListItem> AvailableCongTyTiepNhan { get; set; }

        public HttpPostedFileBase HinhAnhLogo { get; set; }
        [Display(Name ="Thời hạn hợp đồng")]
        public ThoiHanHopDong? ThoiHanHopDongEnum { get; set; }
    }
}