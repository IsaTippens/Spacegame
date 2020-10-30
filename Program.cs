namespace Spacegame
{
    static class Program
    {
        const int screenWidth = 1366;
        const int screenHeight = 768;
        public static void Main()
        {
            using (var game = new TestGame()) {
                game.Run();
            }
        }

    }
}