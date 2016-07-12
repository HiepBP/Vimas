using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
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
                await service.UpdateAsync(model.ToEntity());
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
                int StartHeaderNumber = 1;
                var listDT = GetDataList();
                #region Headers
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
                StartHeaderNumber = 1;
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
                #endregion
                package.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var fileDownloadName = "Báo Cáo Sức Khỏe.xlsx";
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
    }
}