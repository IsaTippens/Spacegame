using System;
using System.Numerics;
using System.Collections.Generic;

using Raylib_cs;
using static Raylib_cs.Raylib;

using static Spacegame.Utils;

namespace Spacegame.Universe
{
    public class StarGrid : Grid<Vector2[]>
    {
        int _maxStars = 3;
        int _minStars = 1;

        public StarGrid()
        {
        }

        public override Vector2[] GenerateChunk(Vector2 sector)
        {
            int width = ChunkSize, height = ChunkSize;
            Vector2[] chunk = new Vector2[RNG.Next(_minStars, _maxStars)];
            for (int i = 0; i < chunk.Length; i++)
            {
                chunk[i] = new Vector2(
                    RNG.Next(1, width),
                    RNG.Next(1, height)
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