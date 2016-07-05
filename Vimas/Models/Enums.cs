﻿using System;
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

        [Display(Name = "Ly dị")]
        Divorce = 2,
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
        [Display(Name = "Trung cấp")]
        TrungCap = 3,
        [Display(Name = "Cao đẳng")]
        CaoDang = 4,
        [Display(Name = "Đại học")]
        DaiHoc = 5,
    }

    public enum Relation
    {
        [Display(Name = "Ba")]
        Ba = 0,
        [Display(Name = "Mẹ")]
        Me = 1,
        [Display(Name = "Anh")]
        Anh = 2,
        [Display(Name = "Chị")]
        Chi = 3,
        [Display(Name = "Em")]
        Em = 4,
        [Display(Name = "Anh Họ")]
        AnhHo = 5,
        [Display(Name = "Chị Họ")]
        ChiHo = 6,
        [Display(Name = "Cậu")]
        Cau = 7,
        [Display(Name = "Dì")]
        Di = 8,
    }
}