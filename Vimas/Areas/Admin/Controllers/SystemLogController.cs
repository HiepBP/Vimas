using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities;
using Vimas.Models.Entities.Services;

namespace Vimas.Areas.Admin.Controllers
{
    [Authorize]
    public class SystemLogController : BaseController
    {
        [Authorize(Roles ="Admin")]
        // GET: Admin/SystemLog
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> Create(string action, string tableName, int entityId)
        {
            var systemLogService = this.Service<ISystemLogService>();
            var user = System.Web.HttpContext.Current.User;
            //try
            //{
                var entity = new SystemLog()
                {
                    NgayThucHien = DateTime.Now,
                    HanhDong = action,
                    TenBang = tableName,
                    ThucHienBoi = user.Identity.Name,
                    id = entityId,
                };
                await systemLogService.CreateAsync(entity);
                return Json(new { success = true });
            //}
            //catch (Exception e)
            //{
            //    return Json(new { success = false });
            //}
        }

        [Authorize(Roles = "Admin")]
        public JsonResult LoadDanhSachLog(JQueryDataTableParamModel param)
        {
            var systemLogService = this.Service<ISystemLogService>();
            var listSystemLog = systemLogService.GetActive().ToList();
            //try
            //{
                var rs = listSystemLog
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.HanhDong.ToLower().Contains(param.sSearch.ToLower())
                        || q.ThucHienBoi.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderByDescending(q => q.NgayThucHien)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.TenBang,
                        q.HanhDong,
                        q.NgayThucHien.Value.ToString(),
                        q.ThucHienBoi,
                    });
                var totalRecords = listSystemLog.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = rs
                }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception e)
            //{
            //    return Json(new { success = false, message = Resource.ErrorMessage });
            //}
        }
    }
}