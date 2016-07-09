using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HeThong.Controllers
{
    public class CongTyChungNgheController : BaseController
    {
        // GET: HeThong/CongTyChungNghe
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDanhSachCongTyChungNghe(JQueryDataTableParamModel param)
        {
            var service = this.Service<ICongTyChungNgheService>();
            var model = service.GetActive().ProjectTo<CongTyChungNgheViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = model
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenTiengViet.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.TenVietTat)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.TenVietTat,
                        q.TenTiengViet,
                        q.TenTiengAnh,
                        q.DiaChiTiengViet,
                        q.NguoiDaiDien,
                        q.DienThoai,
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

        #region Detail
        public ActionResult Detail(int id)
        {
            var service = this.Service<ICongTyChungNgheService>();
            var entity = service.Get(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<CongTyChungNgheViewModel>(entity);

            return this.View(model);
        }
        #endregion

        #region Create
        [Authorize(Roles = "Admin, PhongXKLD")]
        public ActionResult Create()
        {
            var model = new CongTyChungNgheViewModel();
            return this.View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, PhongXKLD")]
        public async Task<ActionResult> Create(CongTyChungNgheViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var service = this.Service<ICongTyChungNgheService>();
            model.Active = true;
            try
            {
                await service.CreateAsync(model.ToEntity());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return this.RedirectToAction("Index");
        }
        #endregion

        #region Edit
        [Authorize(Roles = "Admin, PhongXKLD")]
        public ActionResult Edit(int id)
        {
            var service = this.Service<ICongTyChungNgheService>();

            var entity = service.Get(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CongTyChungNgheViewModel>(entity);

            return this.View(model);
        }

        [Authorize(Roles = "Admin, PhongXKLD")]
        [HttpPost]
        public async Task<JsonResult> Edit(CongTyChungNgheViewModel model)
        {
            var service = this.Service<ICongTyChungNgheService>();

            if (!this.ModelState.IsValid)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng kiểm tra lại thông tin." });
            }

            try
            {
                await service.UpdateAsync(model.ToEntity());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng kiểm thử lại." });
            }

            return Json(new { success = true, message = "Chỉnh sửa thông tin thành công." });
        }
        #endregion

        #region Delete
        [HttpPost]
        [Authorize(Roles = "Admin, PhongXKLD")]
        public async Task<JsonResult> Delete(int id)
        {
            var congTyCNService = this.Service<ICongTyChungNgheService>();

            var entity = congTyCNService.Get(id);

            if (entity == null)
            {
                return Json(new { success = false, message = "Không tồn tại công ty này, xin vui lòng thử lại." });
            }
            else
            {
                try
                {
                    entity.Active = false;
                    await congTyCNService.UpdateAsync(entity);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Xóa công ty thất bại, xin vui lòng thử lại." });
                };
            }
            return Json(new { success = true, message = "Xóa công ty thành công." });
        }
        #endregion
    }
}