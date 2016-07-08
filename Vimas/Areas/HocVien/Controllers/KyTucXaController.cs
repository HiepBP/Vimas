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
    public class KyTucXaController : BaseController
    {
        // GET: HocVien/KyTucXa
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadThongTinKyTucXa(JQueryDataTableParamModel param)
        {
            var kyTucXaService = this.Service<IKyTucXaService>();
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var listThongTinCaNhan = thongTinCaNhanService.GetActive().ToList();

            /*var res = (from h in listThongTinCaNhan
                      join k in listKyTucXa on h.Id equals k.IdThongTinCaNhan
                      select new
                      {
                          Id = k.Id,
                          IdThongTinCaNhan = k.IdThongTinCaNhan != null ? k.IdThongTinCaNhan : null,
                          HoTen = h.HoTen,
                          NgaySinh = h.NgaySinh,
                          NoiSinh = h.NoiSinh,
                          SoNha = h.SoNha,
                          PhuongXa = h.PhuongXa,
                          QuanHuyen = h.QuanHuyen,
                          Tinh = h.Tinh,
                          NgayVao = k.NgayVao != null ? k.NgayVao : null,
                          NgayRa = k.NgayRa != null ? k.NgayRa : null,
                          SoPhong = k.SoPhong != null ? k.SoPhong : null,
                          SoHocTuDo = k.SoHocTuDo != null ? k.SoHocTuDo : null,
                          GhiChu = k.GhiChu != null ? k.GhiChu : null,
                      }).ToList();*/
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
                        q.NgaySinh.ToShortDateString(),
                        
                        // quê quán
                        q.NoiSinh,

                        q.DiaChiLienLac,

                        q.KyTucXas.Where(p => p.NgayVao != null).Select(p => ((DateTime)p.NgayVao).ToShortDateString()).FirstOrDefault(),
                        q.KyTucXas.Where(p => p.NgayRa != null).Select(p => ((DateTime)p.NgayRa).ToShortDateString()).FirstOrDefault(),

                        q.KyTucXas.Where(p => p.SoPhong != null).Select(p => p.SoPhong).FirstOrDefault(),
                        q.KyTucXas.Where(p => p.SoHocTuDo != null).Select(p => p.SoHocTuDo).FirstOrDefault(),
                        
                        q.KyTucXas.Select(p => p.Id).FirstOrDefault(),
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

        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public ActionResult Create()
        {
            var tuple = new Tuple<ThongTinCaNhanEditViewModel, KyTucXaEditViewModel>(new ThongTinCaNhanEditViewModel(), new KyTucXaEditViewModel());
            return View(tuple);
        }
    }
}