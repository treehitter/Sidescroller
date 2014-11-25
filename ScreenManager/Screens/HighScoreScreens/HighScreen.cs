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
    public abstract class HighScreen : GameScreen
    {
        KeyboardState keyStateOld;
        private static XmlSerializer xml = new XmlSerializer(typeof(HighData));
        public struct HighData
        {
            public double highsec1, highsec2, highsec3;
            public int highmin1, highmin2, highmin3;
        }

        List<LevelCompMenuEntry> menuEntries = new List<LevelCompMenuEntry>();
        public int selectedEntry = 0;
        public string menuTitle, value;
        HighData newData = LoadData("highscore1.xml");
        HighData newData2 = LoadData("highscore2.xml");
        HighData newData3 = LoadData("highscore3.xml");
        HighData newData4 = LoadData("highscore4.xml");

        public static HighData LoadData(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return (HighData)xml.Deserialize(reader);
            }
        }
        public IList<LevelCompMenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        public HighScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Escape) && !keyStateOld.IsKeyDown(Keys.Escape))
            {
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
            }
            keyStateOld = key;
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            Vector2 position = new Vector2(512, 400); // changes placement of each menu entry

            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            if (ScreenState == ScreenState.TransitionOn)
                position.X -= transitionOffset * 256;
            else
                position.X += transitionOffset * 512;

            spriteBatch.Begin();


            Vector2 titlePosition = new Vector2(640, 80);          /// change title position
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            Color titleColor = new Color(192, 192, 192, TransitionAlpha); /// change title color
            float titleScale = 5.25f; // changes size of title

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuTitle, titlePosition, Color.Red, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, "Level One", new Vector2(90, 30), Color.Red);
            if (newData.highsec1 < 10)
                spriteBatch.DrawString(font, newData.highmin1 + ":0" + newData.highsec1, new Vector2(60, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData.highmin1 + ":" + newData.highsec1, new Vector2(60, 60), Color.Red);
            if(newData.highsec2 < 10)
                spriteBatch.DrawString(font, newData.highmin2 + ":0" + newData.highsec2, new Vector2(120, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData.highmin2 + ":" + newData.highsec2, new Vector2(120, 60), Color.Red);
            if(newData.highsec3 < 10)
                spriteBatch.DrawString(font, newData.highmin3 + ":0" + newData.highsec3, new Vector2(180, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData.highmin3 + ":" + newData.highsec3, new Vector2(180, 60), Color.Red);

            spriteBatch.DrawString(font, "Level Two", new Vector2(390, 30), Color.Red);
            if(newData2.highsec1 < 10)
                spriteBatch.DrawString(font, newData2.highmin1 + ":0" + newData2.highsec1, new Vector2(360, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData2.highmin1 + ":" + newData2.highsec1, new Vector2(360, 60), Color.Red);
            if(newData2.highsec2 < 10)
                spriteBatch.DrawString(font, newData2.highmin2 + ":0" + newData2.highsec2, new Vector2(420, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData2.highmin2 + ":" + newData2.highsec2, new Vector2(420, 60), Color.Red);
            if(newData2.highsec3 < 10)
                spriteBatch.DrawString(font, newData2.highmin3 + ":0" + newData2.highsec3, new Vector2(480, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData2.highmin3 + ":" + newData2.highsec3, new Vector2(480, 60), Color.Red);

            spriteBatch.DrawString(font, "Level Three", new Vector2(630, 30), Color.Red);
            if(newData3.highsec1 < 10)
                spriteBatch.DrawString(font, newData3.highmin1 + ":0" + newData3.highsec1, new Vector2(600, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData3.highmin1 + ":" + newData3.highsec1, new Vector2(600, 60), Color.Red);
            if(newData3.highsec2 < 10)
                spriteBatch.DrawString(font, newData3.highmin2 + ":0" + newData3.highsec2, new Vector2(660, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData3.highmin2 + ":" + newData3.highsec2, new Vector2(660, 60), Color.Red);
            if(newData3.highsec3 < 10)
                spriteBatch.DrawString(font, newData3.highmin3 + ":0" + newData3.highsec3, new Vector2(720, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData3.highmin3 + ":" + newData3.highsec3, new Vector2(720, 60), Color.Red);

            spriteBatch.DrawString(font, "Level Four", new Vector2(870, 30), Color.Red);
            if(newData4.highsec1 < 10)
                spriteBatch.DrawString(font, newData4.highmin1 + ":0" + newData4.highsec1, new Vector2(840, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData4.highmin1 + ":" + newData4.highsec1, new Vector2(840, 60), Color.Red);
            if(newData4.highsec2 < 10)
                spriteBatch.DrawString(font, newData4.highmin2 + ":0" + newData4.highsec2, new Vector2(900, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData4.highmin2 + ":" + newData4.highsec2, new Vector2(900, 60), Color.Red);
            if(newData4.highsec3 < 10)
                spriteBatch.DrawString(font, newData4.highmin3 + ":0" + newData4.highsec3, new Vector2(960, 60), Color.Red);
            else
                spriteBatch.DrawString(font, newData4.highmin3 + ":" + newData4.highsec3, new Vector2(960, 60), Color.Red);

            spriteBatch.End();
        }
    }
}

