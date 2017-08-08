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
    public abstract class Level
    {
        Vector2 obstaclePosition;
        protected byte[,] tileArray;
        protected Obstacle Obstacle;
        protected Rectangle obstacleBounds;
        protected Texture2D ObstacleSprite;
        protected ContentManager Content;
        protected SpriteBatch SpriteBatch;
        protected Texture2D BackgroundTexture;
        public Rectangle TileSelector;
        const int SpriteWidth = 32;
        const int SpriteHeight = 32;
        int TileHelper;
        protected Entity entity;
        protected Vector2 enemyPosition;
        


        public Level(ContentManager content, SpriteBatch spritebatch)
        {
            this.Content = content;
            this.SpriteBatch = spritebatch;
            TileSelector = new Rectangle();
        }

        public abstract void Initialize();


        public abstract void LoadContent();
        
        //hier verderdoen
        //public void getEnemyPositions()
        //{
        //    for (int row = 0; row < tileArray.GetLength(0); row++)
        //    {
        //        for (int column = 0; column < tileArray.GetLength(1); column++)
        //        {
        //            if (tileArray[row, column] == 9)
        //            {
        //                enemyPosition = new Vector2(column * 32, row * 32);
        //            }
        //        }
        //    }                   
        //}


        public void Collision(GameTime gameTime, Rectangle entityBounds, Entity entity, Vector2 MoveSpeed)
        {
            entity.IsGrounded = false; // !BELANGRIJK! dit veroorzaakt stutter-bug maar lost ander groter probleem op (speler valt niet van blokjes zonder te springen)

            for (int row = 0; row < tileArray.GetLength(0); row++)
            {
                for (int column = 0; column < tileArray.GetLength(1); column++)
                {
                    if (tileArray[row, column] != 0)
                    {
                        obstaclePosition = new Vector2(column * 32, row * 32);
                        obstacleBounds = new Rectangle((int)(obstaclePosition.X), (int)(obstaclePosition.Y), 32, 32);

                        if (entityBounds.Intersects(obstacleBounds))

                        {
                            //we berekenen eerst de positie van elke kant van het blokje en dan tegenover welk punt de speler het dichtste zit met hulp van math.min functies

                            float rightEdge = obstacleBounds.X - (entity.Position.X + entityBounds.Width);
                            float leftEdge = obstacleBounds.X + obstacleBounds.Width - entity.Position.X;
                            float topEdge = obstacleBounds.Y - (entity.Position.Y + entityBounds.Height);
                            float bottomEdge = obstacleBounds.Y + obstacleBounds.Height - entity.Position.Y;

                            float shortestWidth = Math.Min(Math.Abs(rightEdge), Math.Abs(leftEdge));
                            float shortestHeight = Math.Min(Math.Abs(topEdge), Math.Abs(bottomEdge));
                            float shortestFinal = Math.Min(Math.Abs(shortestWidth), Math.Abs(shortestHeight));

                            if (shortestFinal == Math.Abs(leftEdge)) //randomly activates while walking over tiles. causes enemy to bug
                            {
                                entity.Position.X = obstacleBounds.X + obstacleBounds.Width;
                                MoveSpeed.X = 0;
                                entity.TouchedEdge = true;
                            }

                            else if (shortestFinal == Math.Abs(rightEdge))
                            {
                                entity.Position.X = obstacleBounds.X - entityBounds.Width;
                                MoveSpeed.X = 0;
                                entity.TouchedEdge = true;
                            }

                            else if (shortestFinal == Math.Abs(bottomEdge))
                            {
                                entity.Position.Y = obstacleBounds.Y + obstacleBounds.Height;
                                entity.MoveSpeed.Y = 0;     
                            }

                            else if (shortestFinal == Math.Abs(topEdge))
                            {
                                entity.Position.Y = obstacleBounds.Y - entityBounds.Height;
                                MoveSpeed.Y = 0;
                                entity.IsGrounded = true;
                            }

                        }

                    }
                    
                }
            }
        }

        public abstract void DrawBackground(GraphicsDevice graphicsdevice);

        public void Draw()
        {
            for (int row = 0; row < tileArray.GetLength(0); row++)
            {
                for (int column = 0; column < tileArray.GetLength(1); column++)
                {
                    if (tileArray[row, column] != 0)
                    {
                        switch (tileArray[row, column])
                        {
                            case 1:
                                TileHelper = 448; //ground tiles
                                break;

                            case 2:
                                TileHelper = 512; //boxes
                                break;

                            case 3:
                                TileHelper = 64; //floating left
                                break;

                            case 4:
                                TileHelper = 256; //floating right
                                break;

                            case 5:
                                TileHelper = 320; //floating middle
                                break;

                            case 6:
                                TileHelper = 480; //column tiles
                                break;

                            case 7:
                                TileHelper = 192; //connecting column-platform tile
                                break;

                            
                        }

                        TileSelector = new Rectangle(TileHelper, 0, 32, 32);
                        obstaclePosition = new Vector2(column * TileSelector.Width, row * TileSelector.Height);
                        SpriteBatch.Draw(ObstacleSprite, obstaclePosition, TileSelector, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            

        }      

    }
}