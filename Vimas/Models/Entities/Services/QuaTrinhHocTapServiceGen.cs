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
    
    
    public partial interface IQuaTrinhHocTapService : SkyWeb.DatVM.Data.IBaseService<QuaTrinhHocTap>
    {
    }
    
    public partial class QuaTrinhHocTapService : SkyWeb.DatVM.Data.BaseService<QuaTrinhHocTap>, IQuaTrinhHocTapService
    {
        public QuaTrinhHocTapService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.IQuaTrinhHocTapRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
