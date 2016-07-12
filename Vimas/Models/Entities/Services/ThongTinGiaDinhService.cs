using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.Models.Entities.Services
{
    public partial interface IThongTinGiaDinhService
    {
        IQueryable<ThongTinGiaDinh> GetByIdThongTinCaNhan(int idThongTinCaNhan);
    }
    public partial class ThongTinGiaDinhService
    {
        public IQueryable<ThongTinGiaDinh> GetByIdThongTinCaNhan(int idThongTinCaNhan)
        {
            return this.GetActive(q => q.IdThongTinCaNhan == idThongTinCaNhan && q.Active);
        }
    }
}