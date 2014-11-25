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
    public enum AnimationState
    {
        Running,
        Paused,
        Stopped
    }


    class Animation
    {
        public AnimationState state = AnimationState.Stopped;
        public string name = string.Empty;
        public Texture2D texture = null;
        public List<Rectangle> frames = new List<Rectangle>();
        public Vector2 origin = Vector2.Zero;
        public float framesPerSecond = 0.0f;
        public bool isLooping = false;
        public int frameCount = 0;
        public int currentFrame = 0;
        public float totalElapsedTime = 0.0f;

        public Animation(string name, Texture2D texture, List<Rectangle> frames, Vector2 origin, float framesPerSecond, bool isLooping)
        {
            this.name = name;
            this.texture = texture;
            this.frames = frames;
            this.origin = origin;
            this.framesPerSecond = 1 / framesPerSecond;
            this.isLooping = isLooping;
            frameCount = frames.Count;
        }

        public void Play()
        {
            resetAnimation();
            setAnimationStateTo(AnimationState.Running);
        }

        public void Pause()
        {
            setAnimationStateTo(AnimationState.Paused);
        }

        public void Resume()
        {
            setAnimationStateTo(AnimationState.Running);
        }

        public void Stop()
        {
            resetAnimation();
            setAnimationStateTo(AnimationState.Stopped);
        }

        public void Update(GameTime gameTime)
        {
            if (state == AnimationState.Running)
            {
                runAnimation(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 screenPosition)
        {
            spriteBatch.Draw(texture, screenPosition, frames[currentFrame], Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

        }

        private void setAnimationStateTo(AnimationState state)
        {
            this.state = state;
        }

        private void resetAnimation()
        {
            currentFrame = 0;
        }

        private void runAnimation(GameTime gameTime)
        {
            if (isItTimeToAdvanceAFrame(gameTime))
            {
                advanceToNextValidFrame();
            }
        }

        private bool isItTimeToAdvanceAFrame(GameTime gameTime)
        {
            float elapseTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalElapsedTime += elapseTime;
            if (totalElapsedTime > framesPerSecond)
            {
                totalElapsedTime -= framesPerSecond;
                return true;
            }
            return false;
        }

        private void advanceToNextValidFrame()
        {
            currentFrame++;
            if (isLooping)
            {
                currentFrame = currentFrame % frameCount;
            }
            else if (currentFrame >= frameCount)
            {
                currentFrame = (frameCount - 1);
            }
        }


        public AnimationState State
        {
            get { return state; }
        }

        public string Name
        {
            get { return name; }
        }

        public bool IsLooping
        {
            get { return isLooping; }

        }
    }
}
