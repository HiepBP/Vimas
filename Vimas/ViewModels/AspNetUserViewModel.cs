using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.ViewModels
{
    public partial class AspNetUsersViewModel
    {
        public IEnumerable<AspNetRolesViewModel> AspNetRoles { get; set; }
    }
}