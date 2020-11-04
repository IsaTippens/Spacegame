using System;
using System.Numerics;
using System.Collections.Generic;

using Raylib_cs;
using static Raylib_cs.Raylib;

using static Spacegame.Utils;

namespace Spacegame.Universe
{
    public class StarGrid : Grid<Star[]>
    {
        int _maxStars = 3;
        int _minStars = 1;

        public StarGrid()
        {
        }

        public override Star[] GenerateChunk(Vector2 sector)
        {
            int width = ChunkSize, height = ChunkSize;
            Star[] chunk = new Star[RNG.Next(_minStars, _maxStars)];

            for (int i = 0; i < chunk.Length; i++)
            {
                chunk[i] = new Star(new Vector2(
                    RNG.Next(1, width) + sector.X * width,
                    RNG.Next(1, height) + sector.Y * height
                    ),
                    RNG.Next(2, 14),
                    (float)RNG.NextDouble() * (MathF.PI * 2)
                    );
            }

            return AddChunk(sector, chunk);
        }

        public override void Draw()
        {

            base.Draw();
        }
        public override void DebugDraw()
        {
            foreach (var chunk in Chunks)
            {
                Vector2 sector = chunk.Key;
                int x = (int)sector.X * ChunkSize;
                int y = (int)sector.Y * ChunkSize;
                Rectangle rect = new Rectangle(x, y, ChunkSize, ChunkSize);
                DrawRectangleLinesEx(rect, 1, Color.RED);
                DrawText($"({sector.X},{sector.Y})", x + 2, y + 2, 8, Color.RED);
                DrawText($"{chunk.Value.Length} stars", x + 2, y + 22, 8, Color.RED);
            }
            base.DebugDraw();
        }

    }
}