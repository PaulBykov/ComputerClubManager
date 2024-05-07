using System;
using System.Collections.Generic;

namespace ComputerClub.Model;

public partial class Staff
{
    public int Id { get; set; }

    public string Fullname { get; set; }

    public string PassHash { get; set; }

    public string Role { get; set; }

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();

    public override string ToString()
    {
        return $"{Fullname}";
    }
}
