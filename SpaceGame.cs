using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Numerics;

using System.Collections.Generic;


namespace Spacegame
{
    public class SpaceGame : Game
    {
        Dictionary<Vector2, Vector2[]> futureStarChuncks;
        Dictionary<Vector2, Vector2[]> starChuncks;

        int chunkSize;
        Random randy;
        const float speed = 200f;
        Vector2[] stars;
        SpaceShip ship;

        int starMode = 0;

        float starLength;

        Texture2D starGraphic;
        Color[] colors = new[] {
            Color.RED,
            Color.ORANGE,
            Color.YELLOW,
            Color.GREEN,
            Color.BLUE,
            Color.PINK,
            Color.PURPLE
        };
        public SpaceGame() : this(1280, 720)
        {

        }

        public SpaceGame(int screenWidth, int screenHeight) : base(screenWidth, screenHeight, "title")
        {

        }

        public override void Init()
        {
            starLength = 30f;
            starChuncks = new Dictionary<Vector2, Vector2[]>();
            futureStarChuncks = new Dictionary<Vector2, Vector2[]>();
            chunkSize = 150;
            ship = new SpaceShip(new Vector2(0, 0), 0f);
            camera = new Camera2D(
                new Vector2(ScreenWidth / 2, ScreenHeight / 2),
                ship.Position,
                0f,
                1f
            );
            clearColor = Color.BLACK;
            randy = new Random();
            Image img = LoadImage("Resources/Images/Star.png");
            ImageResize(ref img, 10, 10);

            starGraphic = LoadTextureFromImage(img);
            UnloadImage(img);
            base.Init();
        }

        public override void Update()
        {
            Vector2 delta = new Vector2(0, 0);
            if (IsKeyDown(KeyboardKey.KEY_A))
                delta.X -= 1;
            if (IsKeyDown(KeyboardKey.KEY_D))
                delta.X += 1;
            if (IsKeyDown(KeyboardKey.KEY_W))
                delta.Y -= 1;
            if (IsKeyDown(KeyboardKey.KEY_S))
                delta.Y += 1;
            //delta.X += 20;

            if (IsKeyDown(KeyboardKey.KEY_Q))
                starLength -= 1f;
            if (IsKeyDown(KeyboardKey.KEY_E))
                starLength += 1f;

            if (IsKeyDown(KeyboardKey.KEY_T))
                ship.Rotation -= 5f * GetFrameTime();
            if (IsKeyDown(KeyboardKey.KEY_G))
                ship.Rotation += 5f * GetFrameTime();


            if (IsKeyDown(KeyboardKey.KEY_Z))
                camera.zoom -= 0.1f * GetFrameTime();
            if (IsKeyDown(KeyboardKey.KEY_X))
                camera.zoom += 0.1f * GetFrameTime();
            if (IsKeyDown(KeyboardKey.KEY_C))
                camera.zoom = 1f;

            if (IsKeyPressed(KeyboardKey.KEY_KP_0) || IsKeyPressed(KeyboardKey.KEY_ZERO))
                starMode = 0;
            if (IsKeyPressed(KeyboardKey.KEY_KP_1) || IsKeyPressed(KeyboardKey.KEY_ONE))
                starMode = 1;
            if (IsKeyPressed(KeyboardKey.KEY_KP_2) || IsKeyPressed(KeyboardKey.KEY_TWO))
                starMode = 2;
            if (IsKeyPressed(KeyboardKey.KEY_KP_3) || IsKeyPressed(KeyboardKey.KEY_THREE))
                starMode = 3;
            if (IsKeyPressed(KeyboardKey.KEY_KP_4) || IsKeyPressed(KeyboardKey.KEY_FOUR))
                starMode = 4;
            if (IsKeyPressed(KeyboardKey.KEY_KP_5) || IsKeyPressed(KeyboardKey.KEY_FIVE))
                starMode = 5;
            if (IsKeyPressed(KeyboardKey.KEY_KP_6) || IsKeyPressed(KeyboardKey.KEY_SIX))
                starMode = 6;
            if (IsKeyPressed(KeyboardKey.KEY_KP_7) || IsKeyPressed(KeyboardKey.KEY_SEVEN))
                starMode = 7;
            ship.Position += delta * speed * GetFrameTime();
            camera.target = ship.Position;
            ship.Update();

        }

