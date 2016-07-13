using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Models.Entities;
using Vimas.Models.Entities.Services;

namespace Vimas.Areas.Admin.Controllers
{
    public class SystemLogController : BaseController
    {
        // GET: Admin/SystemLog
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> Create(string action, string tableName, int entityId)
        {
            var systemLogService = this.Service<ISystemLogService>();
            var user = System.Web.HttpContext.Current.User;
            try
            {
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
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }
    }
}