using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.Models.Entities.Services
{
    public partial interface IThongTinPhongVanService
    {
        IQueryable<ThongTinPhongVan> GetByIdThongTinCaNhan(int idThongTinCaNhan);
    }

    public partial class ThongTinPhongVanService
    {
        public IQueryable<ThongTinPhongVan> GetByIdThongTinCaNhan(int idThongTinCaNhan)
        {
            return this.GetActive(q => q.IdThongTinCaNhan == idThongTinCaNhan && q.Active);
        }
    }
}