using static Spacegame.Utils;
namespace Spacegame
{
    static class Program
    {
        public static void Main()
        {
            using (var game = new TestGame(screenWidth, screenHeight)) {
                game.Run();
            }
        }

    }
}