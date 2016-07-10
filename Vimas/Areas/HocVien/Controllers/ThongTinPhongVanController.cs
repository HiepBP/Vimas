using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
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
                var listThongTinPhongVan = thongTinPhongVanService.GetByIdThongTinCaNhan(userId).ToList();
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
            model.ThoiHanHopDongEnum = (ThoiHanHopDong)(model.ThoiHanHopDong.HasValue ? model.ThoiHanHopDong.Value : 0);
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
                model.ThoiHanHopDong = (int)model.ThoiHanHopDongEnum;
                await thongTinPhongVanService.CreateAsync(model.ToEntity());
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
            if(model == null || !model.Active)
            {
                return HttpNotFound();
            }
            model.ThoiHanHopDongEnum = (ThoiHanHopDong)(model.ThoiHanHopDong.HasValue ? model.ThoiHanHopDong.Value : 0);
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
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            var thongTinPhongVanService = this.Service<IThongTinPhongVanService>();
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
            await thongTinPhongVanService.UpdateAsync(entity);
            return RedirectToAction("Detail","ThongTinCaNhan", new { id = model.IdThongTinCaNhan });
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
                await thongTinPhongVanService.DeactivateAsync(entity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}