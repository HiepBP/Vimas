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
    public class KyTucXaEditViewModel : KyTucXaViewModel
    {
        public KyTucXaEditViewModel() : base() { }

        public KyTucXaEditViewModel(KyTucXa entity) : base(entity) { }

        public KyTucXaEditViewModel(KyTucXaViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }

        [Display(Name = "Ngày vào")]
        [Required(ErrorMessage = "Vui lòng nhập ngày vào!!!")]
        public override Nullable<System.DateTime> NgayVao { get; set; }

        [Display(Name = "Ngày ra")]
        public override Nullable<System.DateTime> NgayRa { get; set; }

        [Display(Name = "Số phòng")]
        [Required(ErrorMessage = "Vui lòng nhập số phòng!!!")]
        public override string SoPhong { get; set; }

        [Display(Name = "Số hộc tủ đồ")]
        public override string SoHocTuDo { get; set; }

        [Display(Name = "Ghi Chú")]
        public override string GhiChu { get; set; }

        public ThongTinCaNhanEditViewModel ThongTinCaNhan { get; set; }

        public IEnumerable<SelectListItem> AvailableThongTinCaNhan { get; set; }
    }
}