//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vimas.Models.Entities.Repositories
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface ISystemLogRepository : SkyWeb.DatVM.Data.IBaseRepository<SystemLog>
    {
    }
    
    public partial class SystemLogRepository : SkyWeb.DatVM.Data.BaseRepository<SystemLog>, ISystemLogRepository
    {
    	public SystemLogRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
