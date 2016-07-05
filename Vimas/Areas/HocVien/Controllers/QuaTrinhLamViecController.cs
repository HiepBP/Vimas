﻿using AutoMapper.QueryableExtensions;
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
    public class QuaTrinhLamViecController : BaseController
    {
        //
        // GET: /HocVien/QuaTrinhLamViec/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadQuaTrinhLamViec(JQueryDataTableParamModel param, int userId)
        {
            var quaTrinhLamViecService = this.Service<IQuaTrinhLamViecService>();
            var listQuaTrinhLamViec = quaTrinhLamViecService.GetByIdThongTinCaNhan(userId).ProjectTo<QuaTrinhLamViecViewModel>(this.MapperConfig).ToList();
            try { 
            var result = listQuaTrinhLamViec
                .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenCongTy.ToLower().Contains(param.sSearch.ToLower()))
                 .OrderBy(q => q.HinhThucCongTy)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.TenCongTy,
                        q.HinhThucCongTy,
                        q.ChiTietCongViec,
                        q.DiaChiCongTy,
                        q.DangLam,
                        q.TuNam.HasValue ? q.TuNam : 0,
                        q.DenNam.HasValue ? q.DenNam : 0,
                        q.Id,
                    });
            var numberRecord = result.Count();


            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = numberRecord,
                iTotalDisplayRecords = numberRecord,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error" });
            }
        }
        //
        // GET: /HocVien/QuaTrinhLamViec/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /HocVien/QuaTrinhLamViec/Create
        public ActionResult Create(int idThongTinCaNhan)
        {
            var quaTrinhLamViecService = this.Service<IQuaTrinhLamViecService>();
            var model = new QuaTrinhLamViecViewModel()
            {
                IdThongTinCaNhan = idThongTinCaNhan,
                DangLam = false,
            };
            return View(model);
        }

        //
        // POST: /HocVien/QuaTrinhLamViec/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(QuaTrinhLamViecViewModel model)
        {
            try
            {
                var quaTrinhLamViecService = this.Service<IQuaTrinhLamViecService>();
                model.Active = true;
                await quaTrinhLamViecService.CreateAsync(model.ToEntity());
                return Json(new { success = true, message = "Tạo thành công!" });
            }
            catch
            {
                return Json(new { success = false, message = "Có lỗi xảy ra, xin liên hệ admin!!!" });
            }
        }

        //
        // GET: /HocVien/QuaTrinhLamViec/Edit/5
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var quaTrinhLamViecService = this.Service<IQuaTrinhLamViecService>();
            var model = new QuaTrinhLamViecEditViewModel(await quaTrinhLamViecService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, });
            }
            return View(model);
        }

        //
        // POST: /HocVien/QuaTrinhLamViec/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(QuaTrinhLamViecEditViewModel model)
        {
            try
            {
                var quaTrinhLamViecService = this.Service<IQuaTrinhLamViecService>();
                var modifiedEntity = await quaTrinhLamViecService.GetAsync(model.Id);
                if (!model.DangLam.HasValue)
                {
                    model.DangLam = false;
                }
                model.CopyToEntity(modifiedEntity);
                modifiedEntity.Active = true;
                modifiedEntity.IdThongTinCaNhan = model.IdThongTinCaNhan;
                await quaTrinhLamViecService.UpdateAsync(modifiedEntity);
                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch
            {
                return Json(new { success = false, message = "Có lỗi xảy ra, xin liên hệ admin!!!" });
            }
        }

      
        //
        // POST: /HocVien/QuaTrinhLamViec/Delete/5
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var quaTrinhLamViecService = this.Service<IQuaTrinhLamViecService>();
                var deleteEntity = await quaTrinhLamViecService.GetAsync(id);
                if (deleteEntity == null || deleteEntity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                await quaTrinhLamViecService.DeleteAsync(deleteEntity);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
    }
}