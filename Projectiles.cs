using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SideScroller
{
    class Projectiles
    {
        public Texture2D projText;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;
        public bool isVisible;

        public Projectiles(Texture2D texture)
        {
            projText = texture;
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(projText, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 1);
        }
    }
}
