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
    
    
    public partial interface IAspNetRolesRepository : SkyWeb.DatVM.Data.IBaseRepository<AspNetRoles>
    {
    }
    
    public partial class AspNetRolesRepository : SkyWeb.DatVM.Data.BaseRepository<AspNetRoles>, IAspNetRolesRepository
    {
    	public AspNetRolesRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
