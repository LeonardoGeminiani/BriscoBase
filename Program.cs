#define INSERT

using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
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
using (var db = new UserContext())
{
    string[] opts = { "Crea", "Modifica", "Elimina", "Visualizza utenti" };
    switch (ConsoleHelper.MultipleChoice(false, opts, "Cosa Vuoi Fare?"))
    {
        case 0:
            {
                Console.Clear();
                User u = new User();
                Console.WriteLine("Inserisci Nickname: ");
                u.NickName = Console.ReadLine();
                Console.WriteLine("Inserisci Password: ");
                u.Password = Console.ReadLine();
                uint temp;
                do
                {
                    Console.WriteLine("Inserisci il numero di partite giocate: ");
                } while (!UInt32.TryParse(Console.ReadLine(), out temp));
                u.GamesPlayed = temp;
                do
                {
                    Console.WriteLine("Inserisci i punti totali: ");
                } while (!UInt32.TryParse(Console.ReadLine(), out temp));
                u.Points = temp;
                do
                {
                    Console.WriteLine("Inserisci l'SSO: ");
                } while (!UInt32.TryParse(Console.ReadLine(), out temp));
                u.SSO = temp;
                db.Add(u);

            }
            break;
        case 1:
            {
                Console.Clear();
                Console.WriteLine("Che Nickname vuoi modificare?");
                var nick = Console.ReadLine()!;

                var qry = db.Users.Where(u => u.NickName.Equals(nick)).ToList();
                if (qry.Count() == 1)
                {
                    var user = qry[0];
                    var tmp = ModifieUser(user);
                    user.NickName = tmp.NickName;
                    user.Password = tmp.Password;
                    user.GamesPlayed = tmp.GamesPlayed;
                    user.Points = tmp.Points;
                    user.SSO = tmp.SSO;
                }
            }
            break;
        case 2:
            {
                Console.Clear();
                Console.WriteLine("Che Nickname vuoi rimuovere?");
                var nick = Console.ReadLine()!;

                var qry = db.Users.Where(u => u.NickName.Equals(nick)).ToList();
                if (qry.Count() > 0)
                {
                    foreach (var user in qry) db.Remove(user);
                }
            }
            break;
        case 3:
            {
                var temp = db.Users.ToArray();
                ConsoleHelper.MultipleChoice(false, temp.ToStringArray(), "Visualizza gli utenti");
            }
            break;

    }
    db.SaveChanges();

}

#endif
User ModifieUser(User old)
{
    User ret = new();
    Console.WriteLine("vuoi modificare il NickName? attualmente è: " + old.NickName);
    if (ConsoleHelper.Proceed())
        ret.NickName = Console.ReadLine();
    else ret.NickName = old.NickName;
    Console.WriteLine("vuoi modificare la password? attualmente è: " + old.Password);
    if (ConsoleHelper.Proceed())
        ret.Password = Console.ReadLine();
    else ret.Password = old.Password;

    uint npt = 0;
    bool exit = false;
    do
    {
        Console.WriteLine("vuoi modificare il numero di partite giocate? attualmente è: " + old.GamesPlayed);
        if (exit = !ConsoleHelper.Proceed()) break;
    } while (!UInt32.TryParse(Console.ReadLine(), out npt));
    if (!exit) ret.GamesPlayed = npt;
    else ret.GamesPlayed = old.GamesPlayed;

    exit = false;
    do
    {
        Console.WriteLine("vuoi modificare il numero di punti? attualmente è: " + old.Points);
        if (exit = !ConsoleHelper.Proceed()) break;
    } while (!UInt32.TryParse(Console.ReadLine(), out npt));
    if (!exit) ret.Points = npt;
    else ret.Points = old.Points;

    exit = false;
    do
    {
        Console.WriteLine("vuoi modificare l'SSO? attualmente è: " + old.SSO);
        if (exit = !ConsoleHelper.Proceed()) break;
    } while (!UInt32.TryParse(Console.ReadLine(), out npt));
    if (!exit) ret.SSO = npt;
    else ret.SSO = old.SSO;

    exit = false;
    do
    {
        Console.WriteLine("vuoi modificare il ClanId? attualmente è: " + old.ClanId);
        if (exit = !ConsoleHelper.Proceed()) break;
    } while (!UInt32.TryParse(Console.ReadLine(), out npt));
    if (!exit) ret.ClanId = npt;
    else ret.ClanId = old.ClanId;

    return ret;
}