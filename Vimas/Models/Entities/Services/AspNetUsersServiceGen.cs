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
    
    
    public partial interface IAspNetUsersService : SkyWeb.DatVM.Data.IBaseService<AspNetUsers>
    {
    }
    
    public partial class AspNetUsersService : SkyWeb.DatVM.Data.BaseService<AspNetUsers>, IAspNetUsersService
    {
        public AspNetUsersService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IAspNetUsersRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
