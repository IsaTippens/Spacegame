using System;
using System.Numerics;

namespace Spacegame
{
    public class GameObject : Drawable, IUpdateable
    {
        private Vector2 _position;

        private float _rotation;

        private float _scale;

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

        public float RotationDegrees 
        {
            get => Rotation * (180f / MathF.PI);
            set => Rotation = value * (MathF.PI / 180f); 
        }

        public float Scale
        {
            get => _scale;
            set => _scale = value;
        }
        
        public GameObject() : this(0f, 0f)
        {

        }

        public GameObject(float rotation = 0f, float scale = 0f) : this(Vector2.Zero, rotation, scale)
        {

        }

        public GameObject(Vector2 position, float rotation, float scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public virtual void Update()
        {

        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void DebugDraw()
        {
            base.DebugDraw();
        }
    }
}