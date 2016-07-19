using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class ThongTinCaNhanController : BaseController
    {
        // GET: HocVien/ThongTinCaNhan
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDanhSachThongTin(JQueryDataTableParamModel param)
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var listThongTinCaNhan = thongTinCaNhanService.GetActive().ProjectTo<ThongTinCaNhanViewModel>(this.MapperConfig).ToList();
            try
            {
                var rs = listThongTinCaNhan
                    .Where(q => string.IsNullOrEmpty(param.sSearch)
                        || q.HoTen.ToLower().Contains(param.sSearch.ToLower()))
                    .OrderBy(q => q.HoTen)
                    .Skip(param.iDisplayStart)
                    .Take(param.iDisplayLength)
                    .Select(q => new IConvertible[]
                    {
                        q.HoTen,
                        q.GioiTinh == 0 ? "Nữ":"Nam",
                        q.NgaySinh.ToShortDateString(),
                        q.CMND,
                        q.DienThoaiDiDong,
                        q.Id,
                    });
                var totalRecords = listThongTinCaNhan.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = listThongTinCaNhan.Count,
                    iTotalDisplayRecords = totalRecords,
                    aaData = rs
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error" });
            }
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        public ActionResult Create()
        {
            var trungTamGTVLService = this.Service<ITrungTamGTVLService>();
            var model = new ThongTinCaNhanEditViewModel();
            model.Gender = (Gender)1;
            model.FamilyStatus = (FamilyStatus)model.TinhTrangGiaDinh;
            model.EducationLevel = (EducationLevel)(model.TrinhDoVanHoa != null ? model.TrinhDoVanHoa : 0);
            model.AvailableMaNguon = trungTamGTVLService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenCoSo,
                Value = q.Id.ToString(),
                Selected = false,
            });
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ThongTinCaNhanEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                model.Active = true;
                model.GioiTinh = (int)model.Gender;
                model.TrinhDoVanHoa = (int)model.EducationLevel;
                model.TinhTrangGiaDinh = (int)model.FamilyStatus;

                var entity = model.ToEntity();
                await thongTinCaNhanService.CreateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Tạo", controllerName, entity.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, PhongNguon")]
        public async Task<ActionResult> Edit(int id)
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var trungTamGTVLService = this.Service<ITrungTamGTVLService>();
            var model = new ThongTinCaNhanEditViewModel(await thongTinCaNhanService.GetAsync(id));
            model.Gender = (Gender)model.GioiTinh;
            model.FamilyStatus = (FamilyStatus)model.TinhTrangGiaDinh;
            model.EducationLevel = (EducationLevel)(model.TrinhDoVanHoa != null ? model.TrinhDoVanHoa : 0);
            model.AvailableMaNguon = trungTamGTVLService.GetActive().Select(q => new SelectListItem()
            {
                Text = q.TenCoSo,
                Value = q.Id.ToString(),
                Selected = (q.Id == model.IdTrungTamGTVL),
            });
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ThongTinCaNhanEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var entity = await thongTinCaNhanService.GetAsync(model.Id);
            model.CopyToEntity(entity);
            entity.Active = true;
            entity.GioiTinh = (int)model.Gender;
            entity.TrinhDoVanHoa = (int)model.EducationLevel;
            entity.TinhTrangGiaDinh = (int)model.FamilyStatus;

            await thongTinCaNhanService.UpdateAsync(entity);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            var result = await new SystemLogController().Create("Sửa", controllerName, entity.Id);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Detail(int id)
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var model = new ThongTinCaNhanViewModel(await thongTinCaNhanService.GetAsync(id));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PhongNguon")]
        public async System.Threading.Tasks.Task<JsonResult> Delete(int id)
        {
            try
            {
                var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
                var entity = await thongTinCaNhanService.GetAsync(id);
                if (entity == null || entity.Active == false)
                {
                    return Json(new { success = false, message = Resource.ErrorMessage });
                }
                //entity.Active = false;
                await thongTinCaNhanService.DeactivateAsync(entity);
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var result = await new SystemLogController().Create("Xóa", controllerName, entity.Id);

                return Json(new { success = false, message = "Xóa thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = Resource.ErrorMessage });
            }
        }

        public JsonResult LoadThongTinCaNhanDDL(string searchTerm, int pageSize, int pageNumber)
        {
            var thongTinCaNhanService = this.Service<IThongTinCaNhanService>();
            var select2pagedResult = new Select2PagedResult();
            #region GetData
            string cacheKey = "Select2Options";
            var listThongTinCaNhan = new List<Select2OptionModel>();
            //check cache
            if (System.Web.HttpContext.Current.Cache[cacheKey] != null)
            {
                listThongTinCaNhan = (List<Select2OptionModel>)System.Web.HttpContext.Current.Cache[cacheKey];
            }
            else
            {
                listThongTinCaNhan = thongTinCaNhanService.GetActive()
                    .AsEnumerable()
                    .Select(q => new Select2OptionModel()
                    {
                        id = q.Id.ToString(),
                        text = q.HoTen + " - " + Utils.FormatDate(q.NgaySinh),
                    })
                    .ToList();
                //cache results
                System.Web.HttpContext.Current.Cache[cacheKey] = listThongTinCaNhan;
            }
            #endregion
            var totalSearchRecords = listThongTinCaNhan.Count;
            select2pagedResult.Results = listThongTinCaNhan.Where(q => string.IsNullOrEmpty(searchTerm)
                                                                    || q.text.ToLower().Contains(searchTerm.ToLower()));
            select2pagedResult.Total = select2pagedResult.Results.Count();
            select2pagedResult.Results = select2pagedResult.Results.Skip((pageNumber - 1) * pageSize)
                                                                    .Take(pageSize);
            return Json(select2pagedResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByIdDOLAB(int idDOLAB)
        {
            var hopDongDOLABHocVienMappingService = this.Service<IHopDongDOLABHocVienMappingService>();
            var count = 0;
            List<dynamic> listDt = new List<dynamic>();
            var providers = hopDongDOLABHocVienMappingService.GetByIdHopDongDOLAB(idDOLAB)
                .AsEnumerable()
                .Select(q => new
                {
                    No = ++count,
                    HoTen = q.ThongTinCaNhan.HoTen,
                    Id = q.Id,
                });
            return Json(new { data = providers.ToList() }, JsonRequestBehavior.AllowGet);
        }

        #region Export Excel
        public List<dynamic> GetDataList()
        {
            var TTCNService = this.Service<IThongTinCaNhanService>();
            var GTVLService = this.Service<ITrungTamGTVLService>();

            var resultLst = TTCNService.Get()
                .Where(q => q.Active == true)
                .ProjectTo<ThongTinCaNhanEditViewModel>(this.MapperConfig).ToList();

            foreach (var item in resultLst)
            {
                item.TenTrungTam = GTVLService.Get(item.IdTrungTamGTVL).TenCoSo;
            }

            var dynamicList = new List<dynamic>();
            var count = 0;
            foreach (var item in resultLst)
            {
                dynamicList.Add(new
                {
                    stt = ++count,
                    code = item.MaLuuHoSo,
                    name = item.HoTen,
                    japName = item.TenPhienAmNhat,
                    sex = ((Gender)item.GioiTinh).GetAttribute<DisplayAttribute>().Name,
                    birth = item.NgaySinh.ToShortDateString(),
                    birthPlace = item.NoiSinh,
                    trinhDo = ((EducationLevel)(item.TrinhDoVanHoa.HasValue ? item.TrinhDoVanHoa.Value : 6)).GetAttribute<DisplayAttribute>().Name,
                    family = ((FamilyStatus)item.TinhTrangGiaDinh).GetAttribute<DisplayAttribute>().Name,
                    cmnd = item.CMND,
                    ngayCap = item.NgayCap,
                    noiCap = item.NoiCap,
                    hoChieu = item.SoHoChieu,
                    ngayCapHC = item.NgayCapHC.HasValue ? item.NgayCapHC.Value.ToShortDateString() : "",
                    ngayHetHan = item.NgayHetHan.HasValue ? item.NgayHetHan.Value.ToShortDateString() : "",
                    noiCapHC = item.NoiCapHC,
                    address = item.DiaChiTiengAnh,
                    diaChi = item.DiaChiLienLac,
                    phone = item.DienThoaiNha,
                    mobile = item.DienThoaiDiDong,
                    shoeSize = item.CoGiay,
                    shirtSize = item.SizeQuanAo,
                    trungtam = item.TenTrungTam,
                });
            }

            return dynamicList;
        }
        public ActionResult ExportExcel()
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(ms))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Thông tin cá nhân");
                char StartHeaderChar = 'A';
                int StartHeaderNumber = 2;
                var listDT = GetDataList();
                #region Headers

                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Bold = true;
                ws.Cells["" + (StartHeaderChar) + (1)].Style.Font.Size = 16;
                ws.Cells["" + (StartHeaderChar) + (1)].Value = "DANH SÁCH THÔNG TIN CÁ NHÂN - Ngày " + DateTime.Now.ToShortDateString();
                ws.Cells["A1:W1"].Merge = true;
                ws.Cells["A1:W1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                StartHeaderChar = 'A';
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "STT";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Mã Lưu Hồ Sơ";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Họ và Tên";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Họ và tên tiếng Nhật";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Giới tính ";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày sinh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Nơi Sinh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Trình độ văn hóa";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Tình trạng gia đình";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "CMND";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày cấp";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Nơi cấp CMND";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Số hộ chiếu";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày cấp hộ chiếu ";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Ngày hết hạn";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Nơi cấp hộ chiếu";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa chỉ tiếng anh";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Địa chỉ liên lạc";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Điện thoại nhà";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Điện thoại di động";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Cỡ giày";
                ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = "Size quần áo";
                ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = "Thuộc trung tâm GTVL";
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
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.code;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.name;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.japName;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.sex;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.birth;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.birthPlace;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.trinhDo;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.family;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.cmnd;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.ngayCap;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.noiCap;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.hoChieu;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.ngayCapHC;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.ngayHetHan;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.noiCapHC;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.address;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.diaChi;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.phone;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.mobile;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.shoeSize;
                    ws.Cells["" + (StartHeaderChar++) + (StartHeaderNumber)].Value = data.shirtSize;
                    ws.Cells["" + (StartHeaderChar) + (StartHeaderNumber)].Value = data.trungtam;

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

                var fileDownloadName = "Danh sách thông tin cá nhân - " + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return this.File(ms, contentType, fileDownloadName);
            }
        }
        #endregion

    }
}