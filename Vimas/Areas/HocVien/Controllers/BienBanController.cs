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
                await bienBanService.CreateAsync(model.ToEntity());
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
                }
                else
                {
                    hinhAnhPath = entity.HinhAnh;
                }
                model.HinhAnh = hinhAnhPath;
                #endregion
                model.CopyToEntity(entity);
                entity.Active = true;
                await bienBanService.UpdateAsync(entity);
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
                await bienBanService.DeactivateAsync(entity);
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