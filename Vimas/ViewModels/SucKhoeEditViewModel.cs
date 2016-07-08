using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vimas.Models;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class SucKhoeEditViewModel:SucKhoeViewModel
    {
        public SucKhoeEditViewModel() : base() { }

        public SucKhoeEditViewModel(SucKhoe entity) : base(entity) { }

        public SucKhoeEditViewModel(ThongTinCaNhanViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }


        #region Basic Info
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Nhóm máu chỉ có 2 chữ cái.")]
        public override string NhomMau
        {
            get
            {
                return base.NhomMau;
            }

            set
            {
                base.NhomMau = value;
            }
        }
        [IsNumeric(ErrorMessage = "Thị lực mắt trái phải là số")]
        [Range(1, 10, ErrorMessage = "Thị lực mắt trái trong khoảng từ 1 - 10")]
        public override int? ThiLucMatTrai
        {
            get
            {
                return base.ThiLucMatTrai;
            }

            set
            {
                base.ThiLucMatTrai = value;
            }
        }
        [IsNumeric(ErrorMessage = "Thị lực mắt phải phải là số")]
        [Range(1, 10, ErrorMessage = "Thị lực mắt phải trong khoảng 1 - 10")]
        public override int? ThiLucMatPhai
        {
            get
            {
                return base.ThiLucMatPhai;
            }

            set
            {
                base.ThiLucMatPhai = value;
            }
        }
        #endregion

        #region Additional Info
        public string Name { get; set; }
        public IEnumerable<SelectListItem> AvailableIDs { get; set; } 
        #endregion
    }
}