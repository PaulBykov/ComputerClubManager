using System;
using System.Collections.Generic;

namespace ComputerClub.Model;

public partial class Rate
{
    public string Name { get; set; }

    public double Price { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    public override string ToString()
    {
        return Name;
    }
}   
