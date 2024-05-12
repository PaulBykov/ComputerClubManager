using System.Collections.Generic;

namespace ComputerClub.Model;

public partial class User
{
    public string Login { get; set; }

    public string PassHash { get; set; }

    public string Role { get; set; }

    public string Fullname { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();

    public override string ToString()
    {
        return $"{Fullname}";
    }

    public override bool Equals(object obj)
    {
        return obj is User user && user.Login.Equals(Login);
    }
}
