using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.Models.Entities.Services
{
    public partial interface IQuaTrinhHocTapService
    {
        IQueryable<QuaTrinhHocTap> GetByIdThongTinCaNhan(int idThongTinCaNhan);
    }

    public partial class QuaTrinhHocTapService
    {
        public IQueryable<QuaTrinhHocTap> GetByIdThongTinCaNhan(int idThongTinCaNhan)
        {
            return this.Get(q => q.IdThongTinCaNhan == idThongTinCaNhan);
        }
    }
}