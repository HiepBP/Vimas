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
    
    public partial class SystemLogViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<Vimas.Models.Entities.SystemLog>
    {
    	
    			public virtual int no { get; set; }
    			public virtual Nullable<int> id { get; set; }
    			public virtual string TenBang { get; set; }
    			public virtual string HanhDong { get; set; }
    			public virtual Nullable<int> ThucHienBoi { get; set; }
    			public virtual Nullable<System.DateTime> NgayThucHien { get; set; }
    	
    	public SystemLogViewModel() : base() { }
    	public SystemLogViewModel(Vimas.Models.Entities.SystemLog entity) : base(entity) { }
    
    }
}