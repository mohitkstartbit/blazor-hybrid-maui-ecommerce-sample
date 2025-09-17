using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeggieApp.Model.Model.Authentication
{
    public class UserWithRoles
    {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public IList<string> Roles { get; set; }
        
    }
}
