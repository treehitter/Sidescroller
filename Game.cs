using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SideScroller
{
    public class SideScrollerGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ScreenManager screenManager;

        public SideScrollerGame()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            this.graphics.IsFullScreen = true;

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }

    static class Program
    {
        static void Main()
        {
            using (SideScrollerGame game = new SideScrollerGame())
            {
                game.Run();
            }
        }
    }
}
