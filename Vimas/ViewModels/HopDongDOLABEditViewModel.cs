using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class HopDongDOLABEditViewModel : HopDongDOLABViewModel
    {
        public HopDongDOLABEditViewModel() : base() { }

        public HopDongDOLABEditViewModel(HopDongDOLAB entity) : base(entity) { }

        public HopDongDOLABEditViewModel(HopDongDOLABViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        [Display(Name = "Ngày đăng ký")]
        public override Nullable<System.DateTime> NgayDangKy { get; set; }
        [Display(Name = "Ngày nhận")]
        public override Nullable<System.DateTime> NgayNhan { get; set; }
        [Display(Name = "Số đăng ký hợp đồng")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public override string SoDKHopDong { get; set; }
        [Display(Name = "Số công văn")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public override string SoCongVan { get; set; }
        [Display(Name = "Số phiếu tiếp nhận")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public override string SoPhieuTiepNhan { get; set; }
        public IEnumerable<int> SelectedThongTinCaNhan { get; set; }
    }
}