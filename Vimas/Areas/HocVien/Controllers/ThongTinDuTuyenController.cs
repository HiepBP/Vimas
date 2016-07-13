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

namespace Vimas.Areas.HocVien.Controllers
{
    public class ThongTinDuTuyenController : BaseController
    {
        // GET: HocVien/ThongTinDuTuyen
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDanhSachThongTinDuTuyen(JQueryDataTableParamModel param)
        {
            var thongTinDuTuyenService = this.Service<IThongTinDuTuyenService>();
            var listThongTinDuTuyen = thongTinDuTuyenService.GetActive().ToList();
            try
            {
                var rs = listThongTinDuTuyen
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.ThongTinCaNhan.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.NgayDangKy)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.ThongTinCaNhan.HoTen,
                        q.LyDoDiNhat,
                        q.DaDKDiNhatOCongtyKhac == true ? "Rồi":"Chưa",
                        q.DaDiNhat == true ? "Rồi":"Chưa",
                        q.DaDiNuocNgoai == true ? "Rồi":"Chưa",
                        q.NgayDangKy.ToShortDateString(),
                        q.Id,
                    });
                var totalRecords = listThongTinDuTuyen.Count();
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
    }
}