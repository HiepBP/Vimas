using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vimas.Models;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class ThongTinGiaDinhEditViewModel : ThongTinGiaDinhViewModel
    {
        public ThongTinGiaDinhEditViewModel() : base() { }

        public ThongTinGiaDinhEditViewModel(ThongTinGiaDinh entity) : base(entity) { }

        public ThongTinGiaDinhEditViewModel(QuaTrinhHocTapViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập Họ tên")]
        public override string HoTen { get; set; }
        [Display(Name = "Nghề nghiệp")]
        public override string NgheNghiep { get; set; }
        [Display(Name = "Nơi làm việc")]
        public override string NoiLamViec { get; set; }
        [Display(Name = "Địa chỉ")]
        public override string DiaChi { get; set; }
        [Display(Name = "Số điện thoại")]
        [IsNumeric(ErrorMessage = "Yêu cầu nhập số")]
        public override string SoDienThoai { get; set; }

        public Relation Relation { get; set; }
    }
}