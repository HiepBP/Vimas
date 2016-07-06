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
    public class ThongTinNopTienEditViewModel : ThongTinNopTienViewModel
    {
        public ThongTinNopTienEditViewModel() : base() { }

        public ThongTinNopTienEditViewModel(ThongTinNopTien entity) : base(entity) { }

        public ThongTinNopTienEditViewModel(QuaTrinhHocTapViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        [Display(Name ="Số phiếu")]
        [Required]
        public override string SoPhieu { get; set; }
        [Display(Name ="Ngày lập phiếu")]
        public override System.DateTime NgayLapPhieu { get; set; }
        [Display(Name ="Số tiền")]
        [Required, IsNumeric]
        public override decimal SoTien { get; set; }
        [Display(Name = "Lý do")]
        public override string LyDo { get; set; }
        [Display(Name = "Thu/chi")]
        public ThuChi ThuChi { get; set; }
        [Display(Name ="Người nộp tiền")]
        public IEnumerable<SelectListItem> AvailableThongTinCaNhan { get; set; }
        [Display(Name ="Loại tiền")]
        public TypeOfMoney TypeOfMoney { get; set; }
    }
}