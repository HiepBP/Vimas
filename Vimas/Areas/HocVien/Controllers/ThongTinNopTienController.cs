using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Helpers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    [Authorize]
    public class ThongTinNopTienController : BaseController
    {

        //[Authorize(Roles = "Admin, PhongKeToan")]
        // GET: HocVien/ThongTinNopTien
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin, PhongKeToan")]
        public JsonResult LoadDanhSachThongTinNopTien(JQueryDataTableParamModel param)
        {
            var thongTinNopTienService = this.Service<IThongTinNopTienService>();
            var listThongTinNopTien = thongTinNopTienService.GetActive().ProjectTo<ThongTinNopTienViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = listThongTinNopTien
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.ThongTinCaNhan.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderByDescending(q => q.NgayLapPhieu)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.SoPhieu,
                        q.ThongTinCaNhan.HoTen,
                        EnumHelper<TypeOfMoney>.GetDisplayValue((TypeOfMoney)q.LoaiTien),
                        q.SoTien,
                        EnumHelper<ThuChi>.GetDisplayValue((ThuChi)q.ThuHayChi),
                        q.NgayLapPhieu.ToShortDateString(),
                        q.Id,
                    });
                var totalRecords = listThongTinNopTien.Count();
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
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin, PhongKeToan")]
        public ActionResult Create()
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new ThongTinNopTienEditViewModel();
            model.ThuChi = (ThuChi)model.ThuHayChi;
            model.TypeOfMoney = (TypeOfMoney)model.LoaiTien;
            model.AvailableThongTinCaNhan = thongTinCaNhanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.HoTen,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, PhongKeToan")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(ThongTinNopTienEditViewModel model)
        {
            try
            {
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                var thongTinNopTienService = this.Service<IThongTinNopTienService>();
                model.Active = true;
                model.ThuHayChi = (int)model.ThuChi;
                var entity = model.ToEntity();
                await thongTinNopTienService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);
                return Json(new { success = true, message = "Tạo thành công" });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles ="Admin")]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {
            var thongTinNopTienService = this.Service<IThongTinNopTienService>();
            var model = new ThongTinNopTienEditViewModel(await thongTinNopTienService.GetAsync(id));
            if (model == null || model.Active == false)
            {
                return Json(new { success = false, message = Resource.ErrorMessage});
            }
            model.ThuChi = (ThuChi)model.ThuHayChi;
            model.TypeOfMoney = (TypeOfMoney)model.LoaiTien;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(ThongTinNopTienEditViewModel model)
        {
            try
            {
                var thongTinNopTienService = this.Service<IThongTinNopTienService>();
                var entity = await thongTinNopTienService.GetAsync(model.Id);
                
                entity.LoaiTien = model.LoaiTien;
                entity.SoTien = model.SoTien;
                entity.NgayLapPhieu = model.NgayLapPhieu;
                entity.LyDo = model.LyDo;
                entity.ThuHayChi = (int)model.ThuChi;

                await thongTinNopTienService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);
                return Json(new { success = true, message = "Sửa thành công!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var thongTinNopTienService = this.Service<IThongTinNopTienService>();
                var entity = await thongTinNopTienService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                entity.Active = false;
                await thongTinNopTienService.UpdateAsync(entity);
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
            var TTNTService = this.Service<IThongTinNopTienService>();
            var TTCNService = this.Service<IThongTinCaNhanService>();

            var resultLst = TTNTService.Get()
                .Where(q => q.Active == true)
                .ProjectTo<ThongTinNopTienEditViewModel>(this.MapperConfig).ToList();

            var dynamicList = new List<dynamic>();
            var count = 0;
            string loaiTien = "";
            foreach (var item in resultLst)
            {
                switch (item.LoaiTien)
                {
                    case 0:
                        loaiTien = "Tiền dự tuyển";
                        break;
                    case 1:
                        loaiTien = "Tiền đảm bảo khóa học";
                        break;
                    case 2:
                        loaiTien = "Phí dịch vụ thu hộ";
                        break;
                    default:
                        break;
                }

                dynamicList.Add(new
                {
                    stt = ++count,
                    NgNop = TTCNService.Get(item.IdThongTinCaNhan).HoTen,
                    //loaiTien = Enum.GetName(typeof(TypeOfMoney), item.LoaiTien),
                    loaiTien = ((TypeOfMoney)(item.LoaiTien)).GetAttribute<DisplayAttribute>().Name,
                    soPhieu = item.SoPhieu,
                    ngayLapPhieu = item.NgayLapPhieu.ToShortDateString(),
                    thuOrchi = item.ThuHayChi == 0 ? "Thu":"Chi",
                    soTien = item.SoTien.ToString("#0,##"),
                    lyDo = item.LyDo,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Lịch sử thu chi tiền");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "Lịch sử thu chi tiền - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:H1"].Merge = true;
                ws.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Người Nộp";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Loại Tiền";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Số Phiếu";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày Lập Phiếu";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Thu/Chi";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Số Tiền";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Lý Do";
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
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.NgNop;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.loaiTien;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.soPhieu;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.ngayLapPhieu;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.thuOrchi;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.soTien;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.lyDo;

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

                var fileDownloadName = "Lịch sử Thu & Chi tiền - " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion

    }
}