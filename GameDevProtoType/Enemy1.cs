using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameDevProtoType
{
    class Enemy1 : Entity
    {
        public Rectangle Bounds;

        public int Width
        {
            get { return animation.spriteSheet.Width / 4; }
        }

        public int Height
        {
            get { return animation.spriteSheet.Height; }
        }

        public Enemy1(GraphicsDevice graphicsDevice, ContentManager content, Level level)
            : base (graphicsDevice, content, level)
        { }

        public override void Initialize(Vector2 position)
        {
            Position = position;
            MoveSpeed = new Vector2(0, 1);
            Health = 20;
            Active = true;
            IsGrounded = false;
            TouchedEdge = false;
            animationCounter = 0;
            animationBaseSpeed = 0.2;
            Velocity = new Vector2(2f, 0.02f);
            current_animation = "walk_right";

            animation = new Animation();
            animation.Initialize(Position);
        }

        public override void LoadContent()
        {
            EntitySprite = Content.Load<Texture2D>("Graphics\\Enemy1Sheet");
            animation.LoadContent(Content, EntitySprite);

            //Enemy collision box
            Bounds = new Rectangle((int)(Position.X), (int)(Position.Y), 32, 32);
        }

        public override void Update(GameTime gametime)
        {
            enemyPatrol(gametime);
            
            Position.X = MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, GraphicsDevice.Viewport.Height - Height);

            // positie van enemy box updaten
            Bounds.X = (int)Position.X;
            Bounds.Y = (int)Position.Y;
        }

        private void enemyPatrol (GameTime gametime)
        {
            Position += MoveSpeed;

            MoveSpeed.X = Velocity.X;
            current_animation = "walk_right";
            calculateEntityFrame(Velocity);
            

            if (TouchedEdge == true)
            {
                Velocity.X = Velocity.X * -1;
                current_animation = "walk_left";
                TouchedEdge = false;
            }
            //else
            //{
            //    MoveSpeed.X = Velocity.X;
            //    current_animation = "walk_right";
            //    calculateEntityFrame(Velocity);
            //}


            if (!IsGrounded)
            {
                MoveSpeed.Y += 0.15f;
                IsGrounded = false;   
            }
        }
    }
}
