﻿using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    public class BienBanController : BaseController
    {
        // GET: HocVien/BienBan
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDanhSachBienBan(JQueryDataTableParamModel param)
        {
            var bienBanService = this.Service<IBienBanService>();
            var listBienBan = bienBanService.GetActive().ToList();
            try
            {
                var rs = listBienBan
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.ThongTinCaNhan.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderByDescending(q => q.id)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.ThongTinCaNhan.HoTen,
                        q.GhiChu,
                        q.HinhAnh,
                        q.id,
                    });
                var totalRecords = listBienBan.Count();
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

        public ActionResult Create()
        {
            var bienBanService = this.Service<IBienBanService>();
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new BienBanEditViewModel();
            model.AvailableThongTinCaNhan = thongTinCaNhanService.GetActive()
                .AsEnumerable()
                .Select(q => new SelectListItem()
                {
                    Text = q.HoTen + " - " + q.NgaySinh.ToShortDateString(),
                    Value = q.Id.ToString(),
                    Selected = false,
                });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<JsonResult> Create(BienBanEditViewModel model)
        {
            var bienBanService = this.Service<IBienBanService>();
            try
            {
                #region Get Hinh Anh
                string hinhAnhPath = "";
                string root = Server.MapPath("~");
                string parent = Path.GetDirectoryName(root);
                string grandParent = Path.GetDirectoryName(parent);
                string serverPath = grandParent + "/BienBan/";
                List<string> imgExtension = new List<string>() { ".jpg", ".jpeg", ".png" };
                if (model.SelectedHinhAnh != null)
                {
                    string brandLogoFileExtension = Path.GetExtension(model.SelectedHinhAnh.FileName);
                    if (!imgExtension.Contains(brandLogoFileExtension.ToLower()))
                    {
                        return Json(new { success = false, message = Resource.InvalidImageFile });
                    }
                    string hinhAnhFileName = "BienBan_" + Guid.NewGuid().ToString("N") + brandLogoFileExtension;
                    //model.HinhAnhLogo.SaveAs(serverPath + hinhAnhFileName);
                    //temp
                    model.SelectedHinhAnh.SaveAs(HttpContext.Server.MapPath("~/BienBan/") + hinhAnhFileName);
                    hinhAnhPath = "/BienBan/" + hinhAnhFileName;
                }
                #endregion
                model.HinhAnh = hinhAnhPath;
                model.Active = true;
                var entity = model.ToEntity();
                await bienBanService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.id);
                return Json(new { success = true, message = "Tạo biên bản thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var bienBanService = this.Service<IBienBanService>();
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new BienBanEditViewModel(await bienBanService.GetAsync(id));
            model.AvailableThongTinCaNhan = thongTinCaNhanService.GetActive()
                .AsEnumerable()
                .Select(q => new SelectListItem()
                {
                    Text = q.HoTen + " - " + q.NgaySinh.ToShortDateString(),
                    Value = q.Id.ToString(),
                    Selected = model.id == q.Id,
                });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<JsonResult> Edit(BienBanEditViewModel model)
        {
            var bienBanService = this.Service<IBienBanService>();
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var entity = await bienBanService.GetAsync(model.id);
            try
            {
                #region Get Hinh Anh
                string hinhAnhPath = "";
                string root = Server.MapPath("~");
                string parent = Path.GetDirectoryName(root);
                string grandParent = Path.GetDirectoryName(parent);
                string serverPath = grandParent + "/BienBan/";
                List<string> imgExtension = new List<string>() { ".jpg", ".jpeg", ".png" };
                if (model.SelectedHinhAnh != null)
                {
                    string brandLogoFileExtension = Path.GetExtension(model.SelectedHinhAnh.FileName);
                    if (!imgExtension.Contains(brandLogoFileExtension.ToLower()))
                    {
                        return Json(new { success = false, message = Resource.InvalidImageFile });
                    }
                    string hinhAnhFileName = "BienBan_" + Guid.NewGuid().ToString("N") + brandLogoFileExtension;
                    //model.HinhAnhLogo.SaveAs(serverPath + hinhAnhFileName);
                    //temp
                    model.SelectedHinhAnh.SaveAs(HttpContext.Server.MapPath("~/BienBan/") + hinhAnhFileName);
                    hinhAnhPath = "/BienBan/" + hinhAnhFileName;
                    #region Delete old file
                    string strPhysicalFolder = Server.MapPath("~/");

                    string strFileFullPath = strPhysicalFolder + entity.HinhAnh;

                    if (System.IO.File.Exists(strFileFullPath))
                    {
                        System.IO.File.Delete(strFileFullPath);
                    }
                    #endregion
                }
                else
                {
                    hinhAnhPath = entity.HinhAnh;
                }
                model.HinhAnh = hinhAnhPath;
                #endregion
                model.CopyToEntity(entity);
                entity.Active = true;
                entity.ThongTinCaNhan = await thongTinCaNhanService.GetAsync(entity.idThongTinCaNhan);
                await bienBanService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.id);
                return Json(new { success = true, message = "Lưu biên bản thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var bienBanService = this.Service<IBienBanService>();
                var entity = await bienBanService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                #region Delete old file
                string strPhysicalFolder = Server.MapPath("~/");

                string strFileFullPath = strPhysicalFolder + entity.HinhAnh;

                if (System.IO.File.Exists(strFileFullPath))
                {
                    System.IO.File.Delete(strFileFullPath);
                }
                #endregion
                await bienBanService.DeactivateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.id);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> Detail(int id)
        {
            var bienBanService = this.Service<IBienBanService>();
            var model = new BienBanEditViewModel(await bienBanService.GetAsync(id));
            if (model == null || !model.Active)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}