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

        public void Draw()
        {
            int h = 20;
            int w = 20;
            Vector2 top = new Vector2(MathF.Sin(Rotation) * w, -MathF.Cos(Rotation) * h);
            Vector2 left = new Vector2(MathF.Sin((3 * MathF.PI) / 4 + Rotation) * w, -MathF.Cos((3 * MathF.PI) / 4 + Rotation) * h);
            Vector2 right = new Vector2(MathF.Sin((5 * MathF.PI) / 4 + Rotation) * w, -MathF.Cos((5 * MathF.PI) / 4 + Rotation) * h);
            /*DrawTriangle(Position + top,
                            Position + right,
                            Position + left,
                            Color.RED);*/
            DrawLineV(Position + top,
                            Position + right, Color.RED);
            DrawLineV(Position + right,
                            Position + left, Color.RED);
            DrawLineV(Position + left,
                            Position + top, Color.RED);
            
        }

        void DebugDraw() 
        {   
            int h = 20;
            DrawCircleLines((int)Position.X, (int)Position.Y, h, Color.RED);
        }        
    }


}