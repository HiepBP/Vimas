using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vimas.Models.Entities;

namespace Vimas.ViewModels
{
    public class BangCapEditViewModel : BangCapViewModel
    {
        public BangCapEditViewModel() : base() { }

        public BangCapEditViewModel(BangCap entity) : base(entity) { }

        public BangCapEditViewModel(HopDongDOLABViewModel original, IMapper mapper) : this()
        {
            mapper.Map(original, this);
        }
    }
}