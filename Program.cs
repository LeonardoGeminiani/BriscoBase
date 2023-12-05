#define INSERT

using SQLitePCL;

#if CSV
using (var db = new UserContext())
{
    using (var reader = new StreamReader("./Clan.csv"))
    {
        _ = reader.ReadLine();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line!.Split(',');

            db.Add(new Clan()
            {
                Name = values[0],
                Bio = values[1]
            });
        }
    }

    using (var reader = new StreamReader("./Users.csv"))
    {
        _ = reader.ReadLine();
        int i = 0;//Contatori per assegnare ad ogni user un clan
        uint j = 0;
        while (!reader.EndOfStream)
        {
            if(i%3 == 0)//Ogni multiplo di 3, j incrementa e si passa al prossimo clan
                j++;
            var line = reader.ReadLine();
            var values = line!.Split(',');

            db.Add(new User()
            {
                NickName = values[0],
                Password = values[2],
                GamesPlayed = Convert.ToUInt32(values[3]),
                Points = Convert.ToUInt32(values[4]),
                SSO = Convert.ToUInt32(values[5]),
                ClanId = j
            });
            i++;
        }
    }

    db.SaveChanges();
}
#elif INSERT
using(var db = new UserContext())
{
    Console.WriteLine("Che Nickname vuoi rimuovere?");
    var nick = Console.ReadLine()!;
    
    var qry = db.Users.Where(u => u.NickName.Equals(nick)).ToList();
    if(qry.Count() > 0){
        foreach(var user in qry) db.Remove(user);
    }
}

#endif