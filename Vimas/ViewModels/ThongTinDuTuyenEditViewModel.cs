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

        [Display(Name = "Anh chị Đăng ký ở Vimas")]
        [IsNumeric]
        public override string AnhChiDangKyOVimas { get; set; }

        [Display(Name = "Đã đăng kí đi Nhật ở công ty khác")]
        [Required(ErrorMessage = "Vui lòng cho biết thông tin này!!!")]
        public override bool DaDKDiNhatOCongtyKhac { get; set; }
        [Display(Name = "Đã đi nước ngoài")]
        [Required(ErrorMessage = "Vui lòng cho biết thông tin này!!!")]
        public override bool DaDiNuocNgoai { get; set; }
        [Display(Name = "Đã đi Nhật")]
        [Required(ErrorMessage = "Vui lòng cho biết thông tin này!!!")]
        public override bool DaDiNhat { get; set; }
        #endregion

        public IEnumerable<SelectListItem> AvailableThongTinCaNhan { get; set; }
        public string HoTen { get; set; }
    }
}