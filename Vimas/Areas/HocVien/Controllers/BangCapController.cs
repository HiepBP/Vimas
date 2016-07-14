using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        #endregion
    }
}