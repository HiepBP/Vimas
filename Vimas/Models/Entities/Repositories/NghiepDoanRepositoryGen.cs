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
    
    
    public partial interface INghiepDoanRepository : SkyWeb.DatVM.Data.IBaseRepository<NghiepDoan>
    {
    }
    
    public partial class NghiepDoanRepository : SkyWeb.DatVM.Data.BaseRepository<NghiepDoan>, INghiepDoanRepository
    {
    	public NghiepDoanRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
