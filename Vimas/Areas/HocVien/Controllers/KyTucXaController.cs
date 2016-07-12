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
            var listKyTucXa = kyTucXaService.GetActive().ToList();

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
                var rs = listKyTucXa
                    .Where(q => q.Active == true)
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.ThongTinCaNhan.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.ThongTinCaNhan.HoTen)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.ThongTinCaNhan.HoTen,
                        q.ThongTinCaNhan.NgaySinh.ToShortDateString(),
                        
                        // quê quán
                        q.ThongTinCaNhan.NoiSinh,

                        q.ThongTinCaNhan.DiaChiLienLac,

                        ((DateTime)q.NgayVao).ToShortDateString(),
                        q.NgayRa != null ? ((DateTime)q.NgayRa).ToShortDateString() : null,

                        q.SoPhong,
                        q.SoHocTuDo,

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

        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public ActionResult Create()
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new KyTucXaEditViewModel();

            model.AvailableThongTinCaNhan = thongTinCaNhanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.HoTen + "[CMND: " + q.CMND +"]",
                Value = q.Id.ToString(),
                Selected = false,
            });
            
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<ActionResult> Create(KyTucXaEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var kyTucXaService = this.Service<IKyTucXaService>();

                model.Active = true;

                await kyTucXaService.CreateAsync(model.ToEntity());
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<JsonResult> Del(int idktx)
        {
            try
            {
                var kyTucXaService = this.Service<IKyTucXaService>();
                var entity = await kyTucXaService.GetAsync(idktx);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                //entity.Active = false;
                await kyTucXaService.DeactivateAsync(entity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin, PhongQuanLyKTX")]
        //public async Task<JsonResult> Add(int idttcn)
        //{
        //    try
        //    {
        //        var kyTucXaService = this.Service<IKyTucXaService>();
        //        var model = new KyTucXaEditViewModel();
        //        model.IdThongTinCaNhan = idttcn;
        //        model.Active = true;
        //        await kyTucXaService.CreateAsync(model.ToEntity());
        //        return Json(new { success = true, message = "Thêm thành công" });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false, message = Resource.ErrorMessage });
        //    }
        //}

        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<ActionResult> Edit(int idktx)
        {
            var kyTucXaService = this.Service<IKyTucXaService>();
            var model = new KyTucXaEditViewModel(await kyTucXaService.GetAsync(idktx));

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<ActionResult> Edit(KyTucXaEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var kyTucXaService = this.Service<IKyTucXaService>();

            var entity = await kyTucXaService.GetAsync(model.Id);
            model.CopyToEntity(entity);
            entity.Active = true;
            entity.IdThongTinCaNhan = model.IdThongTinCaNhan;
            await kyTucXaService.UpdateAsync(entity);

            return RedirectToAction("Index");
        }

    }
}