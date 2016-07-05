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
    public class ThongTinGiaDinhController : BaseController
    {
        // GET: HocVien/ThongTinGiaDinh
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadThongTinGiaDinh(JQueryDataTableParamModel param, int userId)
        {
            var thongTinGiaDinhService = this.Service<IThongTinGiaDinhService>();
            try
            {
                var listQuaTrinhHocTap = thongTinGiaDinhService.GetByIdThongTinCaNhan(userId).ProjectTo<ThongTinGiaDinhViewModel>();
                {
                    var rs = listQuaTrinhHocTap
                        .Where(q => string.IsNullOrEmpty(param.sSearch)
                            || q.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                        .OrderBy(q => q.HoTen)
                        .Skip(param.iDisplayStart)
                        .Take(param.iDisplayLength)
                        .Select(q => new IConvertible[]
                        {
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
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error" });
            }
        }
    }
}