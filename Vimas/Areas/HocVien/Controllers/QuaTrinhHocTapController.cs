using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Helpers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    [Authorize]
    public class QuaTrinhHocTapController : BaseController
    {
        // GET: HocVien/QuaTrinhHocTap
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadQuaTrinhHocTap(JQueryDataTableParamModel param, int userId)
        {
            var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
            var listQuaTrinhHocTap = quaTrinhHocTapService.GetByIdThongTinCaNhan(userId).ProjectTo<QuaTrinhHocTapViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = listQuaTrinhHocTap
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenTruong.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.LoaiTruong)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.TenTruong,
                        EnumHelper<EducationLevel>.GetDisplayValue((EducationLevel)q.LoaiTruong.Value),
                        q.NganhHoc,
                        q.DaTotNghiep == true ? "Rồi":"Chưa",
                        q.TuNam.HasValue ? q.TuNam : 0,
                        q.DenNam.HasValue ? q.DenNam : 0,
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

        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Create(int idThongTinCaNhan)
        {
            var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
            var model = new QuaTrinhHocTapEditViewModel()
            {
                IdThongTinCaNhan = idThongTinCaNhan,
                EducationLevel = (EducationLevel)0,
                DaTotNghiep = false,
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(QuaTrinhHocTapEditViewModel model)
        {
            try
            {
                var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
                model.Active = true;
                model.LoaiTruong = (int)model.EducationLevel;
                if (!model.DaTotNghiep.HasValue)
                {
                    model.DaTotNghiep = false;
                }
                await quaTrinhHocTapService.CreateAsync(model.ToEntity());
                return Json(new { success = true, message = "Tạo thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
            var model = new QuaTrinhHocTapEditViewModel(await quaTrinhHocTapService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, });
            }
            model.EducationLevel = (EducationLevel)model.LoaiTruong;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(QuaTrinhHocTapEditViewModel model)
        {
            try
            {
                var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
                var entity = await quaTrinhHocTapService.GetAsync(model.Id);
                if (!model.DaTotNghiep.HasValue)
                {
                    model.DaTotNghiep = false;
                }
                model.CopyToEntity(entity);
                entity.DaTotNghiep = model.DaTotNghiep;
                entity.Active = true;
                entity.LoaiTruong = (int)model.EducationLevel;
                entity.IdThongTinCaNhan = model.IdThongTinCaNhan;
                await quaTrinhHocTapService.UpdateAsync(entity);
                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
                var entity = await quaTrinhHocTapService.GetAsync(id);
                if(entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                entity.Active = false;
                await quaTrinhHocTapService.UpdateAsync(entity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}