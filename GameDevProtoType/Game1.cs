using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameDevProtoType
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Level level1;
        Enemy1 enemy1;
        Vector2 StartPosition;
        Vector2 EnemyPosition;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            level1 = new Level1(Content, spriteBatch);
            player = new Player(GraphicsDevice, Content, level1);
            enemy1 = new Enemy1(GraphicsDevice, Content, level1);

            StartPosition = new Vector2(20, 400);
            EnemyPosition = new Vector2(400, 400);

            player.Initialize(StartPosition);
            enemy1.Initialize(EnemyPosition);

            level1.Initialize();
            


            base.Initialize();
        }


        protected override void LoadContent()
        {          
            player.LoadContent();
            level1.LoadContent();
            enemy1.LoadContent();
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            level1.Collision(gameTime, player.Bounds, player, player.MoveSpeed);
            //level1.Collision(gameTime, enemy1.Bounds, enemy1, enemy1.MoveSpeed);
            enemy1.Update(gameTime);
            
                   
            base.Update(gameTime);
        }


        


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            // alle drawing toevoegen tussen begin en end
            spriteBatch.Begin();

            
            level1.DrawBackground(GraphicsDevice);
            level1.Draw();
            player.Draw(spriteBatch);
            enemy1.Draw(spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
