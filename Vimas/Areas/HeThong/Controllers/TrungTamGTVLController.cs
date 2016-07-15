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
    [Authorize]
    public class TrungTamGTVLController : BaseController
    {
        // GET: HeThong/TrungTamGTVL
        #region Index
        public ActionResult Index()
        {
            return this.View();
        }
        public JsonResult LoadDanhSachTrungTam(JQueryDataTableParamModel param)
        {
            var service = this.Service<ITrungTamGTVLService>();
            var model = service.Get().Where(q => q.Active == true)
                .ProjectTo<TrungTamGTVLViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = model
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.TenCoSo.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.TenCoSo)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.MaNguon,
                        q.TenCoSo,
                        q.DiaChi,
                        q.DienThoai,
                        q.Fax,
                        q.SoHDLK,
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

        #region Create
        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Create()
        {
            var model = new TrungTamGTVLViewModel();
            return this.View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        public async Task<ActionResult> Create(TrungTamGTVLViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var service = this.Service<ITrungTamGTVLService>();
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
        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Edit(int id)
        {
            var service = this.Service<ITrungTamGTVLService>();

            var entity = service.Get(id);

            if (entity == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<TrungTamGTVLViewModel>(entity);

            return this.View(model);
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        [HttpPost]
        public async Task<JsonResult> Edit(TrungTamGTVLViewModel model)
        {
            var service = this.Service<ITrungTamGTVLService>();

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

        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Detail(int id)
        {
            var service = this.Service<ITrungTamGTVLService>();
            var entity = service.Get(id);
            if(entity == null)
            {
                return HttpNotFound();
            }
            var model = Mapper.Map<TrungTamGTVLViewModel>(entity);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        public async Task<JsonResult> Delete(int id)
        {
            var trungTamGTVLService = this.Service<ITrungTamGTVLService>();

            var entity = trungTamGTVLService.Get(id);

            if (entity == null)
            {
                return Json(new { success = false, message = "Không tồn tại trung tâm này, xin vui lòng thử lại." });
            }
            else
            {
                try
                {
                    entity.Active = false;
                    await trungTamGTVLService.UpdateAsync(entity);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Xóa trung tâm thất bại, xin vui lòng thử lại." });
                };
            }
            return Json(new { success = true, message = "Xóa trung tâm thành công." });
        }

        #region Export Excel
        public List<dynamic> GetDataList()
        {
            var service = this.Service<ITrungTamGTVLService>();

            var resultLst = service.Get()
                .Where(q => q.Active == true)
                .ProjectTo<TrungTamGTVLViewModel>(this.MapperConfig).ToList();

            var dynamicList = new List<dynamic>();
            var count = 0;
            foreach (var item in resultLst)
            {
                if(!item.NgayKyHopDong.HasValue)
                {
                    item.NgayKyHopDong = DateTime.MinValue;
                }
                if (!item.NgayHetHan.HasValue)
                {
                    item.NgayHetHan = DateTime.MinValue;
                }
                dynamicList.Add(new
                {
                    stt = ++count,
                    Name = item.TenCoSo,
                    MaNguon = item.MaNguon,
                    Address = item.DiaChi,
                    Phone = item.DienThoai,
                    Fax = item.Fax,
                    HDLK = item.SoHDLK,
                    NgayKi = item.NgayKyHopDong.Value.ToShortDateString(),
                    NgayHet = item.NgayHetHan.Value.ToShortDateString(),
                    NgDaiDien = item.NguoiDaiDien,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Danh sách trung tâm giới thiệu việc làm");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "Danh sách trung tâm GTVL - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:J1"].Merge = true;
                ws.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tên cơ sở";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Mã nguồn";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa chỉ";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Điện thoại";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Fax";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Số HDLK";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày kí hợp đồng";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày hết hạn";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Người đại diện";
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
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Name;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.MaNguon;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Address;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Phone;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.Fax;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.HDLK;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.NgayKi;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.NgayHet;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.NgDaiDien;

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

                var fileDownloadName = "Danh sách trung tâm GTVL - " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion

    }
}