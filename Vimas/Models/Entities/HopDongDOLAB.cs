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
    
    public partial class HopDongDOLAB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HopDongDOLAB()
        {
            this.HopDongDOLABHocVienMappings = new HashSet<HopDongDOLABHocVienMapping>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> NgayDangKy { get; set; }
        public Nullable<System.DateTime> NgayNhan { get; set; }
        public string SoDKHopDong { get; set; }
        public string SoCongVan { get; set; }
        public string SoPhieuTiepNhan { get; set; }
        public Nullable<bool> Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDongDOLABHocVienMapping> HopDongDOLABHocVienMappings { get; set; }
    }
}