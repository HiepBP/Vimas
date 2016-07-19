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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Areas.Admin.Controllers;
using Vimas.Models;
using Vimas.Models.Entities.Services;
using Vimas.ViewModels;

namespace Vimas.Areas.HocVien.Controllers
{
    public class KyTucXaController : BaseController
    {
        // GET: HocVien/KyTucXa
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadThongTinKyTucXa(JQueryDataTableParamModel param)
        {
            var kyTucXaService = this.Service<IKyTucXaService>();
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var listKyTucXa = kyTucXaService.GetActive().ToList();

            /*var res = (from h in listThongTinCaNhan
                      join k in listKyTucXa on h.Id equals k.IdThongTinCaNhan
                      select new
                      {
                          Id = k.Id,
                          IdThongTinCaNhan = k.IdThongTinCaNhan != null ? k.IdThongTinCaNhan : null,
                          HoTen = h.HoTen,
                          NgaySinh = h.NgaySinh,
                          NoiSinh = h.NoiSinh,
                          SoNha = h.SoNha,
                          PhuongXa = h.PhuongXa,
                          QuanHuyen = h.QuanHuyen,
                          Tinh = h.Tinh,
                          NgayVao = k.NgayVao != null ? k.NgayVao : null,
                          NgayRa = k.NgayRa != null ? k.NgayRa : null,
                          SoPhong = k.SoPhong != null ? k.SoPhong : null,
                          SoHocTuDo = k.SoHocTuDo != null ? k.SoHocTuDo : null,
                          GhiChu = k.GhiChu != null ? k.GhiChu : null,
                      }).ToList();*/
            try
            {
                var rs = listKyTucXa
                    .Where(q => q.Active == true)
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.ThongTinCaNhan.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.ThongTinCaNhan.HoTen)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.ThongTinCaNhan.HoTen,
                        q.ThongTinCaNhan.NgaySinh.ToShortDateString(),
                        
                        // quê quán
                        q.ThongTinCaNhan.NoiSinh,

                        q.ThongTinCaNhan.DiaChiLienLac,

                        ((DateTime)q.NgayVao).ToShortDateString(),
                        q.NgayRa != null ? ((DateTime)q.NgayRa).ToShortDateString() : null,

                        q.SoPhong,
                        q.SoHocTuDo,

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

        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public ActionResult Create()
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new KyTucXaEditViewModel();

            model.AvailableThongTinCaNhan = thongTinCaNhanService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.HoTen + "[CMND: " + q.CMND + "]",
                Value = q.Id.ToString(),
                Selected = false,
            });

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<ActionResult> Create(KyTucXaEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var kyTucXaService = this.Service<IKyTucXaService>();

                model.Active = true;
                var entity = model.ToEntity();
                await kyTucXaService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<JsonResult> Del(int idktx)
        {
            try
            {
                var kyTucXaService = this.Service<IKyTucXaService>();
                var entity = await kyTucXaService.GetAsync(idktx);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                //entity.Active = false;
                await kyTucXaService.DeactivateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);
                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<ActionResult> Edit(int idktx)
        {
            var kyTucXaService = this.Service<IKyTucXaService>();
            var model = new KyTucXaEditViewModel(await kyTucXaService.GetAsync(idktx));

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongQuanLyKTX")]
        public async Task<ActionResult> Edit(KyTucXaEditViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(model);
                }

                var kyTucXaService = this.Service<IKyTucXaService>();
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                var entity = await kyTucXaService.GetAsync(model.Id);
                model.CopyToEntity(entity);
                entity.Active = true;
                entity.IdThongTinCaNhan = model.IdThongTinCaNhan;
                entity.ThongTinCaNhan = await thongTinCaNhanService.GetAsync(entity.IdThongTinCaNhan);
                await kyTucXaService.UpdateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        #region Export Excel
        public List<dynamic> GetDataList()
        {
            var KTXService = this.Service<IKyTucXaService>();
            var TTCNService = this.Service<IThongTinCaNhanService>();

            var resultLst = KTXService.Get()
                .Where(q => q.Active == true)
                .ProjectTo<KyTucXaEditViewModel>(this.MapperConfig).ToList();

            foreach (var item in resultLst)
            {
                item.ThongTinCaNhan = Mapper.Map<ThongTinCaNhanEditViewModel>(TTCNService.Get(item.IdThongTinCaNhan));
            }

            var dynamicList = new List<dynamic>();
            DateTime? nullTime = null;
            var count = 0;
            foreach (var item in resultLst)
            {
                dynamicList.Add(new
                {
                    stt = ++count,
                    ten = item.ThongTinCaNhan.HoTen,
                    japName = item.ThongTinCaNhan.TenPhienAmNhat,
                    sex = ((Gender)item.ThongTinCaNhan.GioiTinh).GetAttribute<DisplayAttribute>().Name,
                    birthdate = item.ThongTinCaNhan.NgaySinh.ToShortDateString(),
                    address = item.ThongTinCaNhan.DiaChiLienLac,
                    home = item.ThongTinCaNhan.HoKhau,
                    dayIn = item.NgayVao.HasValue ? item.NgayVao.Value.ToShortDateString() : "",
                    dayOut = item.NgayRa.HasValue ? item.NgayRa.Value.ToShortDateString() : "",
                    roomNo = item.SoPhong,
                    lockerNo = item.SoHocTuDo,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Ký túc xá");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "BÁO CÁO KÝ TÚC XÁ - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:K1"].Merge = true;
                ws.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Họ và tên tiếng Việt";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Họ và tên tiếng Nhật";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Giới tính";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày tháng năm sinh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa chỉ liên lạc";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Quê quán";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày vào";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày ra";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Số phòng";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Số hộc tủ đồ";
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
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.ten;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.japName;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.sex;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.birthdate;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.address;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.home;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Style.Numberformat.Format = "dd-mm-yyyy";
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.dayIn;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Style.Numberformat.Format = "dd-mm-yyyy";
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.dayOut;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.roomNo;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.lockerNo;

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

                var fileDownloadName = "Báo cáo Ký túc xá - " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion


    }
}