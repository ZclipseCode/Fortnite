public class Fortnite
{
    public static void Main()
    {
        using (Game game = new Game(500, 500))
        {
            game.Run();
        }
    }
}