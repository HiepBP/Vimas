using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HeThong.Controllers
{
    public class HopDongDOLABController : BaseController
    {
        // GET: HeThong/HopDongDOLAB
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDanhSachDOLAB(JQueryDataTableParamModel param)
        {
            var service = this.Service<IHopDongDOLABService>();
            var model = service.GetActive().ProjectTo<HopDongDOLABViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = model
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.SoDKHopDong.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.NgayDangKy)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.SoDKHopDong,
                        q.SoCongVan,
                        q.SoPhieuTiepNhan,
                        q.NgayDangKy == null ? "": q.NgayDangKy.Value.ToShortDateString(),
                        q.NgayNhan == null ? "" : q.NgayNhan.Value.ToShortDateString(),
                        q.Id,
                    });
                var totalRecords = model.Count();
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
                return Json(new { success = false, message = "Error" });
            }
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            var model = new HopDongDOLABEditViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(HopDongDOLABEditViewModel model)
        {
            var hopDongDOLABService = this.Service<IHopDongDOLABService>();
            var hopDongDOLABHocVienMappingService = this.Service<IHopDongDOLABHocVienMappingService>();
            try
            {
                var entity = model.ToEntity();
                entity.Active = true;
                await hopDongDOLABService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo hợp đồng", controllerName, entity.Id);
                var idHopDongDOLAB = entity.Id;
                try
                {
                    foreach (var item in model.SelectedThongTinCaNhan)
                    {
                        var mapping = new HopDongDOLABHocVienMappingViewModel()
                        {
                            IdHopDongDOLAB = idHopDongDOLAB,
                            IdThongTinCaNhan = item,
                        };
                        await hopDongDOLABHocVienMappingService.CreateAsync(mapping.ToEntity());
                    }
                    return Json(new { success = true, message = "Tạo thành công" });
                }
                catch (Exception e)
                {
                    //await hopDongDOLABService.DeleteAsync(entity);
                    var listMapping = hopDongDOLABHocVienMappingService.GetByIdHopDongDOLAB(idHopDongDOLAB).ToList();
                    foreach (var item in listMapping)
                    {
                        //await hopDongDOLABHocVienMappingService.DeleteAsync(item);
                    }
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        #endregion

        #region Edit
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var hopDongDOLABService = this.Service<IHopDongDOLABService>();
            var model = new HopDongDOLABEditViewModel(await hopDongDOLABService.GetAsync(id));
            if (model == null || !model.Active)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<JsonResult> Edit(HopDongDOLABEditViewModel model)
        {
            var hopDongDOLABService = this.Service<IHopDongDOLABService>();
            try
            {
                var entity = model.ToEntity();
                entity.Active = true;
                await hopDongDOLABService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa hợp đồng", controllerName, entity.Id);
                return Json(new { success = true, message = "Sửa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        public async System.Threading.Tasks.Task<JsonResult> AddHocVien(HopDongDOLABHocVienMappingViewModel model)
        {
            var hopDongDOLABHocVienMappingService = this.Service<IHopDongDOLABHocVienMappingService>();
            var entity = await hopDongDOLABHocVienMappingService.GetByIdHopDongDOLABAndIdTTCNAsync(model.IdHopDongDOLAB.GetValueOrDefault(), model.IdThongTinCaNhan.GetValueOrDefault());
            if (entity == null)
            {
                await hopDongDOLABHocVienMappingService.CreateAsync(model.ToEntity());
                return Json(new { success = true, message = "Thêm thành công!" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (entity.Active == true)
                {
                    return Json(new { success = false, message = "Đã tồn tại học viên này!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    entity.Active = true;
                    await hopDongDOLABHocVienMappingService.UpdateAsync(entity);
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                    var result = await new SystemLogController().Create("Thêm học viên vào HĐ", controllerName, entity.Id);
                    return Json(new { success = true, message = "Thêm thành công!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        public async System.Threading.Tasks.Task<JsonResult> DeleteHocVien(int id)
        {
            var hopDongDOLABHocVienMappingService = this.Service<IHopDongDOLABHocVienMappingService>();
            try
            {
                var entity = await hopDongDOLABHocVienMappingService.GetAsync(id);
                await hopDongDOLABHocVienMappingService.DeactivateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa học viên khỏi HĐ", controllerName, entity.Id);
                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        #endregion

        #region Delete
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id)
        {
            var hopDongDOLABService = this.Service<IHopDongDOLABService>();
            var hopDongDOLABHocVienMappingService = this.Service<IHopDongDOLABHocVienMappingService>();
            try
            {
                var dolabEntity = await hopDongDOLABService.GetAsync(id);
                if (dolabEntity == null && !dolabEntity.Active)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                await hopDongDOLABService.DeactivateAsync(dolabEntity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa hợp đồng", controllerName, dolabEntity.Id);
                var listMapping = hopDongDOLABHocVienMappingService.GetByIdHopDongDOLAB(id).ToList();
                foreach (var item in listMapping)
                {
                    await hopDongDOLABHocVienMappingService.DeactivateAsync(item);

                }
                return Json(new { success = true, message = "Xóa thành công" });
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
            var hopDongDOLABService = this.Service<IHopDongDOLABService>();
            try
            {
                var model = new HopDongDOLABViewModel(hopDongDOLABService.Get(id));
                if(model == null || !model.Active)
                {
                    return Json(new { success = false, message = "Object không tồn tại" });
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = true, message = Resource.ErrorMessage });
            }
        }
        #endregion
    }
}