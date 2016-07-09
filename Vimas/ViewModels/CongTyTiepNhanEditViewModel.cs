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
    public class CongTyTiepNhanEditViewModel : CongTyTiepNhanViewModel
    {
        public CongTyTiepNhanEditViewModel() : base() { }

        public CongTyTiepNhanEditViewModel(CongTyTiepNhan entity) : base(entity) { }

        public CongTyTiepNhanEditViewModel(CongTyTiepNhanViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }

        [Display(Name = "Tên tiếng anh")]
        [Required(ErrorMessage = "Vui lòng nhập tên tiếng anh!")]
        public override string TenTiengAnh { get; set; }
        [Display(Name = "Tên tiếng việt")]
        [Required(ErrorMessage = "Vui lòng nhập tên tiếng việt!")]
        public override string TenTiengViet { get; set; }
        [Display(Name = "Ngành nghề")]
        public override string NganhNghe { get; set; }
        [Display(Name = "Người đại diện")]
        public override string NguoiDaiDien { get; set; }
        [Display(Name = "Địa chỉ")]
        public override string DiaChi { get; set; }
        [Display(Name = "Điện thoại")]
        public override string DienThoai { get; set; }
        [Display(Name = "Fax")]
        public override string Fax { get; set; }

        [Display(Name = "Nghiệp đoàn")]
        public IEnumerable<SelectListItem> AvailableNghiepDoan { get; set; }
    }
}