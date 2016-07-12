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
    public class ThongTinNopTienController : BaseController
    {

        //[Authorize(Roles = "Admin, PhongKeToan")]
        // GET: HocVien/ThongTinNopTien
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin, PhongKeToan")]
        public JsonResult LoadDanhSachThongTinNopTien(JQueryDataTableParamModel param)
        {
            var thongTinNopTienService = this.Service<IThongTinNopTienService>();
            var listThongTinNopTien = thongTinNopTienService.GetActive().ProjectTo<ThongTinNopTienViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = listThongTinNopTien
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.ThongTinCaNhan.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.NgayLapPhieu)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.SoPhieu,
                        q.ThongTinCaNhan.HoTen,
                        EnumHelper<TypeOfMoney>.GetDisplayValue((TypeOfMoney)q.LoaiTien),
                        q.SoTien,
                        EnumHelper<ThuChi>.GetDisplayValue((ThuChi)q.ThuHayChi),
                        q.NgayLapPhieu.ToShortDateString(),
                        q.Id,
                    });
                var totalRecords = listThongTinNopTien.Count();
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

        [Authorize(Roles = "Admin, PhongKeToan")]
        public ActionResult Create()
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new ThongTinNopTienEditViewModel();
            model.ThuChi = (ThuChi)model.ThuHayChi;
            model.TypeOfMoney = (TypeOfMoney)model.LoaiTien;
            model.AvailableThongTinCaNhan = thongTinCaNhanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.HoTen,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, PhongKeToan")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(ThongTinNopTienEditViewModel model)
        {
            try
            {
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                var thongTinNopTienService = this.Service<IThongTinNopTienService>();
                model.Active = true;
                model.ThuHayChi = (int)model.ThuChi;
                var entity = model.ToEntity();
                await thongTinNopTienService.CreateAsync(entity);
                return Json(new { success = true, message = "Tạo thành công" });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles ="Admin")]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var thongTinNopTienService = this.Service<IThongTinNopTienService>();
            var model = new ThongTinNopTienEditViewModel(await thongTinNopTienService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage});
            }
            model.ThuChi = (ThuChi)model.ThuHayChi;
            model.TypeOfMoney = (TypeOfMoney)model.LoaiTien;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(ThongTinNopTienEditViewModel model)
        {
            try
            {
                var thongTinNopTienService = this.Service<IThongTinNopTienService>();
                var entity = await thongTinNopTienService.GetAsync(model.Id);
                
                entity.LoaiTien = model.LoaiTien;
                entity.SoTien = model.SoTien;
                entity.NgayLapPhieu = model.NgayLapPhieu;
                entity.LyDo = model.LyDo;
                entity.ThuHayChi = (int)model.ThuChi;

                await thongTinNopTienService.UpdateAsync(entity);
                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var thongTinNopTienService = this.Service<IThongTinNopTienService>();
                var entity = await thongTinNopTienService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                entity.Active = false;
                await thongTinNopTienService.UpdateAsync(entity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}