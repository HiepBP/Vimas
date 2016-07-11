using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Vimas.Models.Entities.Services
{
    public partial interface IHopDongDOLABHocVienMappingService
    {
        IQueryable<HopDongDOLABHocVienMapping> GetByIdHopDongDOLAB(int idHopDongDOLAB);
        Task<HopDongDOLABHocVienMapping> GetByIdHopDongDOLABAndIdTTCNAsync(int idHopDongDOLAB, int idThongTinCaNhan);
    }
    public partial class HopDongDOLABHocVienMappingService
    {
        public IQueryable<HopDongDOLABHocVienMapping> GetByIdHopDongDOLAB(int idHopDongDOLAB)
        {
            return this.Get(q => q.IdHopDongDOLAB == idHopDongDOLAB && q.Active);
        }

        public async Task<HopDongDOLABHocVienMapping> GetByIdHopDongDOLABAndIdTTCNAsync(int idHopDongDOLAB, int idThongTinCaNhan)
        {
            return await this.FirstOrDefaultAsync(q => q.IdHopDongDOLAB == idHopDongDOLAB && q.IdThongTinCaNhan == idThongTinCaNhan);
        }
    }
}