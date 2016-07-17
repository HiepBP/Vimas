using AutoMapper.QueryableExtensions;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Vimas.Areas.Admin.Controllers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    public class SucKhoeController : BaseController
    {
        // GET: HocVien/SucKhoe
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadDanhSachSucKhoe(JQueryDataTableParamModel param)
        {
            var sucKhoeService = this.Service<ISucKhoeService>();
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();

            var list = sucKhoeService.Get()
                .Where(q => q.Active == true)
                .ProjectTo<SucKhoeEditViewModel>(this.MapperConfig).ToList();

            foreach (var item in list)
            {
                item.Name = thongTinCaNhanService.Get(item.IdThongTinCaNhan).HoTen;
            }

            try
            {
                var count = 0;
                var rs = list
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.Name.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.Name)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        ++count,
                        q.IdThongTinCaNhan,
                        q.Name,
                        q.NhomMau,
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
        #endregion

        #region Create
        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Create()
        {
            var model = new SucKhoeEditViewModel();
            PrepareEdit(model);
            return this.View(model);
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(SucKhoeEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var service = this.Service<ISucKhoeService>();
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
        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Edit(int id)
        {
            var service = this.Service<ISucKhoeService>();
            var entity = service.Get(id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            var model = Mapper.Map<SucKhoeEditViewModel>(entity);
            PrepareEdit(model);
            return this.View(model);
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> Edit(SucKhoeEditViewModel model)
        {
            var service = this.Service<ISucKhoeService>();

            if (!this.ModelState.IsValid)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra, vui lòng kiểm tra lại." });
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
                return Json(new { success = false, message = "Có lỗi xảy ra, vui lòng thử lại." });
            }
            return Json(new { success = true, message = "Cập nhật thành công." });

        }
        #endregion

        #region Detail
        public ActionResult Detail(int id)
        {
            var sucKhoeService = this.Service<ISucKhoeService>();
            var TTCNService = this.Service<IThongTinCaNhanService>();

            var entity = sucKhoeService.Get(id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            var model = Mapper.Map<SucKhoeEditViewModel>(entity);

            model.Name = TTCNService.Get(model.IdThongTinCaNhan).HoTen;

            return this.View(model);
        }
        #endregion

        #region Delete
        [Authorize(Roles = "Admin, PhongNguon")]
        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            var service = this.Service<ISucKhoeService>();

            var entity = service.Get(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Không tồn tại báo cáo này, vui lòng thử lại." });
            }

            entity.Active = false;
            try
            {
                await service.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng thử lại." });
            }

            return Json(new { success = true, message = "Xóa báo cáo thành công" });
        }
        #endregion

        #region Export Excel
        public List<dynamic> GetDataList()
        {
            var sucKhoeService = this.Service<ISucKhoeService>();
            var TTCNService = this.Service<IThongTinCaNhanService>();

            var resultLst = sucKhoeService.Get()
                .Where(q => q.Active == true)
                .ProjectTo<SucKhoeEditViewModel>(this.MapperConfig).ToList();

            foreach (var item in resultLst)
            {
                item.Name = TTCNService.Get(item.IdThongTinCaNhan).HoTen;
            }

            var dynamicList = new List<dynamic>();
            var count = 0;
            foreach (var item in resultLst)
            {
                dynamicList.Add(new
                {
                    stt = ++count,
                    idTTCN = item.IdThongTinCaNhan,
                    Name = item.Name,
                    Height = item.ChieuCao,
                    Weight = item.CanNang,
                    BloodType = item.NhomMau,
                    Hand = item.TayThuan == 1 ? "Tay Phải" : "Tay trái",
                    Tattoo = item.HinhXam == true ? "Có" : "Không",
                    LeftEye = item.ThiLucMatTrai,
                    RightEye = item.ThiLucMatPhai,
                    FirstTime = item.NgayKhamDot1.GetValueOrDefault().ToShortDateString(),
                    FirstTimeNote = item.GhiChuSucKhoeDot1,
                    SecondTime = item.NgayKhamDot2.GetValueOrDefault().ToShortDateString(),
                    SecondTimeNote = item.GhiChuSucKhoeDot2,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Báo Cáo Sức Khỏe");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "BÁO CÁO SỨC KHỎE - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:N1"].Merge = true;
                ws.Cells["A1:N1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "ID Cá Nhân";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Chiều Cao";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Cân Nặng";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Nhóm Máu";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tay Thuận";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Hình Xăm";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Thị Lực Mắt Trái";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Thị Lực Mắt Phải";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày Khám Đợt 1";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ghi Chú Đợt 1";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày Khám Đợt 2";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Ghi Chú Đợt 2";
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
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.idTTCN;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Name;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Height;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Weight;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.BloodType;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Hand;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Tattoo;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.LeftEye;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.RightEye;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.FirstTime;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.FirstTimeNote;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.SecondTime;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.SecondTimeNote;

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

                var fileDownloadName = "Báo Cáo Sức Khỏe - "+DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion

        public void PrepareEdit(SucKhoeEditViewModel model)
        {
            //thong tin ca nhan service
            var TTCNSerice = this.Service<IThongTinCaNhanService>();

            model.AvailableIDs = TTCNSerice.Get()
                .Where(q => q.Active == true)
                .Select(q => new SelectListItem()
                {
                    Selected = false,
                    Text = q.HoTen,
                    Value = q.Id.ToString(),
                });
        }


        //Testing
        public ActionResult ListToExcel()
        {
            //start excel
            Microsoft.Office.Interop.Excel.Application excapp;
            Workbook excelworkBook;
            Worksheet sheet;

            MemoryStream ms = new MemoryStream();

            excapp = new Microsoft.Office.Interop.Excel.Application();

            excapp.Visible = false;
            excapp.DisplayAlerts = false;

            //create a blank workbook
            excelworkBook = excapp.Workbooks.Add(Type.Missing);

            sheet = (Worksheet)excelworkBook.ActiveSheet;
            sheet.Name = "BaoCao";

            #region Set Header
            int headerCol = 1;
            sheet.Cells[1, headerCol] = "STT";
            sheet.Cells[1, ++headerCol] = "ID Cá Nhân";
            sheet.Cells[1, ++headerCol] = "Tên";
            sheet.Cells[1, ++headerCol] = "Chiều Cao";
            sheet.Cells[1, ++headerCol] = "Cân Nặng";
            sheet.Cells[1, ++headerCol] = "Nhóm Máu";
            sheet.Cells[1, ++headerCol] = "Tay Thuận";
            sheet.Cells[1, ++headerCol] = "Hình Xăm";
            sheet.Cells[1, ++headerCol] = "Thị Lực Mắt Trái";
            sheet.Cells[1, ++headerCol] = "Thị Lực Mắt Phải";
            sheet.Cells[1, ++headerCol] = "Ngày Khám Đợt 1";
            sheet.Cells[1, ++headerCol] = "Ghi Chú Đợt 1";
            sheet.Cells[1, ++headerCol] = "Ngày Khám Đợt 2";
            sheet.Cells[1, ++headerCol] = "Ghi Chú Đợt 2";
            #endregion

            #region Parse Data from List to Excel
            int row = 2, col = 1;
            foreach (var item in this.GetDataList())
            {
                sheet.Cells[row, col] = item.stt;
                sheet.Cells[row, ++col] = item.idTTCN;
                sheet.Cells[row, ++col] = item.Name;
                sheet.Cells[row, ++col] = item.Height;
                sheet.Cells[row, ++col] = item.Weight;
                sheet.Cells[row, ++col] = item.BloodType;
                sheet.Cells[row, ++col] = item.Hand;
                sheet.Cells[row, ++col] = item.Tattoo;
                sheet.Cells[row, ++col] = item.LeftEye;
                sheet.Cells[row, ++col] = item.RightEye;
                sheet.Cells[row, ++col] = item.FirstTime;
                sheet.Cells[row, ++col] = item.FirstTimeNote;
                sheet.Cells[row, ++col] = item.SecondTime;
                sheet.Cells[row, ++col] = item.SecondTimeNote;

                col = 1;
                row++;
            }
            #endregion

            excelworkBook.SaveAs(ms);
            excelworkBook.Close();
            excapp.Quit();

            ms.Seek(0, SeekOrigin.Begin);

            var fileDownloadName = "Báo Cáo Sức Khỏe.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return this.File(ms, contentType, fileDownloadName);
        }
    }
}