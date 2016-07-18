using SkyWeb.DatVM.Mvc;
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
    [Authorize]
    public class ThongTinPhongVanController : BaseController
    {
        // GET: HocVien/ThongTinPhongVan
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadThongTinPhongVan(JQueryDataTableParamModel param, int userId)
        {
            var thongTinPhongVanService = this.Service<IThongTinPhongVanService>();
            var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
            try
            {
                var user = System.Web.HttpContext.Current.User;
                var listThongTinPhongVan = thongTinPhongVanService.GetByIdThongTinCaNhan(userId).ToList();
                if (user.IsInRole("PhongXKLD"))
                {
                    listThongTinPhongVan = listThongTinPhongVan.Where(q => q.NgayTrungTuyen != null).ToList();
                }
                {
                    var rs = listThongTinPhongVan
                        .Where(q => string.IsNullOrEmpty(param.sSearch)
                            || (q.NgayPhongVan.HasValue ? q.NgayPhongVan.Value.ToShortDateString() : "").ToLower().Contains(param.sSearch.ToLower()))
                        .OrderBy(q => q.NgayPhongVan)
                        .Skip(param.iDisplayStart)
                        .Take(param.iDisplayLength)
                        .Select(q => new IConvertible[]
                        {
                            q.NgayPhongVan.HasValue? q.NgayPhongVan.Value.ToShortDateString() : "",
                            q.NghePhongVan,
                            q.GhiChuPV,
                            q.NgayTrungTuyen.HasValue? q.NgayTrungTuyen.Value.ToShortDateString():"",
                            q.NgheTrungTuyenTiengViet,
                            q.IdCongTyTiepNhan.HasValue?((congTyTiepNhanService.Get(q.IdCongTyTiepNhan)).TenTiengNhat):"",
                            q.Id,
                        });
                    var totalRecords = listThongTinPhongVan.Count();
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

        public ActionResult Create(int idThongTinCaNhan)
        {
            var congTyChungNgheService = this.Service<ICongTyChungNgheService>();
            var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
            var model = new ThongTinPhongVanEditViewModel();
            model.IdThongTinCaNhan = idThongTinCaNhan;

            model.AvailableCongTyChungNghe = congTyChungNgheService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenTiengViet,
                Value = q.Id.ToString(),
                Selected = false,
            });
            model.AvailableCongTyTiepNhan = congTyTiepNhanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenTiengNhat,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(ThongTinPhongVanEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var thongTinPhongVanService = this.Service<IThongTinPhongVanService>();
            try
            {
                #region Get Hinh Anh
                string hinhAnhPath = "";
                string root = Server.MapPath("~");
                string parent = Path.GetDirectoryName(root);
                string grandParent = Path.GetDirectoryName(parent);
                string serverPath = grandParent + "/UploadedImageData/";
                List<string> imgExtension = new List<string>() { ".jpg", ".jpeg", ".png" };
                if (model.HinhAnhLogo != null)
                {
                    string brandLogoFileExtension = Path.GetExtension(model.HinhAnhLogo.FileName);
                    if (!imgExtension.Contains(brandLogoFileExtension.ToLower()))
                    {
                        return Json(new { success = false, message = Resource.InvalidImageFile });
                    }
                    string hinhAnhFileName = "HinhAnh_" + Guid.NewGuid().ToString("N") + brandLogoFileExtension;
                    //model.HinhAnhLogo.SaveAs(serverPath + hinhAnhFileName);
                    //temp
                    model.HinhAnhLogo.SaveAs(HttpContext.Server.MapPath("~/UploadedImageData/") + hinhAnhFileName);
                    hinhAnhPath = "/UploadedImageData/" + hinhAnhFileName;
                }
                #endregion
                model.HinhAnh = hinhAnhPath;
                model.Active = true;
                model.ThoiHanHopDong = model.ThoiHanHopDongEnum.HasValue ? (int?)model.ThoiHanHopDongEnum.Value : null;

                var entity = model.ToEntity();
                await thongTinPhongVanService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);

                return RedirectToAction("Detail", "ThongTinCaNhan", new { id = model.IdThongTinCaNhan });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var thongTinPhongVanService = this.Service<IThongTinPhongVanService>();
            var congTyChungNgheService = this.Service<ICongTyChungNgheService>();
            var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
            var model = new ThongTinPhongVanEditViewModel(await thongTinPhongVanService.GetAsync(id));
            if (model == null || !model.Active)
            {
                return HttpNotFound();
            }
            if (model.ThoiHanHopDong.HasValue)
            {
                model.ThoiHanHopDongEnum = (ThoiHanHopDong)model.ThoiHanHopDong.GetValueOrDefault();
            }
            model.AvailableCongTyChungNghe = congTyChungNgheService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenTiengViet,
                Value = q.Id.ToString(),
                Selected = q.Id == model.IdCongTyChungNghe,
            });
            model.AvailableCongTyTiepNhan = congTyTiepNhanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenTiengNhat,
                Value = q.Id.ToString(),
                Selected = q.Id == model.IdCongTyTiepNhan,
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(ThongTinPhongVanEditViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(model);
                }
                var thongTinPhongVanService = this.Service<IThongTinPhongVanService>();
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                var entity = await thongTinPhongVanService.GetAsync(model.Id);
                #region Get Hinh Anh
                string hinhAnhPath = "";
                string root = Server.MapPath("~");
                string parent = Path.GetDirectoryName(root);
                string grandParent = Path.GetDirectoryName(parent);
                string serverPath = grandParent + "/UploadedImageData/";
                List<string> imgExtension = new List<string>() { ".jpg", ".jpeg", ".png" };
                if (model.HinhAnhLogo != null)
                {
                    string brandLogoFileExtension = Path.GetExtension(model.HinhAnhLogo.FileName);
                    if (!imgExtension.Contains(brandLogoFileExtension.ToLower()))
                    {
                        return Json(new { success = false, message = Resource.InvalidImageFile });
                    }
                    string hinhAnhFileName = "HinhAnh_" + Guid.NewGuid().ToString("N") + brandLogoFileExtension;
                    //model.HinhAnhLogo.SaveAs(serverPath + hinhAnhFileName);
                    //temp
                    model.HinhAnhLogo.SaveAs(HttpContext.Server.MapPath("~/UploadedImageData/") + hinhAnhFileName);
                    hinhAnhPath = "/UploadedImageData/" + hinhAnhFileName;
                    #region Delete old image
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
                #endregion
                model.HinhAnh = hinhAnhPath;
                model.ThoiHanHopDong = (int)model.ThoiHanHopDongEnum;
                model.CopyToEntity(entity);
                entity.Active = true;
                entity.ThongTinCaNhan = await thongTinCaNhanService.GetAsync(entity.IdThongTinCaNhan);
                entity.IdCongTyChungNghe = model.IdCongTyChungNghe;
                entity.IdCongTyTiepNhan = model.IdCongTyTiepNhan;

                await thongTinPhongVanService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);

                return RedirectToAction("Detail", "ThongTinCaNhan", new { id = model.IdThongTinCaNhan });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> Detail(int id)
        {
            var thongTinPhongVanService = this.Service<IThongTinPhongVanService>();
            var model = new ThongTinPhongVanViewModel(await thongTinPhongVanService.GetAsync(id));
            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var thongTinPhongVanService = this.Service<IThongTinPhongVanService>();
                var entity = await thongTinPhongVanService.GetAsync(id);
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
                await thongTinPhongVanService.DeactivateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);

                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}