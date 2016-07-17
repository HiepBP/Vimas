using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Helpers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    [Authorize]
    public class ThongTinGiaDinhController : BaseController
    {
        // GET: HocVien/ThongTinGiaDinh
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadThongTinGiaDinh(JQueryDataTableParamModel param, int userId)
        {
            var thongTinGiaDinhService = this.Service<IThongTinGiaDinhService>();
            try
            {
                var listQuaTrinhHocTap = thongTinGiaDinhService.GetByIdThongTinCaNhan(userId).ProjectTo<ThongTinGiaDinhViewModel>(this.MapperConfig).ToList();
                {
                    var rs = listQuaTrinhHocTap
                        .Where(q => string.IsNullOrEmpty(param.sSearch)
                            || q.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                        .OrderBy(q => q.HoTen)
                        .Skip(param.iDisplayStart)
                        .Take(param.iDisplayLength)
                        .Select(q => new IConvertible[]
                        {
                            q.HoTen,
                            EnumHelper<Relation>.GetDisplayValue((Relation)q.QuanHe.Value),
                            q.SoDienThoai,
                            q.DiaChi,
                            q.Id,
                        });
                    var totalRecords = listQuaTrinhHocTap.Count();
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
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Create(int idThongTinCaNhan)
        {
            var model = new ThongTinGiaDinhEditViewModel();
            model.Relation = (Relation)(model.QuanHe != null ? model.QuanHe : 0);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<JsonResult> Create(ThongTinGiaDinhEditViewModel model)
        {
            try
            {
                var thongTinGiaDinhService = this.Service<IThongTinGiaDinhService>();
                model.QuanHe = (int)model.Relation;
                model.Active = true;

                var entity = model.ToEntity();
                await thongTinGiaDinhService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);

                return Json(new { success = true, message = "Tạo thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var thongTinGiaDinhService = this.Service<IThongTinGiaDinhService>();
            var model = new ThongTinGiaDinhEditViewModel(await thongTinGiaDinhService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
            model.Relation = (Relation)model.QuanHe;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(ThongTinGiaDinhEditViewModel model)
        {
            try
            {
                var thongTinGiaDinhService = this.Service<IThongTinGiaDinhService>();
                var entity = await thongTinGiaDinhService.GetAsync(model.Id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                model.CopyToEntity(entity);
                entity.Active = true;
                entity.QuanHe = (int)model.Relation;
                entity.IdThongTinCaNhan = model.IdThongTinCaNhan;

                await thongTinGiaDinhService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);

                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var thongTinGiaDinhService = this.Service<IThongTinGiaDinhService>();
                var entity = await thongTinGiaDinhService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                entity.Active = false;
                await thongTinGiaDinhService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);

                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        
        public async System.Threading.Tasks.Task<ActionResult> Detail(int id)
        {
            var thongTinGiaDinhService = this.Service<IThongTinGiaDinhService>();
            var model = new ThongTinGiaDinhEditViewModel(await thongTinGiaDinhService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
            model.Relation = (Relation)model.QuanHe;
            return View(model);
        }
    }
}