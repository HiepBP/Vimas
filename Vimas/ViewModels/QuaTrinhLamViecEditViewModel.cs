using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class QuaTrinhLamViecEditViewModel: QuaTrinhLamViecViewModel
    {
          public QuaTrinhLamViecEditViewModel() : base() { }

        public QuaTrinhLamViecEditViewModel(QuaTrinhLamViec entity) : base(entity) { }

        public QuaTrinhLamViecEditViewModel(QuaTrinhHocTapViewModel original, IMapper mapper)
            : this()
        {
            mapper.Map(original, this);
        }
        
         [IsNumeric(ErrorMessage = "Vui lòng nhập số!!!")]
        [Display(Name = "Từ năm")]
        public override Nullable<int> TuNam { get; set; }

         [IsNumeric(ErrorMessage = "Vui lòng nhập số!!!")]
        [Display(Name = "Đến năm")]
        public override Nullable<int> DenNam { get; set; }


         [Required(ErrorMessage = "Vui lòng nhập tên công ty!!!")]
        [Display(Name = "Tên công ty")]
        public override string TenCongTy { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập hình thức công ty!!!")]
        [Display(Name = "Hình thức công ty")]
        public override string HinhThucCongTy { get; set; }
         [Required(ErrorMessage = "Vui lòng nhập chi tiết công việc!!!")]
        [Display(Name = "Chi tiết công việc")]
        public override string ChiTietCongViec { get; set; }
        [Required]
        [Display(Name = "Địa chỉ công ty")]
        public override string DiaChiCongTy { get; set; }
        [Display(Name = "Đang làm")]
        public override Nullable<bool> DangLam { get; set; }
    }
}