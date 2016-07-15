using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class BangCapEditViewModel : BangCapViewModel
    {
        public BangCapEditViewModel() : base() { }

        public BangCapEditViewModel(BangCap entity) : base(entity) { }

        public BangCapEditViewModel(HopDongDOLABViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        [Display(Name = "Tháng")]
        [IsNumeric(ErrorMessage = "Vui lòng nhập số")]
        [MaxLength(2, ErrorMessage = "Tối đa 2 kí tự")]
        public override Nullable<int> Thang { get; set; }
        [Display(Name = "Năm")]
        [IsNumeric(ErrorMessage = "Vui lòng nhập số")]
        [MaxLength(4, ErrorMessage = "Tối đa 4 kí tự")]
        public override Nullable<int> Nam { get; set; }
        [Display(Name = "Bằng cấp")]
        public override string BangCap1 { get; set; }
        [Display(Name = "Đã nộp")]
        public override Nullable<bool> DaNop { get; set; }
        [Display(Name = "Trình độ")]
        public override string TrinhDo { get; set; }
    }
}