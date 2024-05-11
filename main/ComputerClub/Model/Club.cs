using System.Collections.Generic;

namespace ComputerClub.Model;

public partial class Club
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Balance { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual Session Session { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public override string ToString()
    {
        return $"{Name}";
    }
}
