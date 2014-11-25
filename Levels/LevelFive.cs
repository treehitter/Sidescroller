using System;
using System.Web;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SideScroller
{
    public class LevelFive : GameScreen
    {
        private static XmlSerializer xml = new XmlSerializer(typeof(MyData));
        private static XmlSerializer hxml = new XmlSerializer(typeof(HighData));
        public ContentManager content;
        public SpriteFont gameFont;
        Player player;
        public Stopwatch stopwatch;
        public Stopwatch colwatch;
        public Stopwatch breakstop;
        TileMap5 map;
        bool started;
        bool paused = false;
        public bool collided = false;
        Texture2D healthBar, healthBrack, DJReady, DJNReady;
        Texture2D emblem, emblemBrack;
        Vector2 healthPos;
        Rectangle healthRec, healthBracket, emblemRec, djRec;
        Vector2 FontPos;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Song music;
        KeyboardState keyStateOld;
        public int screenW = 1280;
        public int screenH = 720;
        public int min = 0;

        public LevelFive()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("Font/SpriteFont1");

            Thread.Sleep(1000);

            stopwatch = new Stopwatch();
            colwatch = new Stopwatch();
            breakstop = new Stopwatch();

            Texture2D tex = content.Load<Texture2D>("Guccitone");
            player = new Player();
            player.LoadContent(tex);

            map = new TileMap5();
            Texture2D tex1 = content.Load<Texture2D>("Tiles/in");
            Texture2D tex2 = content.Load<Texture2D>("Tiles/level1basic");
            Texture2D tex3 = content.Load<Texture2D>("Tiles/level1slowtile");
            Texture2D tex4 = content.Load<Texture2D>("Tiles/groundjumptile");
            Texture2D tex5 = content.Load<Texture2D>("Tiles/in");
            Texture2D tex6 = content.Load<Texture2D>("Tiles/floatingjumptile2");
            Texture2D tex7 = content.Load<Texture2D>("Tiles/level1slowtile");
            Texture2D tex8 = content.Load<Texture2D>("portal");
            Texture2D tex9 = content.Load<Texture2D>("Tiles/damageblock");
            Texture2D tex10 = content.Load<Texture2D>("Tiles/groundjumptile");
            Texture2D tex11 = content.Load<Texture2D>("Tiles/bridgetile");
            Texture2D tex12 = content.Load<Texture2D>("Tiles/anisouls");
            Texture2D tex13 = content.Load<Texture2D>("pillar");
            Texture2D tex14 = content.Load<Texture2D>("stalagmite");
            Texture2D tex15 = content.Load<Texture2D>("Animatedshard");
            Texture2D tex16 = content.Load<Texture2D>("bullet");
            map.LoadContent(tex1, tex2, tex3, tex4, tex5, tex6, tex7, tex8, tex9, tex10, tex11, tex12, tex13, tex14, tex15, tex16);

            FontPos = new Vector2(screenW / 2, screenH / 2);

            healthBar = content.Load<Texture2D>("HealthBar");
            healthBrack = content.Load<Texture2D>("healthbracket");

            healthPos = new Vector2(40, 30);
            healthRec = new Rectangle(0, 0, healthBar.Width, healthBar.Height);
            healthBracket = new Rectangle(40, 30, healthBrack.Width, healthBrack.Height);

            emblem = content.Load<Texture2D>("ShardEmblemLevel4");
            emblemBrack = content.Load<Texture2D>("ShardEmblemLevel3");
            emblemRec = new Rectangle(120, 120, emblem.Width, emblem.Height);

            //music = Content.Load<Song>("music/Music");
            MediaPlayer.IsRepeating = true;

            DJReady = content.Load<Texture2D>("djready");
            DJNReady = content.Load<Texture2D>("djnready");
            djRec = new Rectangle(1200, 30, DJReady.Width, DJReady.Height);

            scrolling1 = new Scrolling(content.Load<Texture2D>("backgroundlvl3"), new Rectangle(0, 0, screenW, screenH));
            scrolling2 = new Scrolling(content.Load<Texture2D>("backgroundlvl3"), new Rectangle(screenW, 0, screenW, screenH));

            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            KeyboardState key = Keyboard.GetState();

            if (IsActive)
            {
                if (player.worldPosition.Y >= screenH)
                {
                    LoadingScreen.Load(ScreenManager, false, null, new DeadBackgroundScreen(), new LevelFourDeadScreen());
                }
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
                    if (collided == true)
                    {
                        healthRec.Width -= 2;
                        if (healthRec.Width <= 0)
                        {
                            LoadingScreen.Load(ScreenManager, false, null, new DeadBackgroundScreen(), new LevelFourDeadScreen());
                        }
                    }
                    if (key.IsKeyDown(Keys.R) && !keyStateOld.IsKeyDown(Keys.R))
                    {
                        LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new LevelFour());
                    }
                    if (key.IsKeyDown(Keys.Escape) && !keyStateOld.IsKeyDown(Keys.Escape))
                    {
                        LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
                    }
                    stopwatch.Start();
                    if (colwatch.Elapsed.Seconds == 1)
                    {
                        colwatch.Stop();
                    }

                    map.Update(gameTime);
                    player.Update(gameTime);
                    Camera();
                    Collision(gameTime);
                }
                keyStateOld = key;
            }
        }
        [Serializable]
        public struct MyData
        {
            public string sec;
            public int minute;
            public int totalpick;
            public int finalpick;
        }
        [Serializable]
        public struct HighData
        {
            public double highsec1, highsec2, highsec3;
            public int highmin1, highmin2, highmin3;
        }
        public void Save(MyData data, string path)
        {
            data = LoadData(path);
            data.sec = stopwatch.Elapsed.Seconds.ToString();
            data.minute = min;
            data.totalpick = map.totalpc;
            data.finalpick = data.finalpick + data.totalpick;

            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(MyData));
                xml.Serialize(writer, data);
                writer.Close();
            }
        }
        public void SaveHigh(HighData data, string path)
        {
            double sec = stopwatch.Elapsed.Seconds;
            data = LoadHighData(path);
            if (sec <= data.highsec1)
            {
                if (min <= data.highmin1)
                {
                    data.highmin3 = data.highmin2;
                    data.highsec3 = data.highsec2;
                    data.highmin2 = data.highmin1;
                    data.highsec2 = data.highsec1;
                    data.highmin1 = min;
                }
                data.highsec1 = sec;
            }
            else if (sec <= data.highsec2)
            {
                if (min <= data.highmin2)
                {
                    data.highsec3 = data.highsec2;
                    data.highmin3 = data.highmin2;
                    data.highmin2 = min;
                }
                data.highsec2 = sec;
            }
            else if (sec <= data.highsec3)
            {
                if (min <= data.highmin3)
                    data.highmin3 = min;
                data.highsec3 = sec;
            }
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(HighData));
                xml.Serialize(writer, data);
                writer.Close();
            }
        }

        public static MyData LoadData(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return (MyData)xml.Deserialize(reader);
            }
        }
        public static HighData LoadHighData(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return (HighData)hxml.Deserialize(reader);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            string pause = "PAUSED";
            string healthPer = (int)(healthRec.Width / 1.5) + "%";

            if ((int)stopwatch.Elapsed.TotalSeconds >= 60)
            {
                min = 1;
            }
            if ((int)stopwatch.Elapsed.TotalSeconds >= 120)
            {
                min = 2;
            }
            if ((int)stopwatch.Elapsed.TotalSeconds >= 180)
            {
                min = 3;
            }

            Vector2 FontOrigin = gameFont.MeasureString(pause) / 2;

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            if (IsActive)
            {
                spriteBatch.Begin();
                scrolling1.Draw(spriteBatch);
                spriteBatch.End();

                map.Draw(spriteBatch);
                player.Draw(spriteBatch);

                spriteBatch.Begin();
                spriteBatch.Draw(healthBar, healthPos, healthRec, Color.White);
                spriteBatch.Draw(healthBrack, healthBracket, Color.White);
                if (map.ls == 1)
                {
                    spriteBatch.Draw(emblem, emblemRec, Color.White);
                }
                else
                    spriteBatch.Draw(emblemBrack, emblemRec, Color.White);
                if (stopwatch.Elapsed.Seconds < 10)
                {
                    spriteBatch.DrawString(gameFont, "0", new Vector2(screenW / 2 - 16, 10), Color.DarkGray);
                    spriteBatch.DrawString(gameFont, stopwatch.Elapsed.Seconds.ToString(), new Vector2(screenW / 2, 10), Color.DarkGray);
                }
                else if (stopwatch.Elapsed.Seconds > 9)
                {
                    spriteBatch.DrawString(gameFont, stopwatch.Elapsed.Seconds.ToString(), new Vector2(screenW / 2 - 16, 10), Color.DarkGray);
                }
                spriteBatch.DrawString(gameFont, min.ToString() + " :", new Vector2(screenW / 2 - 45, 10), Color.DarkGray);
                spriteBatch.DrawString(gameFont, "Souls: " + map.totalpc.ToString(), new Vector2(100, 80), Color.DarkGray); // pickup counter
                if (player.counter == 1)
                    spriteBatch.Draw(DJReady, djRec, Color.White);
                else
                    spriteBatch.Draw(DJNReady, djRec, Color.White);
                spriteBatch.End();

                if (paused)
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawString(gameFont, pause, FontPos, Color.Black, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    spriteBatch.End();
                }
            }


            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }

        public void Camera()
        {
            float cameraSpeed = 6f;
            if (player.cameraPosition.X > screenW / 2)
            {
                map.mapX += (int)cameraSpeed;
                player.cameraPosition.X -= 6f;
            }

            if (player.worldPosition.X <= 0)
            {
                player.worldPosition.X = 0;
                player.cameraPosition.X = 0;
            }
            if (map.mapX < 0)
            {
                map.mapX = 0;
                player.cameraPosition.X -= 6f;
            }

            if (player.cameraPosition.X < screenW / 2 && player.facing == "left")
            {
                player.cameraPosition.X += 6f;
                map.mapX -= (int)cameraSpeed;
            }

            if (map.mapX > map.MapW - screenW)
            {
                map.mapX = map.MapW - screenW;
                player.cameraPosition.X += 6f;
            }

            if (player.cameraPosition.X > screenW)
            {
                player.cameraPosition.X -= 12f;
                player.worldPosition.X -= 12f;

            }
        }

        public void Collision(GameTime gameTime)
        {
            MyData data = new MyData();
            HighData hdata = new HighData();
            data.sec = stopwatch.Elapsed.Seconds.ToString();
            data.minute = min;
            data.totalpick = map.totalpc;
            //// walking tiles
            foreach (Rectangle top in map.blocksTop)
            {
                Vector2 depth = RectangleExtensions.GetIntersectionDepth(player.footBounds, top);
                if (depth != Vector2.Zero)
                {
                    collided = false;
                    player.gravity = 0f;
                    player.state = "stand";
                    player.startY = player.worldPosition.Y;
                }
            }

            foreach (Rectangle bottom in map.blocksBot)
            {
                Vector2 depth = RectangleExtensions.GetIntersectionDepth(player.topBounds, bottom);
                if (depth != Vector2.Zero)
                {
                    player.bounceSpeed = 0f;
                    player.bounce = false;
                    player.state = "fall";
                    player.gravity = 6f;
                    player.worldPosition.Y += 8f;
                    player.cameraPosition.Y += 8f;
                    if (player.doubleJ)
                    {
                        player.gravity = 18f;
                    }
                }
            }

            foreach (Rectangle left in map.blocksLeft)
            {
                Vector2 depth = RectangleExtensions.GetIntersectionDepth(player.rightRec, left);
                if (depth != Vector2.Zero)
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        player.worldPosition.X -= 6f;
                        player.cameraPosition.X -= 6f;
                    }
                }
            }

            foreach (Rectangle right in map.blocksRight)
            {
                Vector2 depth = RectangleExtensions.GetIntersectionDepth(player.leftRec, right);
                if (depth != Vector2.Zero)
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        player.worldPosition.X += 6f;
                        player.cameraPosition.X += 6f;
                    }
                }
            }
            ///// damaging tile
            foreach (Rectangle top in map.spikesTop)
            {
                if (top.Intersects(player.footBounds))
                {
                    player.state = "damage";
                    player.gravity = 0f;
                    player.startY = player.worldPosition.Y;
                    collided = true;
                    colwatch.Start();
                }
                else if (player.footBounds.X > top.X)
                    collided = false;
                else if (player.state == "jump")
                    collided = false;
                else if (player.state == "fall")
                    collided = false;
            }

            foreach (Rectangle bottom in map.spikesBot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    collided = true;
                    player.worldPosition.Y += 8f;
                    player.cameraPosition.Y += 8f;
                }
            }

            foreach (Rectangle left in map.spikesLeft)
            {
                if (left.Intersects(player.rightRec))
                {
                    collided = true;
                    if (player.footBounds.Y >= left.Y)
                    {
                        player.worldPosition.X -= 6f;
                        player.cameraPosition.X -= 6f;
                    }
                }
            }

            foreach (Rectangle right in map.spikesRight)
            {
                if (right.Intersects(player.leftRec))
                {
                    collided = true;
                    if (player.footBounds.Y >= right.Y)
                    {
                        player.worldPosition.X += 6f;
                        player.cameraPosition.X += 6f;
                    }
                }
            }
            ///// bounce tile
            foreach (Rectangle top in map.bounceTop)
            {
                Vector2 depth = RectangleExtensions.GetIntersectionDepth(player.footBounds, top);
                if (depth != Vector2.Zero)
                {
                    player.gravity = 0f;
                    player.bounce = true;
                    player.bounceSpeed -= 25f;

                    if (player.bounceSpeed <= 25f)
                    {
                        player.gravity = 6f;
                    }

                }
            }
            foreach (Rectangle bottom in map.bounceBot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    player.bounceSpeed = 0f;
                    player.bounce = false;
                    player.state = "fall";
                    player.worldPosition.Y += 8f;
                    player.cameraPosition.Y += 8f;
                    if (player.doubleJ)
                    {
                        player.worldPosition.Y += 13f;
                        player.cameraPosition.Y += 13f;
                    }
                }
            }

            foreach (Rectangle left in map.bounceLeft)
            {
                Vector2 depth = RectangleExtensions.GetIntersectionDepth(player.rightRec, left);
                if (depth != Vector2.Zero)
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        player.worldPosition.X -= 6f;
                        player.cameraPosition.X -= 6f;
                    }
                    if (player.jump)
                    {
                        player.worldPosition.X -= 8f;
                        player.cameraPosition.X -= 8f;
                    }
                }
            }

            foreach (Rectangle right in map.bounceRight)
            {
                Vector2 depth = RectangleExtensions.GetIntersectionDepth(player.leftRec, right);
                if (depth != Vector2.Zero)
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        player.worldPosition.X += 6f;
                        player.cameraPosition.X += 6f;
                    }
                    if (player.jump)
                    {
                        player.worldPosition.X += 8f;
                        player.cameraPosition.X += 8f;
                    }
                }
            }
            //// slow tile
            foreach (Rectangle top in map.slowTop)
            {
                if (top.Intersects(player.footBounds))
                {
                    player.gravity = 0f;
                    player.state = "stand";
                    player.startY = player.worldPosition.Y;
                    player.speed = 2f;

                    colwatch.Restart();
                }
                else if (player.footBounds.X > top.X && colwatch.Elapsed.Seconds == 1)
                    player.speed = 6f;
                else if (player.footBounds.X < top.X && colwatch.Elapsed.Seconds == 1)
                    player.speed = 6f;
                else if (player.state == "jump" && colwatch.Elapsed.Seconds == 1)
                    player.speed = 6f;
                else if (player.state == "fall" && colwatch.Elapsed.Seconds == 1)
                    player.speed = 6f;

            }
            foreach (Rectangle bottom in map.slowBot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    player.startY = player.worldPosition.Y;
                    player.worldPosition.Y += 8f;
                    player.cameraPosition.Y += 8f;
                }
            }

            foreach (Rectangle left in map.slowLeft)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        player.startY = player.worldPosition.Y;
                        player.worldPosition.X -= 6f;
                        player.cameraPosition.X -= 6f;
                    }
                }
            }

            foreach (Rectangle right in map.slowRight)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        player.startY = player.worldPosition.Y;
                        player.worldPosition.X += 6f;
                        player.cameraPosition.X += 6f;
                    }
                }
            }

            ////shards
            foreach (Rectangle top in map.shardTop)
            {
                if (top.Intersects(player.footBounds))
                {
                    if (map.hit = true && map.ls == 1)
                    {
                        LoadData("leveldata.xml");
                        LoadHighData("highscore4.xml");
                        SaveHigh(hdata, "highscore4.xml");
                        Save(data, "leveldata.xml");
                        LoadingScreen.Load(ScreenManager, false, null, new LevelCompleteBackgroundScreen(), new LevelFourCompleteScreen());
                    }
                    else
                        LoadingScreen.Load(ScreenManager, false, null, new DeadBackgroundScreen(), new LevelFourFailedScreen());
                }
            }

            foreach (Rectangle bottom in map.shardBot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    if (map.hit = true && map.ls == 1)
                    {
                        LoadData("leveldata.xml");
                        LoadHighData("highscore4.xml");
                        SaveHigh(hdata, "highscore4.xml");
                        Save(data, "leveldata.xml");
                        LoadingScreen.Load(ScreenManager, false, null, new LevelCompleteBackgroundScreen(), new LevelFourCompleteScreen());
                    }
                    else
                        LoadingScreen.Load(ScreenManager, false, null, new DeadBackgroundScreen(), new LevelFourFailedScreen());
                }
            }

            foreach (Rectangle left in map.shardLeft)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        if (map.hit = true && map.ls == 1)
                        {
                            LoadData("leveldata.xml");
                            LoadHighData("highscore4.xml");
                            SaveHigh(hdata, "highscore4.xml");
                            Save(data, "leveldata.xml");
                            LoadingScreen.Load(ScreenManager, false, null, new LevelCompleteBackgroundScreen(), new LevelFourCompleteScreen());
                        }
                        else
                        {

                            LoadingScreen.Load(ScreenManager, false, null, new DeadBackgroundScreen(), new LevelFourFailedScreen());
                        }
                    }
                }
            }
            foreach (Rectangle right in map.shardRight)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        if (map.hit = true && map.ls == 1)
                        {
                            LoadData("leveldata.xml");
                            LoadHighData("highscore4.xml");
                            SaveHigh(hdata, "highscore4.xml");
                            Save(data, "leveldata.xml");
                            LoadingScreen.Load(ScreenManager, false, null, new LevelCompleteBackgroundScreen(), new LevelFourCompleteScreen());
                        }
                        else
                            LoadingScreen.Load(ScreenManager, false, null, new DeadBackgroundScreen(), new LevelFourFailedScreen());

                    }
                }
            }
            ///bridge
            KeyboardState key = Keyboard.GetState();
            foreach (Rectangle top in map.bridgeTop)
            {
                if (top.Intersects(player.footBounds))
                {
                    player.gravity = 0f;
                    player.state = "stand";
                    player.startY = player.worldPosition.Y;
                    if (key.IsKeyDown(Keys.S) && !keyStateOld.IsKeyDown(Keys.S))
                    {
                        player.state = "fall";
                        player.gravity = 24f;
                    }
                }
            }
            ////level shards
            foreach (Rectangle top in map.levelshardTop)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.ls = 1;
                    map.shardhit = true;
                }
            }

            foreach (Rectangle bottom in map.levelshardBot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.ls = 1;
                    map.shardhit = true;
                }
            }

            foreach (Rectangle left in map.levelshardLeft)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.ls = 1;
                        map.shardhit = true;
                    }
                }
            }
            foreach (Rectangle right in map.levelshardRight)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.ls = 1;
                        map.shardhit = true;
                    }
                }
            }
            //pick up
            foreach (Rectangle top in map.pickupTop)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc1 = 4;
                    map.pickuphit = true;
                }
            }
            foreach (Rectangle bottom in map.pickupBot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc1 = 4;
                    map.pickuphit = true;
                }
            }
            foreach (Rectangle left in map.pickupLeft)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc1 = 4;
                        map.pickuphit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickupRight)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc1 = 4;
                        map.pickuphit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup2Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc2 = 4;
                    map.pickup2hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup2Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc2 = 4;
                    map.pickup2hit = true;
                }
            }
            foreach (Rectangle left in map.pickup2Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc2 = 4;
                        map.pickup2hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup2Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc2 = 4;
                        map.pickup2hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup3Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc3 = 4;
                    map.pickup3hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup3Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc3 = 4;
                    map.pickup3hit = true;
                }
            }
            foreach (Rectangle left in map.pickup3Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc3 = 4;
                        map.pickup3hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup3Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc3 = 4;
                        map.pickup3hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup4Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc4 = 4;
                    map.pickup4hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup4Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc4 = 4;
                    map.pickup4hit = true;
                }
            }
            foreach (Rectangle left in map.pickup4Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc4 = 4;
                        map.pickup4hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup4Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc4 = 4;
                        map.pickup4hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup5Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc5 = 4;
                    map.pickup5hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup5Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc5 = 4;
                    map.pickup5hit = true;
                }
            }
            foreach (Rectangle left in map.pickup5Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc5 = 4;
                        map.pickup5hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup5Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc5 = 4;
                        map.pickup5hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup6Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc6 = 4;
                    map.pickup6hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup6Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc6 = 4;
                    map.pickup6hit = true;
                }
            }
            foreach (Rectangle left in map.pickup6Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc6 = 4;
                        map.pickup6hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup6Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc6 = 4;
                        map.pickup6hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup7Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc7 = 4;
                    map.pickup7hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup7Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc7 = 4;
                    map.pickup7hit = true;
                }
            }
            foreach (Rectangle left in map.pickup7Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc7 = 4;
                        map.pickup7hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup7Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc7 = 4;
                        map.pickup7hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup8Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc8 = 4;
                    map.pickup8hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup8Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc8 = 4;
                    map.pickup8hit = true;
                }
            }
            foreach (Rectangle left in map.pickup8Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc8 = 4;
                        map.pickup8hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup8Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc8 = 4;
                        map.pickup8hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup9Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc9 = 4;
                    map.pickup9hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup9Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc9 = 4;
                    map.pickup9hit = true;
                }
            }
            foreach (Rectangle left in map.pickup9Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc9 = 4;
                        map.pickup9hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup9Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc9 = 4;
                        map.pickup9hit = true;
                    }
                }
            }
            foreach (Rectangle top in map.pickup10Top)
            {
                if (top.Intersects(player.footBounds))
                {
                    map.pc10 = 4;
                    map.pickup10hit = true;
                }
            }
            foreach (Rectangle bottom in map.pickup10Bot)
            {
                if (bottom.Intersects(player.topBounds))
                {
                    map.pc10 = 4;
                    map.pickup10hit = true;
                }
            }
            foreach (Rectangle left in map.pickup10Left)
            {
                if (left.Intersects(player.rightRec))
                {
                    if (player.footBounds.Y >= left.Y)
                    {
                        map.pc10 = 4;
                        map.pickup10hit = true;
                    }
                }
            }
            foreach (Rectangle right in map.pickup10Right)
            {
                if (right.Intersects(player.leftRec))
                {
                    if (player.footBounds.Y >= right.Y)
                    {
                        map.pc10 = 4;
                        map.pickup10hit = true;
                    }
                }
            }
            map.totalpc = map.pc1 + map.pc2 + map.pc3 + map.pc4 + map.pc5 + map.pc6 + map.pc7 + map.pc8 + map.pc9 + map.pc10;
        }
    }
}