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
    
    public partial class BienBanViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<Vimas.Models.Entities.BienBan>
    {
    	
    			public virtual int id { get; set; }
    			public virtual int idThongTinCaNhan { get; set; }
    			public virtual string GhiChu { get; set; }
    			public virtual string HinhAnh { get; set; }
    			public virtual bool Active { get; set; }
    	
    	public BienBanViewModel() : base() { }
    	public BienBanViewModel(Vimas.Models.Entities.BienBan entity) : base(entity) { }
    
    }
}
