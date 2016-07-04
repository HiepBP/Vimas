using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vimas.Models
{
    public enum FamilyStatus
    {
        [Display(Name = "Độc thân")]
        Single = 0,

        [Display(Name = "Có gia đình")]
        Married = 1,
    }

    public enum Gender
    {
        [Display(Name = "Nam")]
        Male = 1,
        [Display(Name = "Nữ")]
        Female = 0,
    }

    public enum EducationLevel
    {
        [Display(Name = "Tiểu học")]
        TieuHoc = 0,
        [Display(Name = "Trung học cơ sở")]
        TrungHocCoSo = 1,
        [Display(Name = "Trung học phổ thông")]
        TrungHocPhoThong = 2,
        [Display(Name = "Đại học")]
        DaiHoc = 3,
        [Display(Name = "Sau đại học")]
        SauDaiHoc = 4,
    }
}