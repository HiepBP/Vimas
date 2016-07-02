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
                        q.GioiTinh,
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

        public ActionResult Create()
        {
            var trungTamGTVLService = this.Service<ITrungTamGTVLService>();
            var model = new ThongTinCaNhanEditViewModel();
            model.Gender = (Gender)model.GioiTinh;
            model.FamilyStatus = (FamilyStatus)model.TinhTrangGiaDinh;
            model.AvailableMaNguon = trungTamGTVLService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenCoSo,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ThongTinCaNhanViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            model.Active = true;
            await thongTinCaNhanService.CreateAsync(model.ToEntity());
            return RedirectToAction("Index");
        }
    }
}