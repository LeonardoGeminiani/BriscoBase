using(var db = new UserContext())
{
    using(var reader = new StreamReader("./Users.csv")){
        _ = reader.ReadLine();
        while (!reader.EndOfStream){
            var line = reader.ReadLine();
            var values = line!.Split(',');
            
            db.Add(new User(){
                NickName = values[0],
                Password = values[2],
                GamesPlayed = Convert.ToUInt32(values[3]),
                Points = Convert.ToUInt32(values[4]),
                SSO = Convert.ToUInt32(values[5])
            });
        }
    }

    db.SaveChanges();
}