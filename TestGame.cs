using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Spacegame.Universe;
using Spacegame.Systems;

namespace Spacegame
{
    public class TestGame : Game
    {
        StarGrid grid;
        StarGridSystem sgs;

        private static TestGame _instance
        {
            get;
            set;
        }

        public static TestGame instance {
            get => _instance;
        }

        public TestGame(int screenWidth = 1280, int screenHeight = 720) : base(screenWidth, screenHeight)
        {
            _instance = this;
            SetTargetFPS(60);
        }

        public override void Init()
        {
            base.Init();
            clearColor = Color.BLACK;
            grid = new StarGrid();
            grid.ChunkSize = 200;
            camera = new Camera2D(
                new Vector2(ScreenWidth / 2,  ScreenHeight / 2),
                new Vector2(0, 0),
                0f,
                1f
            );
            sgs = new StarGridSystem(grid);
            sgs.camera = camera;
        }

        public override void Update()
        {
            

            if (IsKeyPressed(KeyboardKey.KEY_Q))
                grid.GenerateChunk(new Vector2(1, 1));

            Vector2 delta = Vector2.Zero;
            if (IsKeyDown(KeyboardKey.KEY_W))
                delta.Y -= 1;
            if (IsKeyDown(KeyboardKey.KEY_S))
                delta.Y += 1;
            if (IsKeyDown(KeyboardKey.KEY_A))
                delta.X -= 1;
            if (IsKeyDown(KeyboardKey.KEY_D))
                delta.X += 1;
        
            camera.target += delta * 1000f * GetFrameTime();
            sgs.camera = camera;
            

            sgs.Update();
            base.Update();
        }

        public override void Draw()
        {
            sgs.Draw();
            //sgs.DebugDraw();
            SetWindowTitle(GetFPS().ToString());
            base.Draw();
        }
    }
}