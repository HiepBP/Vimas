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
    public class ThongTinCaNhanEditViewModel : ThongTinCaNhanViewModel
    {
        public ThongTinCaNhanEditViewModel() : base() { }

        public ThongTinCaNhanEditViewModel(ThongTinCaNhan entity) : base(entity) { }

        public ThongTinCaNhanEditViewModel(ThongTinCaNhanViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }

        [Required(ErrorMessage = "Vui lòng nhập họ tên!!!")]
        public override string HoTen
        {
            get
            {
                return base.HoTen;
            }

            set
            {
                base.HoTen = value;
            }
        }

        [StringLength(12,MinimumLength = 9,ErrorMessage ="Độ dài CMND từ 9 đến 12 số!!!")]
        [IsNumeric(ErrorMessage = "Vui lòng nhập số!!!")]
        public override string CMND
        {
            get
            {
                return base.CMND;
            }

            set
            {
                base.CMND = value;
            }
        }

        public Gender Gender { get; set; }
        public FamilyStatus FamilyStatus { get; set; }
        public IEnumerable<SelectListItem> AvailableMaNguon { get; set; }
    }

    public class IsNumericAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal val;
                var isNumeric = decimal.TryParse(value.ToString(), out val);

                if (!isNumeric)
                {
                    return new ValidationResult("Must be numeric");
                }
            }

            return ValidationResult.Success;
        }
    }
}