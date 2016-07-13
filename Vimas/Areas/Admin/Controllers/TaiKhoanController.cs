using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity.Owin;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Configuration;
using Vimas.Helpers;
using System.Web.Security;

namespace Vimas.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TaiKhoanController : BaseController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager SignInManager;
        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<JsonResult> LoadDanhSachTaiKhoan(JQueryDataTableParamModel param)
        {
            try
            {
                var aspNetUserService = this.Service<IAspNetUsersService>();
                var listThongTinCaNhan = aspNetUserService.Get()
                    .Where(q => q.AspNetRoles.FirstOrDefault().Name!="Admin" && q.AspNetRoles.FirstOrDefault().Name != "SysAdmin")
                    .ProjectTo<AspNetUsersViewModel>(this.MapperConfig);

                var rs = (await listThongTinCaNhan
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.UserName.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.UserName)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .ToListAsync())
                    .Select(q => new object[]
                    {
                        q.UserName,
                        q.AspNetRoles.ToArray(),
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
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    _userManager.PasswordHasher = new MP5Hasher(FormsAuthPasswordFormat.MD5);
                }
                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        #region Tạo người dùng mới
        public ActionResult Create()
        {
            var aspNetRoleService = this.Service<IAspNetRolesService>();
            RegisterViewModel model = new RegisterViewModel();
            model.AvailableRoles = aspNetRoleService.Get(q => q.Name != "Admin" && q.Name != "SysAdmin").Select(q => new SelectListItem()
            {
                Selected = false,
                Value = q.Name,
                Text = q.Name,
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                UserManager.PasswordHasher = new MP5Hasher(FormsAuthPasswordFormat.MD5);
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return Json(new { success = false, message = result.Errors });
                }

                var rs = await UserManager.AddToRoleAsync(user.Id, model.RoleName);

                if (!rs.Succeeded)
                {
                    return Json(new { success = false, message = rs.Errors });

                }

                return Json(new { success = true, message = "Tạo người dùng thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Tạo người dùng thất bại" });
            }
        }

        #endregion


        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            var user = UserManager.Users.FirstOrDefault(q => q.Id.Equals(id));

            if (user == null)
            {
                return Json(new { success = false, message = "Người dùng này không tồn tại trong hệ thống, xin hãy thử lại." });
            }

            var currentRole = UserManager.GetRoles(id);

            try
            {
                var b = await UserManager.RemoveFromRolesAsync(id, currentRole.ToArray());
                var rs = await UserManager.DeleteAsync(user);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng thử lại." });
            }

            return Json(new { success = true, message = "Xóa người dùng thành công" });

        }

        public async Task<ActionResult> Edit(string id)
        {
            var aspNetUserService = this.Service<IAspNetUsersService>();
            var aspNetRoleService = this.Service<IAspNetRolesService>();
            var user = await aspNetUserService.GetAsync(id);
            RegisterViewModel model = new RegisterViewModel()
            {
                Email = user.Email,
                Username = user.UserName,
                Password = user.PasswordHash,
                RoleName = user.AspNetRoles.FirstOrDefault().Name,
            };
            model.AvailableRoles = aspNetRoleService.Get(q => q.Name != "Admin" && q.Name != "SysAdmin").Select(q => new SelectListItem()
            {
                Selected = false,
                Value = q.Name,
                Text = q.Name,
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = "Sửa không thành công, xin liên hệ admin"
                    });
            }
            var user = UserManager.Users.FirstOrDefault(q => q.UserName.Equals(model.Username));

            if (user == null)
            {
                return Json(new { success = false, message = "Người dùng không tồn tại trong hệ thống, cập nhật thất bại." });
            }
            user.Email = model.Email;

            if (!model.Password.Equals(user.PasswordHash))
            {
                UserManager.PasswordHasher = new MP5Hasher(FormsAuthPasswordFormat.MD5);
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                //MembershipUser mu = Membership.GetUser(user.UserName);
                //mu.ChangePassword(mu.ResetPassword(), model.Password);
            }

            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Json(new { success = false, message = "Cập nhật thất bại, vui lòng thử lại." });
            }
            var roles = await UserManager.GetRolesAsync(user.Id);
            var roleArry = new string[roles.Count];
            roles.CopyTo(roleArry, 0);
            var b = await UserManager.RemoveFromRolesAsync(user.Id, roleArry);
            var rs = await UserManager.AddToRoleAsync(user.Id, model.RoleName);

            if (!rs.Succeeded)
            {
                return Json(new { success = false, message = rs.Errors });

            }

            return Json(new { success = true, message = "Cập nhật thành công" });
        }
    }
}