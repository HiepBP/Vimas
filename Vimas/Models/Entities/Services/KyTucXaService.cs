using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.Models.Entities.Services
{
    public partial interface IKyTucXaService
    {
        IQueryable<KyTucXa> GetByIdThongTinCaNhan(int idThongTinCaNhan);
    }

    public partial class KyTucXaService
    {
        public IQueryable<KyTucXa> GetByIdThongTinCaNhan(int idThongTinCaNhan)
        {
            return this.GetActive(q => q.IdThongTinCaNhan == idThongTinCaNhan && (bool)q.Active );
        }
    }
}