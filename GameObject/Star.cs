using Spacegame;
using System;
using System.Numerics;
using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Spacegame
{
    public class Star : GameObject
    {

        const float PI = 3.14159265359f;
        float SQRT_PI = 1.77245385091f;

        
        
        Vector2[] graphicPoints;
        public Star()
        {
            this.Rotation = 0f;
            this.Position = Vector2.Zero;
        }
        public Star(Vector2 Position)
        {
            this.Rotation = 0f;
            this.Scale = 5;
            this.Position = Position;
            GenerateGraphic();
        }

        public Star(Vector2 Position, int Scale)
        {
            this.Rotation = 0f;
            this.Scale = Scale;
            this.Position = Position;
            GenerateGraphic();
        }

        public Star(Vector2 Position, int Scale, float Rotation)
        {
            this.Rotation = Rotation;
            this.Scale = Scale;
            this.Position = Position;
            GenerateGraphic();
        }

        public override void Update()
        {
            RotationDegrees += 50 * GetFrameTime();
            GenerateGraphic();
            base.Update();
        }

        public override void Draw()
        {
            var gp = graphicPoints;
            for (int i = 0; i <= 5; i++)
            {
                if (i < 5)
                {
                    DrawLineV(Position + gp[i], Position + gp[i + 5], Color.GOLD);
                    if (i < 4)
                        DrawLineV(Position + gp[i + 5], Position + gp[i + 1], Color.GOLD);
                }
                else
                {
                   DrawLineV(Position + gp[9], Position + gp[0], Color.GOLD);
                }
            }
            base.Draw();
        }

        public override void DebugDraw()
        {
            var gp = graphicPoints;
            for (int i = 0; i <= 5; i++)
            {
                DrawText($"({gp[i].X}, {gp[i].Y})", (int)(Position.X + gp[i].X), (int)(Position.Y + gp[i].Y), 10, Color.RED);
            }
            base.DebugDraw();
        }



        void GenerateGraphic()
        {
            //Calculations here
            //https://www.desmos.com/calculator/0g3m2fumwv
            graphicPoints = new[] {
                //Outer
                //a1
                new Vector2(MathF.Sin(Rotation) * Scale, -MathF.Cos(Rotation) * Scale),
                //a2
                new Vector2(MathF.Sin((2 * PI) / 5 + Rotation) * Scale, -MathF.Cos((2 * PI) / 5 + Rotation) * Scale),
                //a3
                new Vector2(MathF.Sin(4 * PI / 5 + Rotation) * Scale, -MathF.Cos(4 * PI / 5  + Rotation) * Scale),
                //a4
                new Vector2(MathF.Sin((6 * PI) / 5 + Rotation) * Scale, -MathF.Cos((6 * PI) / 5 + Rotation) * Scale),
                //a5
                new Vector2(MathF.Sin((8 * PI) / 5 + Rotation) * Scale, -MathF.Cos((8 * PI) / 5 + Rotation) * Scale),
                //Inner
                //b1
                new Vector2(-MathF.Sin(5 * PI / 4 + Rotation) / SQRT_PI * Scale, -MathF.Cos(PI + 5 * PI / 4 + Rotation) / SQRT_PI * Scale),
                //b2
                new Vector2(-MathF.Sin((8 * PI) / 5 + Rotation) / SQRT_PI * Scale, -MathF.Cos(PI + 8 * PI / 5 + Rotation) / SQRT_PI  * Scale),
                //b3
                new Vector2(-MathF.Sin(Rotation) / SQRT_PI * Scale, -MathF.Cos(PI + Rotation) / SQRT_PI * Scale),
                //b4
                new Vector2(-MathF.Sin((2 * PI) / 5 + Rotation) / SQRT_PI * Scale, -MathF.Cos(PI + (2 * PI) / 5 + Rotation) / SQRT_PI * Scale),
                //b5
                new Vector2(-MathF.Sin((3 * PI) / 4 + Rotation) / SQRT_PI * Scale, -MathF.Cos(PI + (3 * PI) / 4 + Rotation) / SQRT_PI * Scale),
            };
        }
    }
}