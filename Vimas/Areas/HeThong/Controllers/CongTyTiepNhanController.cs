using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HeThong.Controllers
{
    public class CongTyTiepNhanController : BaseController
    {
        // GET: HeThong/CongTyTiepNhan
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCongTyTiepNhan(JQueryDataTableParamModel param)
        {
            var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
            var listCongTyTiepNhan = congTyTiepNhanService.GetActive().ToList();
            try
            {
                var rs = listCongTyTiepNhan
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenTiengViet.ToLower().Contains(param.sSearch.ToLower())
                        || q.TenTiengAnh.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.TenTiengViet)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.TenTiengViet,
                        q.TenTiengAnh,
                        q.NganhNghe,
                        q.NguoiDaiDien,
                        q.DiaChi,
                        q.DienThoai,
                        q.Fax,
                        q.idNghiepDoan.HasValue ? q.NghiepDoan.TenNghiepDoan : "",
                        q.Id,
                    });
                var totalRecords = listCongTyTiepNhan.Count();
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
            var nghiepDoanService = this.Service<INghiepDoanService>();
            var model = new CongTyTiepNhanEditViewModel();
            model.AvailableNghiepDoan = nghiepDoanService.GetActive().Select(q => new SelectListItem() {
                Text = q.TenNghiepDoan,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(CongTyTiepNhanEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
                model.Active = true;
                await congTyTiepNhanService.CreateAsync(model.ToEntity());
                return Json(new { success = true, message = Resource.ErrorMessage });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
            var nghiepDoanService = this.Service<INghiepDoanService>();
            var model = new CongTyTiepNhanEditViewModel(await congTyTiepNhanService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
            model.AvailableNghiepDoan = nghiepDoanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenNghiepDoan,
                Value = q.Id.ToString(),
                Selected = q.Id == model.idNghiepDoan,
            });
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(CongTyTiepNhanEditViewModel model)
        {
            try
            {
                var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
                var entity = await congTyTiepNhanService.GetAsync(model.Id);
                model.CopyToEntity(entity);
                entity.Active = true;
                await congTyTiepNhanService.UpdateAsync(entity);
                return Json(new { success = true, message = "Sửa thành công!" });
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
                var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
                var entity = await congTyTiepNhanService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                await congTyTiepNhanService.DeactivateAsync(entity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}