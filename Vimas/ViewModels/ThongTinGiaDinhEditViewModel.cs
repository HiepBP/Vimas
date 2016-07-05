using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vimas.Models;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class ThongTinGiaDinhEditViewModel : ThongTinGiaDinhViewModel
    {
        public ThongTinGiaDinhEditViewModel() : base() { }

        public ThongTinGiaDinhEditViewModel(ThongTinGiaDinh entity) : base(entity) { }

        public ThongTinGiaDinhEditViewModel(QuaTrinhHocTapViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }

        public Relation Relation { get; set; }
    }
}