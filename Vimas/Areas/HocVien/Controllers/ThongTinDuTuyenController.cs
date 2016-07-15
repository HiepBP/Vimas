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

        #region Create
        [Authorize(Roles ="Admin, PhongNguon")]
        public ActionResult Create()
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new ThongTinDuTuyenEditViewModel();
            model.AvailableThongTinCaNhan = thongTinCaNhanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.HoTen + "[CMND: " + q.CMND + "]",
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(ThongTinDuTuyenEditViewModel model)
        {
            try
            {
                var thongTinDuTuyenService = this.Service<IThongTinDuTuyenService>();
                model.Active = true;
                await thongTinDuTuyenService.CreateAsync(model.ToEntity());
                return Json(new { success = true, message = "Tạo thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        #endregion

        #region Edit
        [Authorize(Roles = "Admin, PhongNguon")]
        public async Task<ActionResult> Edit(int id)
        {
            var thongTinDuTuyenService = this.Service<IThongTinDuTuyenService>();
            var model = new ThongTinDuTuyenEditViewModel(await thongTinDuTuyenService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }

            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var thongTinCaNhan = await thongTinCaNhanService.GetAsync(model.IdThongTinCaNhan);
            model.HoTen = thongTinCaNhan.HoTen;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThongTinDuTuyenEditViewModel model)
        {
            try
            {
                var thongTinDuTuyenService = this.Service<IThongTinDuTuyenService>();
                var entity = await thongTinDuTuyenService.GetAsync(model.Id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                model.CopyToEntity(entity);
                entity.Active = true;
                entity.IdThongTinCaNhan = model.IdThongTinCaNhan;
                await thongTinDuTuyenService.UpdateAsync(entity);
                return Json(new { success = true, message = "Sửa thành công!" });
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
            var model = Mapper.Map<ThongTinDuTuyenEditViewModel>(entity);
            var TTCNService = this.Service<IThongTinCaNhanService>();
            var TTCN = TTCNService.Get(model.IdThongTinCaNhan);
            model.HoTen = TTCN.HoTen;
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