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
    public class BienBanEditViewModel : BienBanViewModel
    {
        public BienBanEditViewModel() : base() { }

        public BienBanEditViewModel(BienBan entity) : base(entity) { }

        public BienBanEditViewModel(HopDongDOLABViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        [Display(Name = "Ghi Chú")]
        public override string GhiChu { get; set; }
        [Display(Name = "Hình Ảnh")]
        public override string HinhAnh { get; set; }

        public HttpPostedFileBase SelectedHinhAnh { get; set; }
        [Display(Name = "Học Viên")]
        public IEnumerable<SelectListItem> AvailableThongTinCaNhan { get; set; }
        public ThongTinCaNhanViewModel ThongTinCaNhan { get; set; }
    }
}