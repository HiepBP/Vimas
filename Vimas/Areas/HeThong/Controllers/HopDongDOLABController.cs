using AutoMapper.QueryableExtensions;
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
    public class HopDongDOLABController : BaseController
    {
        // GET: HeThong/HopDongDOLAB
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDanhSachDOLAB(JQueryDataTableParamModel param)
        {
            var service = this.Service<IHopDongDOLABService>();
            var model = service.GetActive().ProjectTo<HopDongDOLABViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = model
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.SoDKHopDong.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.NgayDangKy)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.SoDKHopDong,
                        q.SoCongVan,
                        q.SoPhieuTiepNhan,
                        q.NgayDangKy == null ? "": q.NgayDangKy.Value.ToShortDateString(),
                        q.NgayNhan == null ? "" : q.NgayNhan.Value.ToShortDateString(),
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

        public ActionResult Create()
        {
            var model = new HopDongDOLABViewModel();
            return View(model);
        }
    }
}