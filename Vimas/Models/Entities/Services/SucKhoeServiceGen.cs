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
    
    
    public partial interface ISucKhoeService : SkyWeb.DatVM.Data.IBaseService<SucKhoe>
    {
    }
    
    public partial class SucKhoeService : SkyWeb.DatVM.Data.BaseService<SucKhoe>, ISucKhoeService
    {
        public SucKhoeService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.ISucKhoeRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}