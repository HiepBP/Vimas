//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vimas.Models.Entities.Services
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface ICongTyTiepNhanService : SkyWeb.DatVM.Data.IBaseService<CongTyTiepNhan>
    {
    }
    
    public partial class CongTyTiepNhanService : SkyWeb.DatVM.Data.BaseService<CongTyTiepNhan>, ICongTyTiepNhanService
    {
        public CongTyTiepNhanService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.ICongTyTiepNhanRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
