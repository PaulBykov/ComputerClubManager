using System.ComponentModel;

namespace ComputerClub.Services
{
    public static class RoleIdentityService
    {
        private static string _userRole = AuthService.GetInstance().CurrentUser.Role.ToLower();


        static RoleIdentityService()
        {
            AuthService.GetInstance().PropertyChanged += OnPropertyChanged;
        }


        public static bool IsAdmin()
        {
            return _userRole.Equals("admin");
        }   

        public static bool IsOwner()
        {
            return _userRole.Equals("owner");
        }

        public static bool IsAdmin(string roleName) 
        {
            return roleName.ToLower().Equals("admin");
        }

        public static bool IsOwner(string roleName)
        {
            return roleName.ToLower().Equals("owner");
        }



        private static void RevalidateUserRole()
        {
            _userRole = AuthService.GetInstance().CurrentUser?.Role.ToLower();
        }

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RevalidateUserRole();
        }
    }
}
