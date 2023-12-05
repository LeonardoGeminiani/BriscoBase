#nullable disable
public class Clan
{
    public uint ClanId { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string LogoPath { get; set; }
    public virtual ICollection<User> Users { get; set; } //virtual serve perchè users non viene mai realmente istanziato ma reagisce con EF tramite linq
}