using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller
{
    class HighScoreEntry
    {
        string text;

        float selectionFade;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public event EventHandler<PlayerIndexEventArgs> Selected;

        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }

        public HighScoreEntry(string text)
        {
            this.text = text;
        }

        public virtual void Update(HighScreen screen, bool isSelected,
                                                      GameTime gameTime)
        {
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
        }

        public virtual void Draw(HighScreen screen, Vector2 position,
                                 bool isSelected, GameTime gameTime)
        {
            Color color = isSelected ? Color.DarkGray : Color.Red;

            double time = gameTime.TotalGameTime.TotalSeconds;

            float pulsate = (float)Math.Sin(time * 6) + 1;

            float scale = 1 + pulsate * 0.05f * selectionFade; // changes size of entry text

            color = new Color(color.R, color.G, color.B, screen.TransitionAlpha);

            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, text, position, color, 0,
                                   origin, scale * 2, SpriteEffects.None, 0);
        }

        public virtual int GetHeight(HighScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }
    }
}
