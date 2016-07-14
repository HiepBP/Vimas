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
    public class ThongTinVeNuocEditViewModel : ThongTinVeNuocViewModel
    {
        public ThongTinVeNuocEditViewModel() : base() { }

        public ThongTinVeNuocEditViewModel(ThongTinVeNuoc entity) : base(entity) { }

        public ThongTinVeNuocEditViewModel(ThongTinVeNuocViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }

        [Display(Name = "Ngày đi")]
        [Required(ErrorMessage = "Vui lòng nhập ngày đi!!!")]
        public override Nullable<System.DateTime> NgayDi { get; set; }

        [Display(Name = "Ngày về")]
        [Required(ErrorMessage = "Vui lòng nhập ngày về!!!")]
        public override Nullable<System.DateTime> NgayVe { get; set; }

        [Display(Name = "Lý do về")]
        public override string LyDoVe { get; set; }

        [Display(Name = "Thanh lý hợp đồng")]
        [Required(ErrorMessage = "Vui lòng cho biết đã thanh lý hợp đồng hay chưa?")]
        public override Nullable<bool> ThanhLyHopDong { get; set; }

        [Display(Name = "Ngày thanh lý")]
        public override Nullable<System.DateTime> NgayThanhLy { get; set; }

        [Display(Name = "Số hợp đồng thanh lý")]
        public override string SoHopDongThanhLy { get; set; }

        public ThanhLyHopDong ThanhLy { get; set; }
    }
}