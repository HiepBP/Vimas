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
    
    public partial class NghiepDoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NghiepDoan()
        {
            this.CongTyTiepNhan = new HashSet<CongTyTiepNhan>();
        }
    
        public int Id { get; set; }
        public string MaNghiepDoan { get; set; }
        public string TenNghiepDoan { get; set; }
        public string TenVietTat { get; set; }
        public string NguoiDaiDien { get; set; }
        public string ChucDanh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public Nullable<System.DateTime> NgayKyHopDong { get; set; }
        public Nullable<decimal> LuongCoBan { get; set; }
        public Nullable<decimal> PhiDichVu { get; set; }
        public Nullable<decimal> PhiUTDT { get; set; }
        public string WebsiteUrl { get; set; }
        public bool Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongTyTiepNhan> CongTyTiepNhan { get; set; }
    }
}
