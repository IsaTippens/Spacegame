using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Spacegame.Universe;

namespace Spacegame
{
    public class TestGame : Game
    {
        StarGrid grid;
        public TestGame(int screenWidth = 1280, int screenHeight = 720) : base(screenWidth, screenHeight)
        {

        }

        public override void Init()
        {
            base.Init();
            clearColor = Color.BLACK;
            grid = new StarGrid();
            grid.ChunkSize = 70;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x <= y; x++)
                {
                    grid.GenerateChunk(new Vector2(x, y));
                }
            }
            camera = new Camera2D(
                new Vector2(0, 0),
                new Vector2(0, 0),
                0f,
                1f
            );
        }

        public override void Update()
        {
            if (IsKeyPressed(KeyboardKey.KEY_Q))
                grid.GenerateChunk(new Vector2(1, 1));
            base.Update();
        }

        public override void Draw()
        {
            grid.Draw();
            grid.DebugDraw();
            base.Draw();
        }
    }
}