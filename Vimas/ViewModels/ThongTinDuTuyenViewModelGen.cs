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
    
    public partial class ThongTinDuTuyenViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<Vimas.Models.Entities.ThongTinDuTuyen>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual int IdThongTinCaNhan { get; set; }
    			public virtual System.DateTime NgayDangKy { get; set; }
    			public virtual string UuDiem { get; set; }
    			public virtual string NhuocDiem { get; set; }
    			public virtual string KyNangKhac { get; set; }
    			public virtual string SoThich { get; set; }
    			public virtual string LyDoDiNhat { get; set; }
    			public virtual string VeNuocLamGi { get; set; }
    			public virtual string NguoiGioiThieu { get; set; }
    			public virtual string BanBeBenNhat { get; set; }
    			public virtual string AnhChiBenNhat { get; set; }
    			public virtual string AnhChiDangKyOVimas { get; set; }
    			public virtual bool DaDKDiNhatOCongtyKhac { get; set; }
    			public virtual bool DaDiNuocNgoai { get; set; }
    			public virtual bool DaDiNhat { get; set; }
    			public virtual string GhiChuSoVan { get; set; }
    			public virtual bool Active { get; set; }
    	
    	public ThongTinDuTuyenViewModel() : base() { }
    	public ThongTinDuTuyenViewModel(Vimas.Models.Entities.ThongTinDuTuyen entity) : base(entity) { }
    
    }
}
