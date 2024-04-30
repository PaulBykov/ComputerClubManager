using System;
using System.Collections.Generic;

namespace ComputerClub.Model.Database;

public partial class Staff
{
    public int Id { get; set; }

    public string Fullname { get; set; }

    public int ClubId { get; set; }

    public string PassHash { get; set; }

    public string Role { get; set; }

    public virtual Club Club { get; set; }
}
