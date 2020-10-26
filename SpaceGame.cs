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
        Color[] colors = new[] {
            Color.RED,
            Color.ORANGE,
            Color.YELLOW,
            Color.GREEN,
            Color.BLUE,
            Color.PINK,
            Color.PURPLE
        };
        public SpaceGame() : base()
        {
            starChuncks = new Dictionary<Vector2, Vector2[]>();
            futureStarChuncks = new Dictionary<Vector2, Vector2[]>();
            chunkSize = 100;
            ship = new SpaceShip(new Vector2(0, 0));
            camera = new Camera2D(
                new Vector2(ScreenWidth / 2, ScreenHeight / 2),
                ship.Position,
                0f,
                1f
            );
        }

        public override void Init()
        {
            clearColor = Color.BLACK;
            randy = new Random();
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

            ship.Position += delta * speed * GetFrameTime();
            camera.target = ship.Position;

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
                        DrawText("*", (int)stars[i].X, (int)stars[i].Y, 8, Color.WHITE);


                    }

                    Vector2[] adjacent = getAdjacentChunks(sector);
                    foreach (var adjSector in adjacent)
                    {
                        foreach (var star in stars)
                        {
                            foreach (var adjStar in getChunkAtSector(adjSector))
                            {
                                Vector2 v1 = star;
                                Vector2 v2 = adjStar;
                                //if (Vector2.Distance(v1, v2) < 36f)
                                float distance = Vector2.Distance(v1, v2);
                                Color color;
                                if (distance < 16f) color = colors[0];
                                else if(distance < 32f) color = colors[1];
                                else if(distance < 64f) color = colors[2];
                                else if(distance < 96f) color = colors[3];
                                else if(distance < 128f) color = colors[4];
                                else if(distance < 150f) color = colors[5];
                                else color = colors[6];
                                DrawLineV(v1, v2, color);
                            }
                        }
                    }
                    //DrawRectangleLines((int)sector.X * chunkSize, (int)sector.Y * chunkSize, chunkSize, chunkSize, Color.WHITE);
                    //DrawText($"({sector.X},{sector.Y})", (int)sector.X * chunkSize + 2, (int)sector.Y * chunkSize + 2, 8, Color.WHITE);
                }
                //DrawRectangleLines((int)ship.Position.X - ScreenWidth / 2, (int)ship.Position.Y - ScreenHeight / 2, ScreenWidth, ScreenHeight, Color.WHITE);
                //DrawRectangleLines((int)ship.Position.X - ScreenWidth / 2 - chunkSize / 2, (int)ship.Position.Y - ScreenHeight / 2 - chunkSize / 2, ScreenWidth + chunkSize, ScreenHeight + chunkSize, Color.RED);
                
                //DrawRectangle((int)sector.X * chunkSize, (int)sector.Y * chunkSize,  16, 12, Color.BLACK);
                
            }

            addChunks();

            DrawSpaceShip(ship);
            //DrawText("Hello World", 100, 100, 20, Color.RAYWHITE);
            char[] keys = new[] {
                'W', 'D', 'S', 'A'
            };

            foreach(var key in keys) 
            {
                Rectangle rect;
                Vector2 camPos = camera.target;
                Vector2 pos = new Vector2(0, 0);
                switch (key) {
                    case 'W' : 
                        pos = new Vector2(camPos.X, camPos.Y - ScreenHeight / 2 + 8);
                        break;
                    case 'A' : 
                        pos = new Vector2(camPos.X - ScreenWidth / 2 + 8, camPos.Y);
                        break;
                    case 'S' : 
                        pos = new Vector2(camPos.X, camPos.Y + ScreenHeight / 2 - 48);
                        break;
                    case 'D' : 
                        pos = new Vector2(camPos.X + ScreenWidth / 2 - 48 , camPos.Y);
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
            int h = 5;
            int w = 3;
            DrawTriangle(ship.Position + new Vector2(0, h),
                            ship.Position + new Vector2(w, -h),
                            ship.Position + new Vector2(-w, -h),
                            Color.RED);
        }
    }
}