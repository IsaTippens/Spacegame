using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Spacegame
{
    public struct SpaceShip
    {
        private Vector2 _position;

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public SpaceShip(Vector2 position)
        {
            _position = new Vector2(0, 0);
        }
        
    }


}