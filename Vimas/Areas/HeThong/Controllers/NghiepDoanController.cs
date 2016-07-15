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

        #region Create
        [Authorize(Roles = "Admin, PhongXKLD")]
        public ActionResult Create()
        {
            var model = new NghiepDoanViewModel();
            return this.View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, PhongXKLD")]
        public async Task<ActionResult> Create(NghiepDoanViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var service = this.Service<INghiepDoanService>();
            model.Active = true;
            try
            {
                await service.CreateAsync(model.ToEntity());
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
            var service = this.Service<INghiepDoanService>();

            var entity = service.Get(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<NghiepDoanViewModel>(entity);

            return this.View(model);
        }

        [Authorize(Roles = "Admin, PhongXKLD")]
        [HttpPost]
        public async Task<JsonResult> Edit(NghiepDoanViewModel model)
        {
            var service = this.Service<INghiepDoanService>();

            if (!this.ModelState.IsValid)
            {
                return Json(new { success = false, message = "Đã có lỗi xảy ra, vui lòng kiểm tra lại thông tin." });
            }

            try
            {
                await service.UpdateAsync(model.ToEntity());
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
            var nghiepDoanService = this.Service<INghiepDoanService>();

            var entity = nghiepDoanService.Get(id);

            if (entity == null)
            {
                return Json(new { success = false, message = "Không tồn tại nghiệp đoàn này, xin vui lòng thử lại." });
            }
            else
            {
                try
                {
                    entity.Active = false;
                    await nghiepDoanService.UpdateAsync(entity);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Xóa nghiệp đoàn thất bại, xin vui lòng thử lại." });
                };
            }
            return Json(new { success = true, message = "Xóa nghiệp đoàn thành công." });
        }
        #endregion

        #region Export Excel
        public List<dynamic> GetDataList()
        {
            var service = this.Service<INghiepDoanService>();

            var resultLst = service.Get()
                .Where(q => q.Active == true)
                .ProjectTo<NghiepDoanViewModel>(this.MapperConfig).ToList();


            var dynamicList = new List<dynamic>();
            var count = 0;
            foreach (var item in resultLst)
            {
                dynamicList.Add(new
                {
                    stt = ++count,
                    name = item.TenNghiepDoan,
                    shortName = item.TenVietTat,
                    daiDien = item.NguoiDaiDien,
                    chucDanh = item.ChucDanh,
                    diaChi = item.DiaChi,
                    phone = item.DienThoai,
                    fax = item.Fax,
                    ngayKi = item.NgayKyHopDong.GetValueOrDefault().ToShortDateString(),
                    salary = item.LuongCoBan.GetValueOrDefault().ToString("#0,## đ"),
                    phiDichVu = item.PhiDichVu.GetValueOrDefault().ToString("#0,## đ"),
                    phiUngTruoc = item.PhiUTDT.GetValueOrDefault().ToString("#0,## đ"),
                    website = item.WebsiteUrl,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Danh Sách Nghiệp Đoàn");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "DANH SÁCH NGHIỆP ĐOÀN - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:M1"].Merge = true;
                ws.Cells["A1:M1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên Nghiệp Đoàn";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên Viết Tắt";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Người Đại Diện";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Chức Danh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa Chỉ";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Điện Thoại";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Fax";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày Kí Hợp Đồng";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Lương Cơ Bản";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Phí Dịch Vụ";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Phí Ứng Trước";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Website";
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
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.shortName;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.daiDien;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.chucDanh;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.diaChi;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.phone;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.fax;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.ngayKi;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.salary;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.phiDichVu;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.phiUngTruoc;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.website;

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

                var fileDownloadName = "Danh Sách Nghiệp Đoàn - " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion

    }
}