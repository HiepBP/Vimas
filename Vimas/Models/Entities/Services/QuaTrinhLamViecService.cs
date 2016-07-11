using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.Models.Entities.Services
{
    public partial interface IQuaTrinhLamViecService
    {
        IQueryable<QuaTrinhLamViec> GetByIdThongTinCaNhan(int idThongTinCaNhan);
    }

    public partial class QuaTrinhLamViecService
    {
        public IQueryable<QuaTrinhLamViec> GetByIdThongTinCaNhan(int idThongTinCaNhan)
        {
            return this.Get(q => q.IdThongTinCaNhan == idThongTinCaNhan && q.Active);
        }
    }
}