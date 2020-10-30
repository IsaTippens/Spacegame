using System;
using System.Numerics;
using System.Collections.Generic;
using static Raylib_cs.Raylib;
using Raylib_cs;
using Spacegame;
using Spacegame.Universe;

namespace Spacegame.Universe
{
    public class Universe : Grid<GameObject>
    {
        public Universe()
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override GameObject GenerateChunk(Vector2 sector)
        {
            throw new NotImplementedException();
        }

    }
}