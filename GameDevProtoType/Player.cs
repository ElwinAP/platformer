using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GameDevProtoType;

namespace GameDevProtoType
{
    public class Player : Entity
    {
        
        KeyboardState currentKeyboardState;
        public Rectangle Bounds;


        public int Width
        {
            get { return animation.spriteSheet.Width / 6; }
        }

        public int Height
        {
            get { return animation.spriteSheet.Height; }
        }

        public Player(GraphicsDevice graphicsDevice, ContentManager content, Level level)
            : base (graphicsDevice, content, level)
        { }

        public override void Initialize(Vector2 position)
        {
            Position = position;
            MoveSpeed = new Vector2(0, 1);
            Health = 100;
            Active = true;
            IsGrounded = false;
            animationCounter = 0;
            animationBaseSpeed = 0.2;
            Velocity = new Vector2(2f, 0.02f);
            MaxVelocity = new Vector2(4f, 0.15f);
            Acceleration = new Vector2(0.01f, 0.002f);
            current_animation = "walk_right";

            animation = new Animation();
            animation.Initialize(Position);
        }

        public override void LoadContent()
        {
            EntitySprite = Content.Load<Texture2D>("Graphics\\playersheet");
            animation.LoadContent(Content, EntitySprite);

            //speler collision box
            Bounds = new Rectangle((int)(Position.X), (int)(Position.Y), 32, 32);
        }

        public override void Update(GameTime gametime)
        {
            currentKeyboardState = Keyboard.GetState();

            UserInput(gametime);

            // speler binnen grenzen scherm houden
            Position.X = MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, GraphicsDevice.Viewport.Height - Height);

            // positie van speler box updaten
            Bounds.X = (int)Position.X;
            Bounds.Y = (int)Position.Y;

        }

        private void UserInput(GameTime gametime) // alle invoer hier afhandelen
        {
            time = gametime.ElapsedGameTime.Milliseconds;

            Position += MoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                if (Velocity.X < MaxVelocity.X)
                {
                    Velocity.X += Acceleration.X * time;
                }
                
                MoveSpeed.X = -Velocity.X;
                current_animation = "walk_left";
                calculateEntityFrame(Velocity);
            }

            else if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                if (Velocity.X < MaxVelocity.X)
                {
                    Velocity.X += Acceleration.X * time;
                }

                MoveSpeed.X = Velocity.X;
                current_animation = "walk_right";
                calculateEntityFrame(Velocity);
            }

            else
            {
                MoveSpeed.X = 0f;
                Velocity.X = 0;
            }
              

            if (currentKeyboardState.IsKeyDown(Keys.Space) && IsGrounded == true)
            {
                Position.Y -= 15f * Velocity.Y; // met velocity vermenigvuldigen heeft bug dat speler even tegen blokjes blijft "plakken" opgelost. Oppassen voor nieuwe bugs
                MoveSpeed.Y = -5f;
                IsGrounded = false;
            }

            // Als speler niet op de grond is, pas zwaartekracht toe. 
            if (!IsGrounded)
            {
                MoveSpeed.Y += 0.15f;
                IsGrounded = false;
            }

            else
            {
                MoveSpeed.Y = 0f;
                Velocity.Y = 1f;
            }
                     
            
        }

        

    }
}
