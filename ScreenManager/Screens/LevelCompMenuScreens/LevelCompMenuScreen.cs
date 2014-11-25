using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.IO;
namespace SideScroller
{   
    public abstract class LevelCompMenuScreen : GameScreen
    {
        private static XmlSerializer xml = new XmlSerializer(typeof(MyData));
        public struct MyData
        {
            public string sec;
            public int minute;
            public int totalpick;
            public int finalpick;
        }

        List<LevelCompMenuEntry> menuEntries = new List<LevelCompMenuEntry>();
        public int selectedEntry = 0;
        public string menuTitle, value;
        MyData newData = LoadData("leveldata.xml");

        public static MyData LoadData(string path) 
 	    { 
 	        using (StreamReader reader = new StreamReader(path)) 
 	        {
 	            return (MyData)xml.Deserialize(reader); 
 	        } 
 	    }
        public IList<LevelCompMenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        public LevelCompMenuScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;
            
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void HandleInput(InputState input)
        {
            if (input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            if (input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                OnCancel(playerIndex);
            }
        }

        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            menuEntries[selectedEntry].OnSelectEntry(playerIndex);
        }

        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            MyData newData = LoadData("leveldata.xml");

            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }
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

            for (int i = 0; i < menuEntries.Count; i++)
            {
                LevelCompMenuEntry menuEntry = menuEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);

                menuEntry.Draw(this, position, isSelected, gameTime);

                position.Y += menuEntry.GetHeight(this) + 20; /// change width in between each menu entry
            }

            Vector2 titlePosition = new Vector2(640, 80);          /// change title position
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            Color titleColor = new Color(192, 192, 192, TransitionAlpha); /// change title color
            float titleScale = 5.25f; // changes size of title

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuTitle, titlePosition, Color.Red, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, "Completed in: " + newData.minute + "m " + newData.sec + "s", new Vector2(512, 300), Color.Red);
            spriteBatch.DrawString(font, "Souls picked up: " + newData.totalpick, new Vector2(512, 325), Color.Red);
            spriteBatch.DrawString(font, "Total Souls: " + newData.finalpick, new Vector2(512, 350), Color.Red);
            
            spriteBatch.End();
        }
    }
}
