namespace ComputerClub.Model;

public partial class Session
{
    public int ClubId { get; set; }

    public string UserLogin { get; set; }

    public virtual Club Club { get; set; }

    public virtual User UserLoginNavigation { get; set; }
}
