using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameDevProtoType
{
    enum LevelState
    {
        level1,
        level2,
        level3
    }
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Level level;
        Enemy enemy1;
        Enemy enemy2;
        LevelState _state;    

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            if (_state == LevelState.level1)
            {
                level = new Level1(Content, spriteBatch);
            }
          
            player = new Player(GraphicsDevice, Content, level);
            enemy1 = new Enemy(GraphicsDevice, Content, level);
            enemy2 = new Enemy(GraphicsDevice, Content, level);

            level.Initialize();
            player.Initialize(level.StartPosition);
            enemy1.Initialize(level.enemy1Position);
            enemy2.Initialize(level.enemy2Position);

            base.Initialize();
        }

        protected override void LoadContent()
        {          
            player.LoadContent();
            enemy1.LoadContent();
            enemy2.LoadContent();
            level.LoadContent();          
        }

        protected override void UnloadContent()
        {
            Content.Unload(); //niet zeker of dit juist alle content unload.
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (player.ReachedExit == true)
            {
                switch (_state)
                {
                    case LevelState.level1:
                        TransferLevel2(gameTime);
                        break;

                    case LevelState.level2:
                        TransferLevel3(gameTime);
                        break;
                }

                player.ReachedExit = false;
            }

            player.Update(gameTime);
            level.Collision(player.Bounds, player);
            enemy1.Update(gameTime);
            level.Collision(enemy1.Bounds, enemy1);
            enemy2.Update(gameTime);
            level.Collision(enemy2.Bounds, enemy2);

            base.Update(gameTime);
        }

        private void TransferLevel2 (GameTime gameTime)
        {
            UnloadContent();
            _state = LevelState.level2;
            level = new Level2(Content, spriteBatch);
            StartNewLevel(gameTime);
        }

        private void TransferLevel3 (GameTime gameTime)
        {
            UnloadContent();
            _state = LevelState.level3;
            level = new Level3(Content, spriteBatch);
            StartNewLevel(gameTime);
        }

        public void StartNewLevel (GameTime gameTime)
        {
            level.Initialize();
            player.Initialize(level.StartPosition);
            enemy1.Initialize(level.enemy1Position);
            enemy2.Initialize(level.enemy2Position);
            LoadContent();
            Draw(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            // alle drawing toevoegen tussen begin en end
            spriteBatch.Begin();
          
            level.DrawBackground(GraphicsDevice);
            level.Draw();
            player.Draw(spriteBatch);
            enemy1.Draw(spriteBatch);
            enemy2.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
