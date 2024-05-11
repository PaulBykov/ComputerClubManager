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
