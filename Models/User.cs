
public class User
{
    public uint UserId { get; set; }
    public string NickName { get; set; }
    public string Password { get; set; }
    public string? LogoPath { get; set; }
    public UInt32 GamesPlayed { get; set; }
    public UInt32 Points { get; set; }
    public UInt32 SSO { get; set; }
    public uint? ClanId { get; set; }
    public Clan? Clan { get; set; }

    public override string ToString()
    {
        return string.Format(
            "User:{0,-3} Nick:{1,-20} Pw:{2,-18} SSO:{3,-10} GamePlayed:{4,-3} Points:{5,-6} ClanId:{6,-6}",
            UserId, NickName, Password, SSO, GamesPlayed, Points, ClanId
        );
        // return $"User:{ UserId } Nick:{ NickName } Email:{ Email } Pw:{ Password } SSO:{ SSO } GamePlayed:{ GamePlayed } Points:{ Points }";
    }
}