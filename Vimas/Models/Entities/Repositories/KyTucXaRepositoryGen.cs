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
    
    
    public partial interface IKyTucXaRepository : SkyWeb.DatVM.Data.IBaseRepository<KyTucXa>
    {
    }
    
    public partial class KyTucXaRepository : SkyWeb.DatVM.Data.BaseRepository<KyTucXa>, IKyTucXaRepository
    {
    	public KyTucXaRepository(System.Data.Entity.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
