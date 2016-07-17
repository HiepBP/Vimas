using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    public class ThongTinVeNuocController : BaseController
    {
        // GET: HocVien/ThongTinVeNuoc
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadThongTinVeNuoc(JQueryDataTableParamModel param, int userId)
        {
            var thongTinVeNuocService = this.Service<IThongTinVeNuocService>();

            try
            {
                var listThongTinVeNuoc = thongTinVeNuocService.GetByIdThongTinCaNhan(userId).ToList();
                {
                    var rs = listThongTinVeNuoc
                        .Where(q => string.IsNullOrEmpty(param.sSearch)
                            || (q.NgayVe.HasValue ? q.NgayVe.Value.ToShortDateString() : "").ToLower().Contains(param.sSearch.ToLower()))
                        .OrderBy(q => q.NgayVe)
                        .Skip(param.iDisplayStart)
                        .Take(param.iDisplayLength)
                        .Select(q => new IConvertible[]
                        {
                            q.NgayDi != null ? ((DateTime)q.NgayDi).ToShortDateString() : null,
                            q.NgayVe != null ? ((DateTime)q.NgayVe).ToShortDateString() : null,
                            q.LyDoVe,
                            (q.ThanhLyHopDong != null && q.ThanhLyHopDong == true) ? "Đã xong": "Vẫn chưa",
                            q.NgayThanhLy != null ? ((DateTime)q.NgayThanhLy).ToShortDateString() : null,
                            q.SoHopDongThanhLy,

                            q.Id,
                        });

                    var totalRecords = rs.Count();
                    return Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = totalRecords,
                        iTotalDisplayRecords = totalRecords,
                        aaData = rs
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error" });
            }
        }

        public async Task<ActionResult> Detail(int id)
        {
            var thongTinVeNuocService = this.Service<IThongTinVeNuocService>();
            var model = new ThongTinVeNuocEditViewModel(await thongTinVeNuocService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
            model.ThanhLy = (ThanhLyHopDong)(model.ThanhLyHopDong == true ? 1 : 0);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon, PhongXKLD")]
        public async Task<JsonResult> Del(int id)
        {
            try
            {
                var thongTinVeNuocService = this.Service<IThongTinVeNuocService>();
                var entity = await thongTinVeNuocService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                entity.Active = false;
                await thongTinVeNuocService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);

                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin, PhongNguon, PhongXKLD")]
        public ActionResult Create(int idThongTinCaNhan)
        {
            var model = new ThongTinVeNuocEditViewModel();
            model.ThanhLy = (ThanhLyHopDong)(model.ThanhLyHopDong == true ? 1 : 0);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon, PhongXKLD")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(ThongTinVeNuocEditViewModel model)
        {
            try
            {
                var thongTinVeNuocService = this.Service<IThongTinVeNuocService>();
                model.ThanhLyHopDong = (int)model.ThanhLy == 1 ? true : false;
                model.Active = true;

                var entity = model.ToEntity();
                await thongTinVeNuocService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);

                return Json(new { success = true, message = "Tạo thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin, PhongNguon, PhongXKLD")]
        public async Task<ActionResult> Edit(int id)
        {
            var thongTinVeNuocService = this.Service<IThongTinVeNuocService>();
            var model = new ThongTinVeNuocEditViewModel(await thongTinVeNuocService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
            model.ThanhLy = (ThanhLyHopDong)(model.ThanhLyHopDong == true ? 1 : 0);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon, PhongXKLD")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThongTinVeNuocEditViewModel model)
        {
            try
            {
                var thongTinVeNuocService = this.Service<IThongTinVeNuocService>();
                var entity = await thongTinVeNuocService.GetAsync(model.Id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                model.CopyToEntity(entity);
                entity.Active = true;
                entity.ThanhLyHopDong = (int)model.ThanhLy == 1 ? true : false;
                entity.IdThongTinCaNhan = model.IdThongTinCaNhan;

                await thongTinVeNuocService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);

                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}