using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ScreenSystemLibrary;


namespace SideScroller
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //
        public enum GameState
        {
            MainMenu,
            Playing,
            Death
        }
        GameState CurrentState = GameState.MainMenu;
        //

        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        TileMap map;
        Song music;
        bool started;
        StartMenuScreen StartScreen;
        public bool paused = false;
        KeyboardState keyStateOld;
        SpriteFont font1;
        Vector2 FontPos;
        public Texture2D healthBar;
        public Vector2 healthPos;
        public Rectangle healthRec;
        Scrolling scrolling1;
        Scrolling scrolling2;

        ScreenSystem screenSystem;
        GameOverScreen screen;
        PlayScreen Pscreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            started = false;
            StartScreen = new StartMenuScreen();
            this.IsMouseVisible = true;

            screenSystem = new ScreenSystem(this);
            screen = new GameOverScreen();
            Components.Add(screenSystem);
        }

        protected override void Initialize()
        {
            screenSystem.AddScreen(screen);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font1 = Content.Load<SpriteFont>("Font/SpriteFont1");
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            player = new Player();
            Texture2D tex = Content.Load<Texture2D>("Character[1]");
            healthBar = Content.Load<Texture2D>("HealthBar");
            healthPos = new Vector2(50, 30);
            healthRec = new Rectangle(0, 0, healthBar.Width, healthBar.Height);
            player.LoadContent(tex);
            map = new TileMap();
            Texture2D tex1 = Content.Load<Texture2D>("in");
            Texture2D tex2 = Content.Load<Texture2D>("block");
            Texture2D tex3 = Content.Load<Texture2D>("block[1]");
            map.LoadContent(tex1, tex2, tex3);
            //music = Content.Load<Song>("music/Music");
            MediaPlayer.IsRepeating = true;

            scrolling1 = new Scrolling(Content.Load<Texture2D>("BackG"), new Rectangle(0, 0, 1280, 720));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("BackG1"), new Rectangle(1280, 0, 1280, 720));
            
            StartScreen.LoadContent(Content);
            
        }
        
        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            IsMouseVisible = true;
            MouseState mouse = Mouse.GetState();
            KeyboardState key = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (key.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (screen.playA == true)
            {
                player.Update(gameTime);
                Camera();
                Collision();
            }
     
           /* StartScreen.Update();

            if (StartScreen.Play.clicked)
            {
                if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
                    scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;
                if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
                    scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
                scrolling1.Update();
                scrolling2.Update();
                if (!started)
                {                  
                    //MediaPlayer.Play(music);
                    started = true;
                }

                if (key.IsKeyDown(Keys.P) && !keyStateOld.IsKeyDown(Keys.P))
                {
                    paused = !paused;
                }

                if (!paused)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.T))
                    {
                        healthRec.Width -= 1;
                        if (healthRec.Width == 0)
                        {
                            
                        }
                    }
                    if (player.worldPosition.Y >= graphics.GraphicsDevice.Viewport.Height)
                    {
                        
                    }

                    player.Update(gameTime);
                    Camera();
                    Collision();
                }
                keyStateOld = key;
     
            }
            if (!StartScreen.Play.clicked)
            {
                StartScreen.Update();
            }

            if (StartScreen.Exit.clicked)
            {
                this.Exit();
            }*/
            base.Update(gameTime);
        }
    
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);
            string output = "PAUSED";
            
            Vector2 FontOrigin = font1.MeasureString(output) / 2;



            if (screen.playA == true)
            {
                map.Draw(spriteBatch);
                player.Draw(spriteBatch);
                
            }

            
            /*if (StartScreen.Play.clicked)
            {
                spriteBatch.Begin();
                scrolling1.Draw(spriteBatch);
                //scrolling2.Draw(spriteBatch);
                spriteBatch.End();

                map.Draw(spriteBatch);
                player.Draw(spriteBatch);

                spriteBatch.Begin();
                spriteBatch.Draw(healthBar, healthPos, healthRec, Color.White);
                spriteBatch.End();

                if (paused)
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawString(font1, output, FontPos, Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    spriteBatch.End();  
                }

                if (healthRec.Width == 0)
                {
                        player.worldPosition.X = 0;
                        player.cameraPosition.X = 0;
                        player.worldPosition.Y = 0;
                        player.cameraPosition.Y = 0;
                        map.mapX = 0;
                        healthRec.Width = 100;
                }              
                if (player.worldPosition.Y >= graphics.GraphicsDevice.Viewport.Height)
                {

                        player.worldPosition.X = 0;
                        player.cameraPosition.X = 0;
                        player.worldPosition.Y = 0;
                        player.cameraPosition.Y = 0;
                        map.mapX = 0;
                }

            }

            else if (!StartScreen.Play.clicked)
            {
                StartScreen.Draw(spriteBatch);

            }*/
            base.Draw(gameTime);
        }

        public void Camera()
        {
            float cameraSpeed = 5f;
            if (player.cameraPosition.X > graphics.GraphicsDevice.Viewport.Width / 2)
            {
                map.mapX += (int)cameraSpeed;
                player.cameraPosition.X -= 5f;
            }

            if (player.worldPosition.X <= 0)
            {
                player.worldPosition.X = 0;
                player.cameraPosition.X = 0;
            }
            if (map.mapX < 0)
            {
                map.mapX = 0;
                player.cameraPosition.X -= 5f;
            }

            if (player.cameraPosition.X < graphics.GraphicsDevice.Viewport.Width / 2 && player.facing == "left")
            {
                player.cameraPosition.X += 5f;
                map.mapX -= (int)cameraSpeed;
            }
            if (map.mapX > map.MapW - graphics.GraphicsDevice.Viewport.Width)
            {
                map.mapX = map.MapW - graphics.GraphicsDevice.Viewport.Width;
                player.cameraPosition.X += 5f;
            }

            if (player.cameraPosition.X > graphics.GraphicsDevice.Viewport.Width)
            {
                player.cameraPosition.X -= 10f;
                player.worldPosition.X -= 10f;
 
            }
        }      

        public void Collision()
        {
                foreach (Rectangle top in map.blocksTop)
                {
                    if (top.Intersects(player.footBounds))
                    {
                        player.gravity = 0f;
                        player.state = "stand";
                        player.startY = player.worldPosition.Y;
                    }
                }

                foreach (Rectangle bottom in map.blocksBot)
                {
                    if (bottom.Intersects(player.topBounds))
                    {
                        player.worldPosition.Y += 8f;
                        player.cameraPosition.Y += 8f;
                    }
                }         

                foreach (Rectangle left in map.blocksLeft)
                {
                    if (left.Intersects(player.rightRec))
                    {
                        if (player.footBounds.Y >= left.Y)
                        {

                            player.worldPosition.X -= 5f;
                            player.cameraPosition.X -= 5f;
                        }
                    }
                }

                foreach (Rectangle right in map.blocksRight)
                {
                    if (right.Intersects(player.leftRec))
                    {
                        if (player.footBounds.Y >= right.Y)
                        {
                            player.worldPosition.X += 5f;
                            player.cameraPosition.X += 5f;
                        }
                    }
                }           
        }
    }
}
