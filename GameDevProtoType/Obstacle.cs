using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameDevProtoType;

namespace GameDevProtoType
{
    public class Obstacle
    {
        public Texture2D ObstacleSprite;
        public Vector2 Position;
    

        public int Width
        {
            get { return ObstacleSprite.Width; }
        }

        public int Height
        {
            get { return ObstacleSprite.Height; }
        }

        public void Initialize (Texture2D sprite)
        {
            ObstacleSprite = sprite;
        }



        public void Update (GameTime gametime)
        {

        }


    }
}
