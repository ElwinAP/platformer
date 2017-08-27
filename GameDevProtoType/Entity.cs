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
    public abstract class Entity
    {

        public Texture2D EntitySprite;
        public Vector2 Position, MoveSpeed;
        public bool Active; // later gebruiken voor death state
        public int Health;
        public GraphicsDevice GraphicsDevice;
        public ContentManager Content;
        public Level Level;
        public bool _IsGrounded;

        public bool IsGrounded
        {
            get { return _IsGrounded; }
            set { _IsGrounded = value; }
        }

        public bool _TouchedEdge;

        public bool TouchedEdge
        {
            get { return _TouchedEdge; }
            set { _TouchedEdge = value; }
        }
        
        public int time;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Vector2 MaxVelocity;
        public int animationFrame;
        public double animationCounter;
        public double animationBaseSpeed; // dit tel ik op bij de animationCounter in animation.calculateframe om snelheid van animatie te bepalen
        public Animation animation;
        public string current_animation;

        public Entity(GraphicsDevice graphicsDevice, ContentManager content, Level level)
        {
            this.GraphicsDevice = graphicsDevice;
            this.Content = content;
            this.Level = level;
        }

        public abstract void Initialize(Vector2 position);

        public abstract void LoadContent();

        public abstract void Update(GameTime gametime);
        

        public void calculateEntityFrame(Vector2 velocity)
        {
            if (animationCounter == 0 || animationCounter == 1)
            {
                animationFrame = (int)animationCounter;
            }
           
            // Geprobeerd stutter issue met velocity op te lossen
            // animationBaseSpeed *= velocity.X;

            animationCounter = animation.calculateFrame(animationCounter, animationBaseSpeed);
        }
        
        public void walkLeft ()
        {
            MoveSpeed.X = -Velocity.X;
            current_animation = "walk_left";
            calculateEntityFrame(Velocity);
        } 

        public void walkRight ()
        {
            MoveSpeed.X = Velocity.X;
            current_animation = "walk_right";
            calculateEntityFrame(Velocity);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            animation.Draw(spritebatch, Position, current_animation, animationFrame);           
        }

    }

        
}