        public override void Draw()
        {
            Vector2 sector = getCurrentChunk(ship.Position);
            generateAdjacentChunks(sector);
            foreach (var entry in starChuncks)
            {
                sector = entry.Key;
                if (isOnScreen(sector))
                {
                    stars = getChunkAtSector(sector);
                    for (int i = 0; i < stars.Length; i++)
                    {
                        DrawTexture(starGraphic, (int)stars[i].X - 5, (int)stars[i].Y - 5, Color.WHITE);
                        //DrawText("*", (int)stars[i].X, (int)stars[i].Y, 8, Color.WHITE);
                    }

                    Vector2[] adjacent = getAdjacentChunks(sector);
                    foreach (var adjSector in adjacent)
                    {
                        DrawStarLines(stars, adjSector, starMode);
                    }

                    //DrawRectangleLines((int)sector.X * chunkSize, (int)sector.Y * chunkSize, chunkSize, chunkSize, Color.WHITE);
                    //DrawText($"({sector.X},{sector.Y})", (int)sector.X * chunkSize + 2, (int)sector.Y * chunkSize + 2, 8, Color.WHITE);
                }
                //DrawRectangleLines((int)ship.Position.X - ScreenWidth / 2, (int)ship.Position.Y - ScreenHeight / 2, ScreenWidth, ScreenHeight, Color.WHITE);
                //DrawRectangleLines((int)ship.Position.X - ScreenWidth / 2 - chunkSize / 2, (int)ship.Position.Y - ScreenHeight / 2 - chunkSize / 2, ScreenWidth + chunkSize, ScreenHeight + chunkSize, Color.RED);

                //DrawRectangle((int)sector.X * chunkSize, (int)sector.Y * chunkSize,  16, 12, Color.BLACK);

            }

            addChunks();

            ship.Draw();
            //DrawText("Hello World", 100, 100, 20, Color.RAYWHITE);
            char[] keys = new[] {
                'W', 'D', 'S', 'A'
            };

            foreach (var key in keys)
            {
                Rectangle rect;
                Vector2 camPos = camera.target;
                Vector2 pos = new Vector2(0, 0);
                switch (key)
                {
                    case 'W':
                        pos = new Vector2(camPos.X, camPos.Y - ScreenHeight / 2 + 8);
                        break;
                    case 'A':
                        pos = new Vector2(camPos.X - ScreenWidth / 2 + 8, camPos.Y);
                        break;
                    case 'S':
                        pos = new Vector2(camPos.X, camPos.Y + ScreenHeight / 2 - 48);
                        break;
                    case 'D':
                        pos = new Vector2(camPos.X + ScreenWidth / 2 - 48, camPos.Y);
                        break;
                }
                DrawText($"{key}", (int)pos.X + 16, (int)pos.Y + 16, 8, Color.WHITE);
                rect = new Rectangle(pos.X, pos.Y, 40, 40);
                DrawRectangleRoundedLines(rect, 0.5f, 2, 2, Color.WHITE);
            }

            SetWindowTitle(GetFPS().ToString());
        }

        void addChunks()
        {
            foreach (var chunk in futureStarChuncks)
            {
                if (!starChuncks.ContainsKey(chunk.Key))
                    starChuncks.Add(chunk.Key, chunk.Value);
            }
        }

        bool isOnScreen(Vector2 sector)
        {
            Vector2 camPos = camera.target;
            Vector2 sectorPos = sector * chunkSize;
            return (sectorPos.X < camPos.X + ScreenWidth / 2 + chunkSize) &&
                    (sectorPos.X > camPos.X - ScreenWidth / 2 - chunkSize) &&
                    (sectorPos.Y < camPos.Y + ScreenHeight / 2 + chunkSize) &&
                    (sectorPos.Y > camPos.Y - ScreenHeight / 2 - chunkSize);

        }
        public Vector2 getCurrentChunk(Vector2 position)
        {
            return new Vector2(MathF.Floor(position.X / chunkSize), MathF.Floor(position.Y / chunkSize));
        }

        Vector2[] getChunkAtSector(Vector2 sector)
        {
            if (futureStarChuncks.ContainsKey(sector))
            {
                return futureStarChuncks[sector];
            }
            if (!starChuncks.ContainsKey(sector))
            {
                return generateChunk(sector);
            }
            return starChuncks[sector];
        }

