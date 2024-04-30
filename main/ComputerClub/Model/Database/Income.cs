using System;
using System.Collections.Generic;

namespace ComputerClub.Model.Database;

public partial class Income
{
    public int Id { get; set; }

    public int ClubId { get; set; }

    public int Amount { get; set; }

    public virtual Club Club { get; set; }
}
