using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.ViewModels
{
    public partial class AspNetUserViewModel
    {
        public IEnumerable<AspNetRoleViewModel> AspNetRoles { get; set; }
    }
}