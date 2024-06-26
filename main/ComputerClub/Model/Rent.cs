﻿using System;

namespace ComputerClub.Model;

public partial class Rent
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public TimeOnly Length { get; set; }

    public int ComputerId { get; set; }

    public virtual Computer Computer { get; set; }

    public override string ToString()
    {
        return $"{Id} начало {StartTime.ToString(@"HH\\mm\\ss")}";
    }
}
