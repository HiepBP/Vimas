//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vimas.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class CongTyTiepNhanViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<Vimas.Models.Entities.CongTyTiepNhan>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual string TenTiengAnh { get; set; }
    			public virtual string TenTiengNhat { get; set; }
    			public virtual string NganhNghe { get; set; }
    			public virtual string NguoiDaiDien { get; set; }
    			public virtual string DiaChi { get; set; }
    			public virtual string DienThoai { get; set; }
    			public virtual string Fax { get; set; }
    			public virtual Nullable<int> idNghiepDoan { get; set; }
    			public virtual bool Active { get; set; }
    	
    	public CongTyTiepNhanViewModel() : base() { }
    	public CongTyTiepNhanViewModel(Vimas.Models.Entities.CongTyTiepNhan entity) : base(entity) { }
    
    }
}
