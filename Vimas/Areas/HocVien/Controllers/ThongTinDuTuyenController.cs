using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    public class ThongTinDuTuyenController : BaseController
    {
        // GET: HocVien/ThongTinDuTuyen

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDanhSachThongTinDuTuyen(JQueryDataTableParamModel param)
        {
            var thongTinDuTuyenService = this.Service<IThongTinDuTuyenService>();
            var listThongTinDuTuyen = thongTinDuTuyenService.GetActive().ToList();
            try
            {
                var rs = listThongTinDuTuyen
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.ThongTinCaNhan.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.NgayDangKy)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.ThongTinCaNhan.HoTen,
                        q.LyDoDiNhat,
                        q.DaDKDiNhatOCongtyKhac == true ? "Rồi":"Chưa",
                        q.DaDiNhat == true ? "Rồi":"Chưa",
                        q.DaDiNuocNgoai == true ? "Rồi":"Chưa",
                        q.NgayDangKy.ToShortDateString(),
                        q.Id,
                    });
                var totalRecords = listThongTinDuTuyen.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = rs
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        #endregion

        #region Detail
        public ActionResult Detail(int id)
        {
            var service = this.Service<IThongTinDuTuyenService>();
            var entity = service.Get(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<ThongTinDuTuyenViewModel>(entity);

            return this.View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        public async Task<JsonResult> Delete(int id)
        {
            var thongTinDuTuyenService = this.Service<IThongTinDuTuyenService>();

            var entity = thongTinDuTuyenService.Get(id);

            if (entity == null)
            {
                return Json(new { success = false, message = "Không tồn tại dữ liệu thông tin dự tuyển này, xin vui lòng thử lại." });
            }
            else
            {
                try
                {
                    entity.Active = false;
                    await thongTinDuTuyenService.UpdateAsync(entity);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Xóa thông tin dự tuyển thất bại, xin vui lòng thử lại." });
                };
            }
            return Json(new { success = true, message = "Xóa thông tin dự tuyển." });
        }
        #endregion
    }
}