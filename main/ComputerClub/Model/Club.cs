using System;
using System.Collections.Generic;

namespace ComputerClub.Model;

public partial class Club
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ClubLogin { get; set; }

    public double Balance { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public override string ToString()
    {
        return $"{Name}";
    }
}
