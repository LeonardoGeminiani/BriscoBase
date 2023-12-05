#nullable disable

public class User
{
    public uint UserId { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UInt32 GamePlayed { get; set; }
    public UInt32 Points { get; set; }
    public UInt32 SSO { get; set; }
    public Clan Clan { get; set; }

    public override string ToString()
    {
        return string.Format(
            "User:{0,-3} Nick:{1,-20} Email:{2,-30} Pw:{3,-18} SSO:{4,-10} GamePlayed:{5,-3} Points:{6,-6}",
            UserId, NickName, Email, Password, SSO, GamePlayed, Points
        );
        // return $"User:{ UserId } Nick:{ NickName } Email:{ Email } Pw:{ Password } SSO:{ SSO } GamePlayed:{ GamePlayed } Points:{ Points }";
    }
}