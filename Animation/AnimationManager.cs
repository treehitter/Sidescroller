using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SideScroller
{
    class AnimationManager
    {
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private string currentAnimation = string.Empty;

        public AnimationManager()
        {
        }

        public void Add(string name, Texture2D texture, int rows, int columns, float framesPerSecond, bool isLooping)
        {
            List<Rectangle> frames = getFramesFromTexture(texture, rows, columns);
            Vector2 origin = getFramesOrigin(texture, rows, columns);
            Animation animation = new Animation(name, texture, frames, origin, framesPerSecond, isLooping);
            animations.Add(name, animation);
        }

        public bool SetAnimationTo(string name)
        {
            if (animations.ContainsKey(name))
            {
                if (currentAnimation != string.Empty)
                {
                    animations[currentAnimation].Pause();
                }
                currentAnimation = name;
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            animations[currentAnimation].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 screenPosition)
        {
            animations[currentAnimation].Draw(spriteBatch, screenPosition);
        }

        private List<Rectangle> getFramesFromTexture(Texture2D texture, int rows, int columns)
        {
            List<Rectangle> frames = new List<Rectangle>();
            int frameWidth = (int)getFrameDimentions(texture, rows, columns).X;
            int frameHeight = (int)getFrameDimentions(texture, rows, columns).Y;
            int yPosition = 0;
            int xPosition = 0;

            for (int row = 0; row < rows; row++)
            {
                yPosition = row * frameHeight;
                for (int column = 0; column < columns; column++)
                {
                    xPosition = column * frameWidth;
                    frames.Add(new Rectangle(xPosition, yPosition, frameWidth, frameHeight));
                }
            }
            return frames;
        }

        private Vector2 getFramesOrigin(Texture2D texture, int rows, int columns)
        {
            int frameWidth = (int)getFrameDimentions(texture, rows, columns).X;
            int frameHeight = (int)getFrameDimentions(texture, rows, columns).Y;
            return new Vector2(frameWidth / 2, frameHeight / 2);
        }

        private Vector2 getFrameDimentions(Texture2D texture, int rows, int columns)
        {
            return new Vector2(texture.Width / columns, texture.Height / rows);
        }

        public Animation CurrentAnimation
        {
            get { return animations[currentAnimation]; }
        }


    }
}
