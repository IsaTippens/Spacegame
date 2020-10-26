namespace Spacegame
{
    static class Program
    {
        public static void Main()
        {
            using (var game = new SpaceGame()) {
                game.ScreenHeight = 768;
                game.ScreenWidth = 1366;
                game.Run();
            }
        }

    }
}