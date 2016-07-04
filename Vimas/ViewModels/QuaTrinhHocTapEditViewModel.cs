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
        
        [IsNumeric]
        [Display(Name = "Tên trường")]
        public override Nullable<int> TuNam { get; set; }
        [IsNumeric]
        public override Nullable<int> DenNam { get; set; }
        [Required]
        public override string TenTruong { get; set; }
        public override Nullable<int> LoaiTruong { get; set; }
        [Required]
        public override string NganhHoc { get; set; }
        public override Nullable<bool> DaTotNghiep { get; set; }
        public EducationLevel EducationLevel { get; set; }
    }
}