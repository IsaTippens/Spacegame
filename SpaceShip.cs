using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System;

namespace Spacegame
{
    public class SpaceShip
    {
        private Vector2 _position;

        private float _rotation;

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public float Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }
        public SpaceShip(Vector2 position, float rotation)
        {
            _position = position;
            _rotation = rotation;
        }

        public void Update()
        {
            //Rotation += 10f * GetFrameTime();
        }

        //Calculations here
        //https://www.desmos.com/calculator/iksujbjslc
        public void Draw()
        {
            int size = 20;
            int h = size;
            int w = size;

            //top, right, inner, left
            Vector2[] points = new[] { 
                new Vector2(MathF.Sin(Rotation) * w, -MathF.Cos(Rotation) * h),
                new Vector2(MathF.Sin((5 * MathF.PI) / 4 + Rotation) * w, -MathF.Cos((5 * MathF.PI) / 4 + Rotation) * h),
                new Vector2(-MathF.Sin(Rotation) / 4 * w, -MathF.Cos(MathF.PI + Rotation) / 4 * h),
                new Vector2(MathF.Sin((3 * MathF.PI) / 4 + Rotation) * w, -MathF.Cos((3 * MathF.PI) / 4 + Rotation) * h),
            };

            for (int i = 1; i <= points.Length; i++)
            {
                if (i == points.Length)
                {
                    DrawLineV(Position + points[i - 1],
                            Position + points[0], Color.RED);
                }
                else
                {
                    DrawLineV(Position + points[i - 1],
                            Position + points[i], Color.RED);
                }
            }
        }

        void DebugDraw()
        {
            int h = 20;
            DrawCircleLines((int)Position.X, (int)Position.Y, h, Color.RED);
        }
    }


}