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
    
    
    public partial interface IKyTucXaService : SkyWeb.DatVM.Data.IBaseService<KyTucXa>
    {
    }
    
    public partial class KyTucXaService : SkyWeb.DatVM.Data.BaseService<KyTucXa>, IKyTucXaService
    {
        public KyTucXaService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IKyTucXaRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
