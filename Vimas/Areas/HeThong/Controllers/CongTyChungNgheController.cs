using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HeThong.Controllers
{
    public class CongTyChungNgheController : BaseController
    {
        // GET: HeThong/CongTyChungNghe
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDanhSachCongTyChungNghe(JQueryDataTableParamModel param)
        {
            var service = this.Service<ICongTyChungNgheService>();
            var model = service.GetActive().ProjectTo<CongTyChungNgheViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = model
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenTiengViet.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.TenVietTat)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.TenVietTat,
                        q.TenTiengViet,
                        q.TenTiengAnh,
                        q.DiaChiTiengViet,
                        q.NguoiDaiDien,
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
            var service = this.Service<ICongTyChungNgheService>();
            var entity = service.Get(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<CongTyChungNgheViewModel>(entity);

            return this.View(model);
        }
        #endregion

        #region Create
        [Authorize(Roles = "Admin, PhongXKLD")]
        public ActionResult Create()
        {
            var model = new CongTyChungNgheViewModel();
            return this.View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, PhongXKLD")]
        public async Task<ActionResult> Create(CongTyChungNgheViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var service = this.Service<ICongTyChungNgheService>();
            model.Active = true;
            try
            {
                var entity = model.ToEntity();
                await service.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return this.RedirectToAction("Index");
        }
        #endregion

        #region Edit
        [Authorize(Roles = "Admin, PhongXKLD")]
        public ActionResult Edit(int id)
        {
            var service = this.Service<ICongTyChungNgheService>();

            var entity = service.Get(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CongTyChungNgheViewModel>(entity);

            return this.View(model);
        }

        [Authorize(Roles = "Admin, PhongXKLD")]
        [HttpPost]
        public async Task<JsonResult> Edit(CongTyChungNgheViewModel model)
        {
            var service = this.Service<ICongTyChungNgheService>();

            if (!this.ModelState.IsValid)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng kiểm tra lại thông tin." });
            }

            try
            {
                var entity = model.ToEntity();
                await service.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng kiểm thử lại." });
            }

            return Json(new { success = true, message = "Chỉnh sửa thông tin thành công." });
        }
        #endregion

        #region Delete
        [HttpPost]
        [Authorize(Roles = "Admin, PhongXKLD")]
        public async Task<JsonResult> Delete(int id)
        {
            var congTyCNService = this.Service<ICongTyChungNgheService>();

            var entity = congTyCNService.Get(id);

            if (entity == null)
            {
                return Json(new { success = false, message = "Không tồn tại công ty này, xin vui lòng thử lại." });
            }
            else
            {
                try
                {
                    entity.Active = false;
                    await congTyCNService.UpdateAsync(entity);
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                    var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Xóa công ty thất bại, xin vui lòng thử lại." });
                };
            }
            return Json(new { success = true, message = "Xóa công ty thành công." });
        }
        #endregion

        #region Export Excel
        public List<dynamic> GetDataList()
        {
            var service = this.Service<ICongTyChungNgheService>();

            var resultLst = service.Get()
                .Where(q => q.Active == true)
                .ProjectTo<CongTyChungNgheViewModel>(this.MapperConfig)
                .ToList();

            var dynamicList = new List<dynamic>();
            var count = 0;
            foreach (var item in resultLst)
            {
                dynamicList.Add(new
                {
                    stt = ++count,
                    vietTat = item.TenVietTat,
                    tenAnh = item.TenTiengAnh,
                    tenViet = item.TenTiengViet,
                    diachiAnh = item.DiaChiTiengAnh,
                    diachiViet = item.DiaChiTiengViet,
                    phone = item.DienThoai,
                    daiDien = item.NguoiDaiDien,
                    chucDanh = item.ChucDanh,
                    von = item.VonDieuLe.GetValueOrDefault().ToString("#0,##"),
                    employee = item.SoNhanVien,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Danh sách công ty chứng nghề");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "DANH SÁCH CÔNG TY CHỨNG NGHỀ - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:K1"].Merge = true;
                ws.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên Viết Tắt";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên Tiếng Anh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên Tiếng Việt";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa Chỉ Tiếng Anh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa Chỉ Tiếng Việt";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Điện Thoại";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Người Đại Diện";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Chức Danh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Vốn Điều Lệ";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Số Nhân Viên";
                var EndHeaderChar = StartHeaderChar;
                var EndHeaderNumber = StartHeaderNumber;
                StartHeaderChar = 'A';
                #endregion
                #region Set style for rows and columns

                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()].Style.Font.Bold = true;

                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()].AutoFitColumns();

                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()]
                    .Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()]
                    .Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.GreenYellow);

                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                ws.Cells["" + StartHeaderChar + StartHeaderNumber.ToString() +
                    ":" + EndHeaderChar + EndHeaderNumber.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thick;

                ws.View.FreezePanes(2, 1);
                #endregion
                #region Set values for cells                
                foreach (var data in listDT)
                {
                    ws.Cells["" + (StartHeaderChar++) + (++StartHeaderNumber)].Value = data.stt;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.vietTat;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.tenAnh;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.tenViet;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.diachiAnh;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.diachiViet;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.phone;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.daiDien;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.chucDanh;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.von;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.employee;

                    StartHeaderChar = 'A';
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                ws.Cells[ws.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[ws.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells[ws.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[ws.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                #endregion
                package.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var fileDownloadName = "Danh sách công ty chứng nghề - " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion

    }
}