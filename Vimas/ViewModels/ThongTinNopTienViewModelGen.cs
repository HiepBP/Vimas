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
    
    public partial class ThongTinNopTienViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<Vimas.Models.Entities.ThongTinNopTien>
    {
    	
    			public virtual int Id { get; set; }
    			public virtual int IdThongTinCaNhan { get; set; }
    			public virtual int LoaiTien { get; set; }
    			public virtual string SoPhieu { get; set; }
    			public virtual System.DateTime NgayLapPhieu { get; set; }
    			public virtual int ThuHayChi { get; set; }
    			public virtual decimal SoTien { get; set; }
    			public virtual string LyDo { get; set; }
    			public virtual bool Active { get; set; }
    	
    	public ThongTinNopTienViewModel() : base() { }
    	public ThongTinNopTienViewModel(Vimas.Models.Entities.ThongTinNopTien entity) : base(entity) { }
    
    }
}
