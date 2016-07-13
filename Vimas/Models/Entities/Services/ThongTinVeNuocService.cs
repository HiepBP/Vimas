using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.Models.Entities.Services
{
    public partial interface IThongTinVeNuocService
    {
        IQueryable<ThongTinVeNuoc> GetByIdThongTinCaNhan(int idThongTinCaNhan);
    }

    public partial class ThongTinVeNuocService
    {
        public IQueryable<ThongTinVeNuoc> GetByIdThongTinCaNhan(int idThongTinCaNhan)
        {
            return this.GetActive(q => q.IdThongTinCaNhan == idThongTinCaNhan && q.Active);
        }
    }
}