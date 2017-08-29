using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        Song gameoversound;
        bool gamestateactive;
        int gameoverinterval;
        int time;
        LevelState _levelstate;    

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gamestateactive = true;
            gameoverinterval = 2000; //milliseconds
            time = 0;

            if (_levelstate == LevelState.level1)
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
            gameoversound = Content.Load<Song>("Sounds\\gameover");
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
                switch (_levelstate)
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

            if (gamestateactive == true)
            {
                player.Update(gameTime);
            }
           
            level.Collision(player.Bounds, player);
            enemy1.Update(gameTime);
            level.Collision(enemy1.Bounds, enemy1);
            enemy2.Update(gameTime);
            level.Collision(enemy2.Bounds, enemy2);
           
            if (player.NotActive == true)
            {                
                GameOver(gameTime);
            }

            base.Update(gameTime);
        }

        private void TransferLevel2 (GameTime gameTime)
        {           
            UnloadContent();
            _levelstate = LevelState.level2;
            level = new Level2(Content, spriteBatch);          
            StartNewLevel(gameTime);
        }

        private void TransferLevel3 (GameTime gameTime)
        {
            UnloadContent();
            _levelstate = LevelState.level3;
            level = new Level3(Content, spriteBatch);
            StartNewLevel(gameTime);
        }

        private void GameOver (GameTime gameTime)
        {           
            time += gameTime.ElapsedGameTime.Milliseconds;
            gamestateactive = false;
            MediaPlayer.Play(gameoversound);

            if (time > gameoverinterval)
            {
                UnloadContent();
                _levelstate = LevelState.level1;
                level = new Level1(Content, spriteBatch);
                gamestateactive = true;
                StartNewLevel(gameTime);
            }           
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
