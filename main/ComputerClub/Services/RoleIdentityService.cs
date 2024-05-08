using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerClub.Services
{
    public static class RoleIdentityService
    {
        public static bool IsAdmin(string roleName) 
        {
            return roleName.ToLower().Equals("admin");
        }

        public static bool IsOwner(string roleName)
        {
            return roleName.ToLower().Equals("owner");
        }
    }
}
