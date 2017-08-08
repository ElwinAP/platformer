using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameDevProtoType
{
    public class Animation
    {
        public Texture2D spriteSheet;
        protected Vector2 Position;
        protected float time, frameTime;
        protected ContentManager Content;
        protected int frameHeight = 32;
        protected int frameWidth = 32;
        protected const int framesPerMovement = 2;
        protected float origin;

        //Ik gebruik een dictionary om animation frames bij te houden
        Dictionary<string, Rectangle[]> Animations;


        public Animation ()
        {
            
        }

        public double calculateFrame(double animationcounter, double animationspeed)
        {
            
            if (animationcounter < 2)
            {
                animationcounter = animationcounter + animationspeed;
            }

            else
            {
                animationcounter = 0;
            }
            
            return animationcounter;           
        }

        public void Initialize (Vector2 position)
        {
            this.Position = position;
            time = 0f;
            frameTime = 0.1f;
            origin = 0f;
            Animations = new Dictionary<string, Rectangle[]>();
        }

        public void LoadContent(ContentManager content, Texture2D spritesheet)
        {
            this.Content = content;
            this.spriteSheet = spritesheet; 

            Animations["walk_right"] = new Rectangle[2] { new Rectangle(32, 0, frameWidth, frameHeight), new Rectangle(64, 0, frameWidth, frameHeight) };
            Animations["walk_left"] = new Rectangle[2] { new Rectangle(0, 0, frameWidth, frameHeight), new Rectangle(96, 0, frameWidth, frameHeight) };
        }

        public void Unloadcontent()
        {
            Content.Unload();
            spriteSheet = null;
            time = frameTime = 0;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, string current_animation, int frame)
        {
            spriteBatch.Draw(spriteSheet, position, Animations[current_animation][frame], Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}
