using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameDevProtoType
{
    class Level2 : Level
    {

        public Level2(ContentManager content, SpriteBatch spritebatch)
            : base (content, spritebatch)
        { }

        protected void CreateTileArray()
        {
            tileArray = new byte[,]
            {
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0 },
            { 5,5,5,5,5,5,5,5,5,4,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,6,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,7,6,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,6,6,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,10,0,0,0,0,1,6,6,6,0,0,0,0,0,8,0 },
            { 1,1,1,1,1,1,1,1,1,1,1,7,0,0,6,6,6,6,1,1,1,1,1,1,1 },
            { 6,6,6,6,6,6,6,6,6,6,6,6,9,9,6,6,6,6,6,6,6,6,6,6,6 }
            };
        }

        public override void Initialize()
        {
            CreateTileArray();
            StartPosition = new Vector2(20, 400);
            enemy1Position = new Vector2(260, 384);
            enemy2Position = new Vector2(120, 160);
        }

        public override void LoadContent()
        {
            BackgroundTexture = Content.Load<Texture2D>("Graphics\\Level2");
            ObstacleSprite = Content.Load<Texture2D>("Graphics\\Tilesheet");
        }

        public override void DrawBackground(GraphicsDevice graphicsdevice)
        {
            SpriteBatch.Draw(BackgroundTexture, graphicsdevice.Viewport.Bounds, Color.White);
        }
    }
}
