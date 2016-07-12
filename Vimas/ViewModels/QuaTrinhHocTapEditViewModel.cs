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
    public class QuaTrinhHocTapEditViewModel : QuaTrinhHocTapViewModel
    {
        public QuaTrinhHocTapEditViewModel() : base() { }

        public QuaTrinhHocTapEditViewModel(QuaTrinhHocTap entity) : base(entity) { }

        public QuaTrinhHocTapEditViewModel(QuaTrinhHocTapViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        
        [IsNumeric(ErrorMessage = "Vui lòng nhập số")]
        [Display(Name = "Từ năm")]
        public override Nullable<int> TuNam { get; set; }
        [IsNumeric(ErrorMessage = "Vui lòng nhập số")]
        [Display(Name = "Đến năm")]
        public override Nullable<int> DenNam { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Tên trường")]
        [Display(Name = "Tên trường")]
        public override string TenTruong { get; set; }
        [Display(Name = "Loại trường")]
        public override Nullable<int> LoaiTruong { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập ngành học")]
        [Display(Name = "Ngành học")]
        public override string NganhHoc { get; set; }
        [Display(Name = "Đã tốt nghiệp")]
        public override Nullable<bool> DaTotNghiep { get; set; }
        public EducationLevel EducationLevel { get; set; }
    }
}