using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HeThong.Controllers
{
    public class CongTyTiepNhanController : BaseController
    {
        // GET: HeThong/CongTyTiepNhan
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCongTyTiepNhan(JQueryDataTableParamModel param)
        {
            var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
            var listCongTyTiepNhan = congTyTiepNhanService.GetActive().ToList();
            try
            {
                var rs = listCongTyTiepNhan
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenTiengNhat.ToLower().Contains(param.sSearch.ToLower())
                        || q.TenTiengAnh.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.TenTiengNhat)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.TenTiengNhat,
                        q.TenTiengAnh,
                        q.NganhNghe,
                        q.NguoiDaiDien,
                        q.DiaChi,
                        q.DienThoai,
                        q.Fax,
                        q.idNghiepDoan.HasValue ? q.NghiepDoan.TenNghiepDoan : "",
                        q.Id,
                    });
                var totalRecords = listCongTyTiepNhan.Count();
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

        public ActionResult Create()
        {
            var nghiepDoanService = this.Service<INghiepDoanService>();
            var model = new CongTyTiepNhanEditViewModel();
            model.AvailableNghiepDoan = nghiepDoanService.GetActive().Select(q => new SelectListItem() {
                Text = q.TenNghiepDoan,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(CongTyTiepNhanEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
                model.Active = true;
                var entity = model.ToEntity();
                await congTyTiepNhanService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);
                return Json(new { success = true, message = "Tạo thành công" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
            var nghiepDoanService = this.Service<INghiepDoanService>();
            var model = new CongTyTiepNhanEditViewModel(await congTyTiepNhanService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
            model.AvailableNghiepDoan = nghiepDoanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenNghiepDoan,
                Value = q.Id.ToString(),
                Selected = q.Id == model.idNghiepDoan,
            });
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(CongTyTiepNhanEditViewModel model)
        {
            try
            {
                var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
                var entity = await congTyTiepNhanService.GetAsync(model.Id);
                model.CopyToEntity(entity);
                entity.Active = true;
                await congTyTiepNhanService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);
                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }
        
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var congTyTiepNhanService = this.Service<ICongTyTiepNhanService>();
                var entity = await congTyTiepNhanService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                await congTyTiepNhanService.DeactivateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        #region Export Excel
        public List<dynamic> GetDataList()
        {
            var CTTNService = this.Service<ICongTyTiepNhanService>();
            var nghiepDoanService = this.Service<INghiepDoanService>();

            var resultLst = CTTNService.Get()
                .Where(q => q.Active == true)
                .ProjectTo<CongTyTiepNhanEditViewModel>(this.MapperConfig).ToList();

            foreach (var item in resultLst)
            {
                if (item.idNghiepDoan.HasValue)
                {
                    item.nghiepDoanName = nghiepDoanService.Get(item.idNghiepDoan).TenNghiepDoan;
                }
            }

            var dynamicList = new List<dynamic>();
            var count = 0;
            foreach (var item in resultLst)
            {
                dynamicList.Add(new
                {
                    stt = ++count,
                    name = item.TenTiengAnh,
                    japName = item.TenTiengNhat,
                    nghiepDoan = item.nghiepDoanName,
                    job = item.NganhNghe,
                    ngDaiDien = item.NguoiDaiDien,
                    address = item.DiaChi,
                    phone = item.DienThoai,
                    fax = item.Fax,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Danh sách công ty tiếp nhận");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "DANH SÁCH CÔNG TY TIẾP NHẬN - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:I1"].Merge = true;
                ws.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên tiếng Anh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên tiếng Nhật";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Thuộc nghiệp đoàn";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngành nghề";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Người đại diện";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa chỉ";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Điện thoại";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Fax";
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
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.name;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.japName;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.nghiepDoan;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.job;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.ngDaiDien;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.address;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.phone;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.fax;

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

                var fileDownloadName = "Danh sách công ty tiếp nhận - " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion

    }
}