using System;
using System.Collections.Generic;

namespace ComputerClub.Model.Database;

public partial class Rate
{
    public string Name { get; set; }

    public int Price { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();
}
