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
    public class QuaTrinhHocTapController : BaseController
    {
        // GET: HocVien/QuaTrinhHocTap
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int idThongTinCaNhan)
        {
            var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
            var model = new QuaTrinhHocTapEditViewModel()
            {
                IdThongTinCaNhan = idThongTinCaNhan,
                EducationLevel = 0,
            };
            return View(model);
        }

        public JsonResult LoadQuaTrinhHocTap(JQueryDataTableParamModel param)
        {
            var quaTrinhHocTapService = this.Service<IQuaTrinhHocTapService>();
            var listQuaTrinhHocTap = quaTrinhHocTapService.GetActive().ProjectTo<QuaTrinhHocTapViewModel>(this.MapperConfig).ToList();
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
                        q.LoaiTruong,
                        q.NganhHoc,
                        q.DaTotNghiep,
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
    }
}