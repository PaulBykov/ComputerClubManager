namespace ComputerClub.Model;

public partial class Computer
{
    public int Id { get; set; }

    public int ClubId { get; set; }

    public string RateName { get; set; }

    public virtual Club Club { get; set; }

    public virtual Rate RateNameNavigation { get; set; }

    public virtual Rent Rent { get; set; }
}
