//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vimas.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class KyTucXa
    {
        public int id { get; set; }
        public Nullable<int> idThongTinCaNhan { get; set; }
        public Nullable<System.DateTime> NgayVao { get; set; }
        public Nullable<System.DateTime> NgayRa { get; set; }
        public Nullable<int> SoPhong { get; set; }
        public Nullable<int> SoHocTuDo { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> isActive { get; set; }
    
        public virtual ThongTinCaNhan ThongTinCaNhan { get; set; }
    }
}
