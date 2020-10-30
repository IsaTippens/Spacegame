using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Numerics;

namespace Spacegame
{
    public abstract class Game : IDisposable
    {

        public Color clearColor;
        public Camera2D camera;

        public Vector2 ScreenDimensions;
        public int ScreenWidth {
            get => (int)ScreenDimensions.X;
            set  {
                SetWindowSize(value, ScreenHeight);
                ScreenDimensions.X = value;
            }
            
        }

        public int ScreenHeight {
            get => (int)ScreenDimensions.Y;
            set  {
                SetWindowSize(ScreenWidth, value);
                ScreenDimensions.Y = value;
            }
        }

        public Game() : this(800, 480, "Hello World")
        {
            
        }

        public Game(int screenWidth, int screenHeight) : this(screenWidth, screenHeight, "")
        {
        
        }
        public Game(int screenWidth, int screenHeight, string title)
        {
            
            InitWindow(screenWidth, screenHeight, title);
            ScreenDimensions = new Vector2(screenWidth, screenHeight);
            clearColor = Color.RAYWHITE;
            Init();
        }

        public virtual void Init() {

        }
        public void Run()
        {
            //ToggleFullscreen();
            while (!WindowShouldClose())
            {
                Update();
                
                BeginDrawing();
                ClearBackground(clearColor);
                BeginMode2D(camera);
                Draw();
                EndMode2D();
                EndDrawing();
            }

            CloseWindow();
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

        public void Dispose() {
            GC.SuppressFinalize(this);
        }

    }
}