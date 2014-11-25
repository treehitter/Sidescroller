 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SideScroller
{
    class Player
    { 
        public bool doubleJ = false;
        public float doubleJSpeed;
        public int counter = 0;

        Texture2D texture;
        public Stopwatch stopwatch, bouncewatch;
        public Vector2 cameraPosition;
        public Vector2 worldPosition;
        public Vector2 playerVelocity;
        public Rectangle rectangle;
        public Rectangle footBounds;
        public Rectangle leftRec;
        public Rectangle rightRec;
        public Rectangle topBounds;
        KeyboardState keystate;
        KeyboardState oldkeys;
        public string facing;
        public string state;
        public bool jump, bounce, walk;
        public float jumpSpeed, bounceSpeed;
        public float gravity;
        public float startY;
        public float speed;
        public float interval;
        float time;
        public string prevstate;
        Point frameSize;
        Point currentFrame;
        public Rectangle source;

        public Player()
        {
            cameraPosition = new Vector2(0, 0);
            worldPosition = new Vector2(0, 0);
            playerVelocity = new Vector2(0, 0);
            stopwatch = new Stopwatch();
            bouncewatch = new Stopwatch();
            startY = worldPosition.Y;
            facing = "right";
            prevstate = state;
            state = "stand";
            bounce = false;
            jump = false;
            gravity = 0f;
            speed = 6f;
            currentFrame = new Point(0, 0);
            time = 0f;
            interval = 200f;
        }

        public void LoadContent(Texture2D tex)
        {
            texture = tex;
        }

        public void Update(GameTime gt)
        {

            time += (float)gt.ElapsedGameTime.TotalMilliseconds;
            oldkeys = keystate;
            keystate = Keyboard.GetState();
            cameraPosition.Y += gravity;
            worldPosition.Y += gravity;

            if (keystate.IsKeyDown(Keys.D))
            {
                cameraPosition.X += speed;
                worldPosition.X += speed;
                playerVelocity.X += speed;

                facing = "right";                             
            }

            if (keystate.IsKeyDown(Keys.A))
            {
                cameraPosition.X -= speed;
                worldPosition.X -= speed;
                playerVelocity.X -= speed;
                
                facing = "left";                              
            }

            if (oldkeys.IsKeyDown(Keys.A) && !keystate.IsKeyDown(Keys.A))
            {
                state = "stand";
            }

            if (oldkeys.IsKeyDown(Keys.D) && !keystate.IsKeyDown(Keys.D))
            {
                state = "stand";
            }
      
            if (state == "stand")
            {
         
                if (keystate.IsKeyDown(Keys.A))
                {
                    state = "walk";
                    MoveAnimation(gt);
                }
                if (keystate.IsKeyDown(Keys.D))
                {
                    state = "walk";
                    MoveAnimation(gt);
                }
            }
            if (state == "damage")
            {

                if (keystate.IsKeyDown(Keys.A))
                {
                    state = "damagewalk";
                    DamageAnimation(gt);
                }
                if (keystate.IsKeyDown(Keys.D))
                {
                    state = "damagewalk";
                    DamageAnimation(gt);
                }
            }
            if (state == "damagewalk")
            {
                gravity = 6f;
                walk = true;
                DamageAnimation(gt);
            }
            if (state == "walk")
            {
                gravity = 6f;
                walk = true;
            }
            if (state == "stand")
            {
                gravity = 6f;
                StandAnimation(gt);
            }

            //double jumper counter + 1
            stopwatch.Start();
            if (stopwatch.Elapsed.TotalSeconds >= 1.5)
            {
                counter = 1;
            }

            if (bounce)
            {
                bouncewatch.Start();
                state = "jump";
                cameraPosition.Y += bounceSpeed;
                worldPosition.Y += bounceSpeed;
                bounceSpeed += 1f;
                if (doubleJ)
                {
                    state = "jump";
                    cameraPosition.Y += 0f;
                    worldPosition.Y += 0f;
                    doubleJSpeed += 1f;
                    counter = 0;
                    if (doubleJSpeed == 0)
                    {
                        doubleJSpeed = 0;
                        doubleJ = false;
                        state = "fall";
                    }
                }
                
                if (bounceSpeed == 0)
                {
                    bounceSpeed = 0;
                    bounce = false;
                    state = "fall";
                }
            }

            if (jump)
            {
                state = "jump";
                cameraPosition.Y += jumpSpeed;
                worldPosition.Y += jumpSpeed;
                jumpSpeed += 1f;
                if (jumpSpeed == 0)
                {
                    jumpSpeed = 0;
                    jump = false;
                    state = "fall";
                }

                if (doubleJ)
                {                    
                    state = "jump";
                    cameraPosition.Y += 0f;
                    worldPosition.Y += 0f;
                    doubleJSpeed += 1f;
                    counter = 0;
                    if (doubleJSpeed == 0)
                    {
                        doubleJSpeed = 0;
                        doubleJ = false;
                        state = "fall";
                    }
                }
            } 

            if (doubleJ)
            {
                    state = "jump";
                    cameraPosition.Y += doubleJSpeed;
                    worldPosition.Y += doubleJSpeed;
                    doubleJSpeed += 1f;
                    counter = 0;
                    if (doubleJSpeed == 0)
                    {
                        doubleJSpeed = 0;
                        doubleJ = false;
                        state = "fall";
                    }                    
            }
           
            else
            {
                //if double jump counter equals 1, double jump is ready
                if (counter == 1)
                {
                    if (keystate.IsKeyDown(Keys.Space) && !oldkeys.IsKeyDown(Keys.Space))
                    {
                        doubleJ = true;
                        doubleJSpeed -= 21f;
                        gravity = 0f;
                        stopwatch.Reset();
                    }
                }
                    if (worldPosition.Y == startY)
                    {
                        if (keystate.IsKeyDown(Keys.W) && !oldkeys.IsKeyDown(Keys.W))
                        {
                            jumpSpeed -= 18f;
                            jump = true;
                            //gravity = 0f;
                        }
                    }               
            }

            if (state == "fall")
            {
                gravity = 6f;
                FallAnimation();
            }
            if (state == "damage")
            {
                gravity = 6f;
                DamageAnimation(gt);
            }
            if (state == "jump")
            {
                JumpAnimation();
            }
            if (worldPosition.Y > startY)
            {
                state = "fall";
            }
        
            rectangle = new Rectangle((int)worldPosition.X, (int)worldPosition.Y, 20, 25);

            footBounds = new Rectangle(rectangle.Center.X , rectangle.Center.Y , rectangle.Width , rectangle.Height / 2);
            rightRec = new Rectangle(rectangle.Center.X , rectangle.Y , rectangle.Width / 2, rectangle.Height);
            leftRec = new Rectangle(rectangle.Left, rectangle.Y, 1, rectangle.Height);
            topBounds = new Rectangle(rectangle.Center.X, rectangle.Center.Y, rectangle.Width, rectangle.Height);
            oldkeys = keystate;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (facing == "right")
            {
                spriteBatch.Draw(texture,cameraPosition,source,Color.White,0f,new Vector2(rectangle.Width / 2 ,rectangle.Height / 2) ,1.0f ,SpriteEffects.FlipHorizontally, 0);
            }

            if (facing == "left")
            {
                spriteBatch.Draw(texture, cameraPosition, source, Color.White, 0f, new Vector2(rectangle.Width / 2, rectangle.Height / 2), 1.0f, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }

        public void MoveAnimation(GameTime gt)
        {
            time += (float)gt.ElapsedGameTime.TotalMilliseconds;
            currentFrame.Y = 3;            
            
            frameSize = new Point(33, 33);
            if (time > interval)
            {
                currentFrame.X++;
                if (currentFrame.X >= 4)
                {
                    currentFrame.X = 0;
                }
                source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
                time = 0f;
            }
        }

        public void JumpAnimation()
        {
            currentFrame.Y = 2;
            currentFrame.X = 0;
            frameSize = new Point(33, 33);
            source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);

        }

        public void FallAnimation()
        {
            currentFrame.Y = 0;
            currentFrame.X = 0;
            frameSize = new Point(33, 33);
            source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
        }
        public void StandAnimation(GameTime gt)
        {
            time += (float)gt.ElapsedGameTime.TotalSeconds;
            currentFrame.Y = 4;

            frameSize = new Point(33, 33);
            if (time > interval)
            {
                currentFrame.X++;
                if (currentFrame.X >= 7)
                {
                    currentFrame.X = 0;
                }
                source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
                time = 0f;
            }
        }
        public void DamageAnimation(GameTime gt)
        {
            time += (float)gt.ElapsedGameTime.TotalMilliseconds;
            currentFrame.Y = 1;

            frameSize = new Point(33, 33);
            if (time > interval)
            {
                currentFrame.X++;
                if (currentFrame.X >= 10)
                {
                    currentFrame.X = 0;
                }
                source = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
                time = 0f;
            }
        }
    }    
}
