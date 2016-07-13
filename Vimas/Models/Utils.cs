using AutoMapper;
using SkyWeb.DatVM.Data;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vimas.Models.Entities;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace Vimas.Models
{
    public static class Utils
    {
        public static bool HasRequiredAttribute(this PropertyInfo property)
        {
            return property.IsDefined(typeof(RequiredAttribute), true);
        }
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }

        public static MvcHtmlString RenderHtmlAttributes(KeyValuePair<string, string>[] values)
        {
            if (values == null)
            {
                return null;
            }

            var result = new StringBuilder();

            foreach (var value in values)
            {
                result.AppendFormat("{0}=\"{1}\"", value.Key, value.Value);
            }

            return new MvcHtmlString(result.ToString());
        }

        public static MvcHtmlString RenderHtmlAttributes(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            var result = new StringBuilder();

            foreach (var property in properties)
            {
                result.AppendFormat("{0}=\"{1}\"", property.Name, property.GetValue(obj));
            }

            return new MvcHtmlString(result.ToString());
        }

        public static void SetMessage(this Controller controller, string message)
        {
            controller.ViewData["Message"] = message;
        }

        public static string ToErrorsString(this DbEntityValidationException ex)
        {
            return string.Join(Environment.NewLine, ex.EntityValidationErrors.SelectMany(q => q.ValidationErrors.Select(p => p.ErrorMessage)));
        }

        public static TDest ToExactType<TSource, TDest>(this TSource source)
            where TDest : class, new()
        {
            var result = new TDest();
            DependencyUtils.Resolve<IMapper>().Map(source, result);

            return result;
        }

        public static DateTime GetEndOfDate(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
        }

        public static DateTime GetStartOfDate(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
        }

        /// <summary>
        /// using this method to get DateTime.Now
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentDateTime()
        {

            #region Get DateTime.Now
            //Get time UTC 
            DateTime utcNow = DateTime.UtcNow;
            //Parse UTC to time SE Asia
            DateTime datetimeNow = TimeZoneInfo.ConvertTime(utcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            #endregion

            return datetimeNow;
        }

        /// <summary>
        /// using this to convert string to dd/mm/yyyy
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string datetime)
        {
            return DateTime.ParseExact(datetime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static string FormatDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToShortDateString() : "";
        }

        public static string GetEnumDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static bool ExportToExcel(List<string> headers, IEnumerable<object> _list, string fileName)
        {
            // Khởi động chtr Excel
            COMExcel.Application exApp = new COMExcel.Application();

            // Thêm file temp xls
            COMExcel.Workbook exBook = exApp.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);

            // Lấy sheet 1.
            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];
            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, 1];

            // header.Add("#;1;2;r");
            // i represents for column
            int maxRow = 2;
            var col = 1;

            #region Add header
            for (int i = 0; i < headers.Count; i++)
            {
                var header = headers[i];
                string[] items = header.Split(';');

                var value = items[0];
                var row = Int32.Parse(items[1]);
                var range = Int32.Parse(items[2]);

                if (maxRow < row + 1)
                {
                    maxRow = row + 1;
                }

                r = (COMExcel.Range)exSheet.Cells[row, col];

                if (range < 2)
                {
                    r.Value2 = items[0];

                }
                else
                {
                    var type = items[3];

                    //merge column
                    if (type.Equals("c"))
                    {
                        var mergedCell = (COMExcel.Range)exSheet.Range[r, exSheet.Cells[row, col + range - 1]].Merge();
                        r.Value2 = value;
                        col--;
                    }
                    // merge row
                    else
                    {
                        var mergedCell = (COMExcel.Range)exSheet.Range[r, exSheet.Cells[range, col]].Merge();
                        r.Value2 = value;
                    }
                }

                col++;
            }
            #endregion

            //#region Add value to table
            var list = _list.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var type = item.GetType();
                PropertyInfo[] properties;

                properties = type.GetProperties();
                r = (COMExcel.Range)exSheet.Cells[maxRow + i, 1];
                r.Value2 = i + 1;
                for (int j = 0; j < properties.Length; j++)
                {
                    var property = properties[j];
                    r = (COMExcel.Range)exSheet.Cells[maxRow + i, j + 2];
                    r.Value2 = property.GetValue(item, null);
                }
            }
            //#endregion

            #region fit all column
            COMExcel.Range usedrange = exSheet.UsedRange; // detect all col were used (column whic has value)
            //usedrange.Column.autofit();
            usedrange.Columns.AutoFit();
            #endregion

            #region save file to local disk
            var issuccess = true;
            try
            {
                exBook.SaveAs(fileName, COMExcel.XlFileFormat.xlWorkbookNormal,
                             null, null, false, false,
                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                            false, false, false, false, false);


                //folderBrowserDialog1.ShowDialog();
                //System.Windows
                //if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK
                //    && saveFileDialog1.FileName.Length > 0)
                //{
                //    System.Windows.Input.
                //    richTextBox1.SaveFile(saveFileDialog1.FileName,
                //        RichTextBoxStreamType.PlainText);
                //}
            }
            catch (Exception e)
            {
                //message = e.message.tostring();
                issuccess = false;
            }
            finally
            {
                exApp.Quit();
            }

            return issuccess;
        }
        #endregion
        
    }
}