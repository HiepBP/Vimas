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

namespace Vimas.Areas.HeThong.Controllers
{
    public class NghiepDoanController : BaseController
    {
        // GET: HeThong/NghiepDoan
        #region Index
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDanhSachNghiepDoan (JQueryDataTableParamModel param)
        {
            var service = this.Service<INghiepDoanService>();
            var model = service.GetActive().ProjectTo<NghiepDoanViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = model
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenNghiepDoan.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.MaNghiepDoan)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.MaNghiepDoan,
                        q.TenNghiepDoan,
                        q.TenVietTat,
                        q.NguoiDaiDien,
                        q.ChucDanh,
                        q.DienThoai,
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

        #region Detail
        public ActionResult Detail(int id)
        {
            var service = this.Service<INghiepDoanService>();
            var entity = service.Get(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<NghiepDoanViewModel>(entity);

            return this.View(model);
        }
        #endregion

    }
}