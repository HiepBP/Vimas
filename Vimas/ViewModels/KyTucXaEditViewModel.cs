using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

        [IsNumeric]
        [Display(Name = "Số phòng")]
        [Required(ErrorMessage = "Vui lòng nhập số phòng!!!")]
        public override string SoPhong { get; set; }

        [IsNumeric]
        [Display(Name = "Số hộc tủ đồ")]
        [Required(ErrorMessage = "Vui lòng nhập số hộc tủ đồ!!!")]
        public override string SoHocTuDo { get; set; }
        [Display(Name = "Ghi Chú")]
        public override string GhiChu { get; set; }
    }
}