using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    [Authorize]
    public class ThongTinCaNhanController : BaseController
    {
        // GET: HocVien/ThongTinCaNhan
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDanhSachThongTin(JQueryDataTableParamModel param)
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var listThongTinCaNhan = thongTinCaNhanService.GetActive().ProjectTo<ThongTinCaNhanViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = listThongTinCaNhan
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.HoTen)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.HoTen,
                        q.GioiTinh == 0 ? "Nữ":"Nam",
                        q.NgaySinh.ToShortDateString(),
                        q.CMND,
                        q.DienThoaiDiDong,
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
        public ActionResult Create()
        {
            var trungTamGTVLService = this.Service<ITrungTamGTVLService>();
            var model = new ThongTinCaNhanEditViewModel();
            model.Gender = (Gender)1;
            model.FamilyStatus = (FamilyStatus)model.TinhTrangGiaDinh;
            model.EducationLevel = (EducationLevel)(model.TrinhDoVanHoa != null ? model.TrinhDoVanHoa : 0);
            model.AvailableMaNguon = trungTamGTVLService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenCoSo,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ThongTinCaNhanEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                model.Active = true;
                model.GioiTinh = (int)model.Gender;
                model.TrinhDoVanHoa = (int)model.EducationLevel;
                model.TinhTrangGiaDinh = (int)model.FamilyStatus;
                await thongTinCaNhanService.CreateAsync(model.ToEntity());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        public async Task<ActionResult> Edit(int id)
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var trungTamGTVLService = this.Service<ITrungTamGTVLService>();
            var model = new ThongTinCaNhanEditViewModel(await thongTinCaNhanService.GetAsync(id));
            model.Gender = (Gender)model.GioiTinh;
            model.FamilyStatus = (FamilyStatus)model.TinhTrangGiaDinh;
            model.EducationLevel = (EducationLevel)(model.TrinhDoVanHoa != null ? model.TrinhDoVanHoa : 0);
            model.AvailableMaNguon = trungTamGTVLService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenCoSo,
                Value = q.Id.ToString(),
                Selected = (q.Id == model.IdTrungTamGTVL),
            });
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThongTinCaNhanEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var entity = await thongTinCaNhanService.GetAsync(model.Id);
            model.CopyToEntity(entity);
            entity.Active = true;
            entity.GioiTinh = (int)model.Gender;
            entity.TrinhDoVanHoa = (int)model.EducationLevel;
            entity.TinhTrangGiaDinh = (int)model.FamilyStatus;
            await thongTinCaNhanService.UpdateAsync(entity);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Detail(int id)
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new ThongTinCaNhanViewModel(await thongTinCaNhanService.GetAsync(id));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                var entity = await thongTinCaNhanService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                entity.Active = false;
                await thongTinCaNhanService.UpdateAsync(entity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}