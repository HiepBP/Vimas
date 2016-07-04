using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vimas.Models;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class QuaTrinhHocTapEditViewModel : QuaTrinhHocTapViewModel
    {
        public QuaTrinhHocTapEditViewModel() : base() { }

        public QuaTrinhHocTapEditViewModel(QuaTrinhHocTap entity) : base(entity) { }

        public QuaTrinhHocTapEditViewModel(QuaTrinhHocTapViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
        public EducationLevel EducationLevel { get; set; }
    }
}