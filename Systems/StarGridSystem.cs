using Spacegame.Universe;
using static Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;
using static Spacegame.Utils;
using System;

namespace Spacegame.Systems
{
    public class StarGridSystem : GameObject
    {
        private Texture2D star;
        private StarGrid _sg;

        public StarGrid StarGrid
        {
            get => _sg;
        }

        public Camera2D camera;

        public Vector2 cameraTarget
        {
            get => camera.target;
        }
        public StarGridSystem() : this(new StarGrid())
        {

        }

        public StarGridSystem(StarGrid starGrid)
        {
            _sg = starGrid;
            LoadTexture();
        }

        public void LoadTexture()
        {
            Image img = LoadImage("Resources/Images/Star.png");
            ImageResize(ref img, 10, 10);

            star = LoadTextureFromImage(img);
            UnloadImage(img);
        }
        public override void Update()
        {
            Vector2 center = PositionToSector(cameraTarget);
            int xBlocks = (int)MathF.Ceiling(screenWidth / _sg.ChunkSize) + 1;
            int yBlocks = (int)MathF.Ceiling(screenHeight / _sg.ChunkSize) + 1;

            for (int y = -1; y < yBlocks + 1; y++)
            {
                for (int x = -1; x < xBlocks + 1; x++)
                {
                    Vector2 sector = new Vector2(
                        center.X - (xBlocks / 2) + x,
                        center.Y - (yBlocks / 2) + y
                    );
                    if (!_sg.IsSectorGenerated(sector))
                    {
                        _sg.GenerateChunk(sector);
                    }
                    else
                    {
                        Star[] chunk = _sg.GetChunk(sector);
                        foreach (var star in chunk)
                        {
                            star.Update();
                        }
                    }
                }
            }

            _sg.Update();
            base.Update();
        }

        public override void Draw()
        {
            Vector2 center = PositionToSector(cameraTarget);
            int cs = _sg.ChunkSize;
            int xBlocks = (int)MathF.Ceiling(screenWidth / cs) + 1;
            int yBlocks = (int)MathF.Ceiling(screenHeight / cs) + 1;

            for (int y = -1; y < yBlocks + 1; y++)
            {
                for (int x = -1; x < xBlocks + 1; x++)
                {
                    Vector2 sector = new Vector2(
                        center.X - (xBlocks / 2) + x,
                        center.Y - (yBlocks / 2) + y
                    );
                    Star[] chunk = _sg.GetChunk(sector);
                    foreach (var star in chunk)
                    {
                        star.Draw();
                        //star.DebugDraw();
                    }
                }
            }
            _sg.Draw();
            base.Draw();
        }

        public override void DebugDraw()
        {
            _sg.DebugDraw();
            base.DebugDraw();
        }


        //Move these to a global static class
        //mv
        bool isOnScreen(Vector2 sector)
        {
            int chunkSize = _sg.ChunkSize;
            int ScreenWidth = Utils.screenWidth;
            int ScreenHeight = Utils.screenHeight;
            Vector2 camPos = cameraTarget;
            Vector2 sectorPos = sector * chunkSize;
            return (sectorPos.X < camPos.X + ScreenWidth / 2 + chunkSize) &&
                    (sectorPos.X > camPos.X - ScreenWidth / 2 - chunkSize) &&
                    (sectorPos.Y < camPos.Y + ScreenHeight / 2 + chunkSize) &&
                    (sectorPos.Y > camPos.Y - ScreenHeight / 2 - chunkSize);

        }

        //mv
        Vector2 PositionToSector(Vector2 position)
        {
            int cs = _sg.ChunkSize;
            return new Vector2(MathF.Floor(position.X / cs), MathF.Floor(position.Y / cs));
        }


    }
}