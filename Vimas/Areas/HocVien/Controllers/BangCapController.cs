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

namespace Vimas.Areas.HocVien.Controllers
{
    [Authorize]
    public class BangCapController : BaseController
    {
        // GET: HocVien/BangCap
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDanhSachBangCap(JQueryDataTableParamModel param)
        {
            var bangCapService = this.Service<IBangCapService>();
            var listBangCap = bangCapService.GetActive().ToList();
            try
            {
                var rs = listBangCap
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.BangCap1.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderByDescending(q => q.Id)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.BangCap1,
                        q.Thang + "/" q.Nam,
                        q.TrinhDo,
                        q.DaNop.GetValueOrDefault()?"Rồi":"Chưa",
                        q.Id,
                    });
                var totalRecords = listBangCap.Count();
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

        #region Create
        public ActionResult Create(int idThongTinDuTuyen)
        {
            var model = new BangCapEditViewModel();
            model.IdThongTinDuTuyen = idThongTinDuTuyen;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<JsonResult> Create(BangCapViewModel model)
        {
            var bangCapService = this.Service<IBangCapService>();
            try
            {
                var entity = model.ToEntity();
                entity.Active = true;
                await bangCapService.CreateAsync(entity);
                return Json(new { success = true, message = "Tạo thành công" });
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
            var bangCapService = this.Service<IBangCapService>();
            var model = new BangCapEditViewModel(await bangCapService.GetAsync(id));
            if(model == null || !model.Active)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public async System.Threading.Tasks.Task<JsonResult> Edit(BangCapEditViewModel model)
        {
            try
            {
                var bangCapService = this.Service<IBangCapService>();
                var entity = await bangCapService.GetAsync(model.Id);

                entity.Thang = model.Thang;
                entity.Nam = model.Nam;
                entity.BangCap1 = model.BangCap1;
                entity.DaNop = model.DaNop;
                entity.TrinhDo = model.TrinhDo;

                await bangCapService.UpdateAsync(entity);
                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var bangCapService = this.Service<IBangCapService>();
                var entity = await bangCapService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                entity.Active = false;
                await bangCapService.UpdateAsync(entity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        #endregion
    }
}