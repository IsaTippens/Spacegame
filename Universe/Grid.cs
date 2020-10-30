using System;
using System.Numerics;
using System.Collections.Generic;

/*
    Grid -> collections of chunks
            Position of Chunk -> Sector(Vector 2) : Key
            Content of Chunk -> Dynamic(T) : Value
*/

namespace Spacegame.Universe
{
    public abstract class Grid<T> : GameObject
    {
        private Dictionary<Vector2, T> _chunks;

        public Dictionary<Vector2, T> Chunks {
            get => _chunks;
        }

        private int _chunkSize;

        public int ChunkSize 
        {
            get => _chunkSize;
            set => _chunkSize = value;
        }


        public Grid()
        {
            _chunks = new Dictionary<Vector2, T>();
        }

        public virtual T AddChunk(Vector2 sector, T value)
        {
            //WARN  WARN    WARN    WARN    WARN    WARN    WARN    WARN
            //Possible Future frustration
            //Chunks being overriden here
            if (IsSectorGenerated(sector))
            {
                _chunks[sector] = value;
                Console.WriteLine($"Chunk OVERRIDE at sector {sector}");
            }
            else
            {
                _chunks.Add(sector, value);
                Console.WriteLine($"Chunk ADD at sector {sector}");
            }
            
            return value;
        }
        public abstract T GenerateChunk(Vector2 sector);

        public virtual T GetChunk(Vector2 sector)
        {
            if (!IsSectorGenerated(sector))
            {
                return GenerateChunk(sector);
            }
            return _chunks[sector];
        }

        public bool IsSectorGenerated(Vector2 sector) 
        {
            return _chunks.ContainsKey(sector);
        }

        public Vector2 GetSectorAtPosition(Vector2 position)
        {
            return new Vector2(MathF.Floor(position.X / ChunkSize), MathF.Floor(position.Y / ChunkSize));
        }
    }
}