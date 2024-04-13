public class Fortnite
{
    public static void Main()
    {
        using (Game game = new Game(1280, 720))
        {
            game.Run();
        }
    }
}