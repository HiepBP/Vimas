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
    
    
    public partial interface IAspNetUserClaimsService : SkyWeb.DatVM.Data.IBaseService<AspNetUserClaims>
    {
    }
    
    public partial class AspNetUserClaimsService : SkyWeb.DatVM.Data.BaseService<AspNetUserClaims>, IAspNetUserClaimsService
    {
        public AspNetUserClaimsService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IAspNetUserClaimsRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}