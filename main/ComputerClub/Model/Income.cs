using System;
using System.Collections.Generic;

namespace ComputerClub.Model;

public partial class Income
{
    public int Id { get; set; }

    public int ClubId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly Date { get; set; }

    public string RateName { get; set; }

    public virtual Club Club { get; set; }
}
