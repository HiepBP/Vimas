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
    
    
    public partial interface IBienBanService : SkyWeb.DatVM.Data.IBaseService<BienBan>
    {
    }
    
    public partial class BienBanService : SkyWeb.DatVM.Data.BaseService<BienBan>, IBienBanService
    {
        public BienBanService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IBienBanRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
