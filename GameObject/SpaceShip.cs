using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System;

namespace Spacegame
{
    public class SpaceShip : GameObject
    {
        Vector2[] graphicPoints;
        public SpaceShip(Vector2 position, float rotation, float size) : base(position, rotation, size)
        {
            GenerateGraphic();
        }

        public override void Update()
        {
            //Rotation += 10f * GetFrameTime();
        }


        public override void Draw()
        {
            var gp = graphicPoints;



            for (int i = 1; i <= gp.Length; i++)
            {
                if (i == gp.Length)
                {
                    DrawLineV(Position + gp[i - 1],
                            Position + gp[0], Color.RED);
                }
                else
                {
                    DrawLineV(Position + gp[i - 1],
                            Position + gp[i], Color.RED);
                }
            }
        }

        public override void DebugDraw()
        {
            DrawCircleLines((int)Position.X, (int)Position.Y, Scale, Color.RED);
            DrawCircleLines((int)Position.X, (int)Position.Y, Scale / 4, Color.RED);
        }

        void GenerateGraphic()
        {
            //Calculations here
            //https://www.desmos.com/calculator/iksujbjslc
            //top, right, inner, left
            graphicPoints = new[] {
                new Vector2(MathF.Sin(Rotation) * Scale, -MathF.Cos(Rotation) * Scale),
                new Vector2(MathF.Sin((5 * MathF.PI) / 4 + Rotation) * Scale, -MathF.Cos((5 * MathF.PI) / 4 + Rotation) * Scale),
                new Vector2(-MathF.Sin(Rotation) / 4 * Scale, -MathF.Cos(MathF.PI + Rotation) / 4 * Scale),
                new Vector2(MathF.Sin((3 * MathF.PI) / 4 + Rotation) * Scale, -MathF.Cos((3 * MathF.PI) / 4 + Rotation) * Scale),
            };
        }
    }


}