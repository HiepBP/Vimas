using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class ThongTinDuTuyenEditViewModel : ThongTinDuTuyenViewModel
    {
        public ThongTinDuTuyenEditViewModel() : base() { }

        public ThongTinDuTuyenEditViewModel(ThongTinDuTuyen entity) : base(entity) { }

        public ThongTinDuTuyenEditViewModel(ThongTinCaNhanViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }

        #region base
        [Display(Name = "Ngày đăng ký")]
        [Required(ErrorMessage = "Vui lòng nhập ngày đăng ký!!!")]
        public override DateTime NgayDangKy { get; set; }
        [Display(Name = "Ưu điểm")]
        public override string UuDiem { get; set; }
        [Display(Name = "Nhược điểm")]
        public override string NhuocDiem { get; set; }
        [Display(Name = "Kỹ năng khác")]
        public override string KyNangKhac { get; set; }
        [Display(Name = "Sở thích")]
        public override string SoThich { get; set; }
        [Display(Name = "Lý do đi Nhật")]
        public override string LyDoDiNhat { get; set; }
        [Display(Name = "Về nước làm gì")]
        public override string VeNuocLamGi { get; set; }
        [Display(Name = "Người giới thiệu")]
        public override string NguoiGioiThieu { get; set; }
        [Display(Name = "Bạn bè bên Nhật")]
        public override string BanBeBenNhat { get; set; }
        [Display(Name = "Anh chị bên Nhật")]
        public override string AnhChiBenNhat { get; set; }
        [Display(Name = "Anh chị Đăng ký ở Vimas")]
        public override Nullable<int> AnhChiDangKyOVimas { get; set; }
        [Display(Name = "Đã đăng kí đi Nhật ở công ty khác")]
        [Required(ErrorMessage = "Vui lòng cho biết thông tin này!!!")]
        public override bool DaDKDiNhatOCongtyKhac { get; set; }
        [Display(Name = "Đã đi nước ngoài")]
        [Required(ErrorMessage = "Vui lòng cho biết thông tin này!!!")]
        public override bool DaDiNuocNgoai { get; set; }
        [Display(Name = "Đã đi Nhật")]
        [Required(ErrorMessage = "Vui lòng cho biết thông tin này!!!")]
        public override bool DaDiNhat { get; set; }
        [Display(Name = "Ghi chú sơ vấn")]
        public override string GhiChuSoVan { get; set; }
        #endregion

        public IEnumerable<SelectListItem> AvailableThongTinCaNhan { get; set; }
    }
}