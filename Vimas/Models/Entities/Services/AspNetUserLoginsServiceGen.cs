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
    
    
    public partial interface IAspNetUserLoginsService : SkyWeb.DatVM.Data.IBaseService<AspNetUserLogins>
    {
    }
    
    public partial class AspNetUserLoginsService : SkyWeb.DatVM.Data.BaseService<AspNetUserLogins>, IAspNetUserLoginsService
    {
        public AspNetUserLoginsService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IAspNetUserLoginsRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
