using System;
using System.Collections.Generic;

namespace ComputerClub.Model;

public partial class Rent
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public TimeOnly Length { get; set; }

    public int ComputerId { get; set; }

    public virtual Computer Computer { get; set; }
}