        Vector2[] generateAdjacentChunks(Vector2 sector)
        {
            Vector2[] arr = new[] {
            sector + new Vector2(-1, 1),
            sector + new Vector2(-1, 1),
            sector + new Vector2(0, 1),
            sector + new Vector2(1, 1),
            sector + new Vector2(-1, 0),
            sector,
            sector + new Vector2(1, 0),
            sector + new Vector2(-1, -1),
            sector + new Vector2(0, -1),
            sector + new Vector2(1, -1)
            };

            for (int i = 0; i < arr.Length; i++)
            {
                getChunkAtSector(arr[i]);
            }
            return arr;
        }

        Vector2[] getAdjacentChunks(Vector2 sector)
        {
            return generateAdjacentChunks(sector);
        }

        Vector2[] generateChunk(Vector2 sector)
        {
            int width = (int)sector.X * chunkSize + chunkSize / 2;
            int height = (int)sector.Y * chunkSize + chunkSize / 2;
            Vector2[] chunk = new Vector2[randy.Next(1, 6)];
            for (int i = 0; i < chunk.Length; i++)
            {
                chunk[i] = new Vector2(randy.Next(width - chunkSize / 2, width + chunkSize / 2), randy.Next(height - chunkSize / 2, height + chunkSize / 2));
            }
            futureStarChuncks.Add(sector, chunk);
            return chunk;
        }

        void DrawSpaceShip(SpaceShip ship)
        {
            int h = 50;
            int w = 50;
            Vector2 top = new Vector2(MathF.Sin(ship.Rotation) * w, -MathF.Cos(ship.Rotation) * h);
            Vector2 left = new Vector2(MathF.Sin((3 * MathF.PI) / 4 + ship.Rotation) * w, -MathF.Cos((3 * MathF.PI) / 4 + ship.Rotation) * h);
            Vector2 right = new Vector2(MathF.Sin((5 * MathF.PI) / 4 + ship.Rotation) * w, -MathF.Cos((5 * MathF.PI) / 4 + ship.Rotation) * h);
            /*DrawTriangle(ship.Position + top,
                            ship.Position + right,
                            ship.Position + left,
                            Color.RED);*/
            DrawLineV(ship.Position + top,
                            ship.Position + right, Color.RED);
            DrawLineV(ship.Position + right,
                            ship.Position + left, Color.RED);
            DrawLineV(ship.Position + left,
                            ship.Position + top, Color.RED);
            DrawCircleLines((int)ship.Position.X, (int)ship.Position.Y, h, Color.RED);
        }

        void DrawStarLines(Vector2[] currStars, Vector2 adjSector, int mode)
        {
            foreach (var star in stars)
            {
                foreach (var adjStar in getChunkAtSector(adjSector))
                {
                    Vector2 v1 = star;
                    Vector2 v2 = adjStar;
                    //if (Vector2.Distance(v1, v2) < 36f)
                    float distance = Vector2.Distance(v1, v2);
                    if (starMode == 0)
                    {
                        if (distance < starLength)
                        {
                            Color color;
                            if (distance < 16f) color = colors[0];
                            else if (distance < 32f) color = colors[1];
                            else if (distance < 64f) color = colors[2];
                            else if (distance < 96f) color = colors[3];
                            else if (distance < 128f) color = colors[4];
                            else if (distance < 150f) color = colors[5];
                            else color = colors[6];
                            DrawLineV(v1, v2, color);
                        }
                    }
                    else if (starMode == 1)
                    {
                        if (distance < 16f)
                        {
                            DrawLineV(v1, v2, colors[0]);
                        }
                    }
                    else if (starMode == 2)
                    {
                        if (distance > 16f && distance < 32f)
                        {
                            DrawLineV(v1, v2, colors[1]);
                        }
                    }
                    else if (starMode == 3)
                    {
                        if (distance > 32f && distance < 64f)
                        {
                            DrawLineV(v1, v2, colors[2]);
                        }
                    }
                    else if (starMode == 4)
                    {
                        if (distance > 64f && distance < 96f)
                        {
                            DrawLineV(v1, v2, colors[3]);
                        }
                    }
                    else if (starMode == 5)
                    {
                        if (distance > 96f && distance < 128f)
                        {
                            DrawLineV(v1, v2, colors[4]);
                        }
                    }
                    else if (starMode == 6)
                    {
                        if (distance > 128f && distance < 150f)
                        {
                            DrawLineV(v1, v2, colors[5]);
                        }
                    }
                    else
                    {
                        if (distance > 150f)
                        {
                            DrawLineV(v1, v2, colors[6]);
                        }
                    }
                }
            }

        }
    }
}