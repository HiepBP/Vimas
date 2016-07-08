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

namespace Vimas.Areas.HocVien.Controllers
{
    public class SucKhoeController : BaseController
    {
        // GET: HocVien/SucKhoe
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDanhSachSucKhoe(JQueryDataTableParamModel param)
        {
            var sucKhoeService = this.Service<ISucKhoeService>();
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();

            var list = sucKhoeService.Get()
                .Where(q => q.Active == true)
                .ProjectTo<SucKhoeEditViewModel>(this.MapperConfig).ToList();

            foreach (var item in list)
            {
                item.Name = thongTinCaNhanService.Get(item.IdThongTinCaNhan).HoTen;
            }

            try
            {
                var count = 0;
                var rs = list
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.Name.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.Name)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        ++count,
                        q.IdThongTinCaNhan,
                        q.Name,
                        q.NhomMau,
                        q.Id,
                    });
                var totalRecords = rs.Count();
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
            var model = new SucKhoeEditViewModel();
            PrepareEdit(model);
            return this.View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(SucKhoeEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var service = this.Service<ISucKhoeService>();
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
        public ActionResult Edit(int id)
        {
            var service = this.Service<ISucKhoeService>();
            var entity = service.Get(id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            var model = Mapper.Map<SucKhoeEditViewModel>(entity);
            PrepareEdit(model);
            return this.View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(SucKhoeEditViewModel model)
        {
            var service = this.Service<ISucKhoeService>();

            if (!this.ModelState.IsValid)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra, vui lòng kiểm tra lại." });
            }

            try
            {
                await service.UpdateAsync(model.ToEntity());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, message = "Có lỗi xảy ra, vui lòng thử lại." });
            }
            return Json(new { success = true, message = "Cập nhật thành công." });

        } 
        #endregion

        public ActionResult Detail(int id)
        {
            var sucKhoeService = this.Service<ISucKhoeService>();
            var TTCNService = this.Service<IThongTinCaNhanService>();

            var entity = sucKhoeService.Get(id);
            if(entity == null)
            {
                return this.HttpNotFound();
            }
            var model = Mapper.Map<SucKhoeEditViewModel>(entity);

            model.Name = TTCNService.Get(model.IdThongTinCaNhan).HoTen;

            return this.View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            var service = this.Service<ISucKhoeService>();

            var entity = service.Get(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Không tồn tại báo cáo này, vui lòng thử lại." });
            }

            entity.Active = false;
            try
            {
                await service.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng thử lại." });
            }

            return Json(new { success = true, message = "Xóa báo cáo thành công" });
        }

        public void PrepareEdit(SucKhoeEditViewModel model)
        {
            //thong tin ca nhan service
            var TTCNSerice = this.Service<IThongTinCaNhanService>();

            model.AvailableIDs = TTCNSerice.Get()
                .Where(q => q.Active == true)
                .Select(q => new SelectListItem()
            {
                Selected = false,
                Text = q.HoTen,
                Value = q.Id.ToString(),
            });
        }
    }
}