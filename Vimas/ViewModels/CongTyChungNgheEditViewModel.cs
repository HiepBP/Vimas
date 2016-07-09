using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vimas.ViewModels
{
    [MetadataType(typeof(CongTyChungNgheViewModelMetaData))]
    public partial class CongTyChungNgheViewModel
    {

    }
    public class CongTyChungNgheViewModelMetaData
    {
        [Required(ErrorMessage = "Vui lòng nhập vào Tên viết tắt")]
        public virtual string TenVietTat { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào Tên tiếng Việt")]
        public virtual string TenTiengViet { get; set; }

        [IsNumeric(ErrorMessage = "Vui lòng chỉ nhập số")]
        public virtual Nullable<decimal> VonDieuLe { get; set; }

        [IsNumeric(ErrorMessage = "Vui lòng chỉ nhập số")]
        public virtual Nullable<int> SoNhanVien { get; set; }
    }
}