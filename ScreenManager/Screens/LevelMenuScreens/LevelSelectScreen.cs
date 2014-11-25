using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SideScroller
{
    class LevelSelectScreen : LevelMenuScreen
    {
        public LevelSelectScreen()
            : base("")
        {
            LevelMenuEntry levelOneMenuEntry = new LevelMenuEntry("Level One");
            LevelMenuEntry levelTwoMenuEntry = new LevelMenuEntry("Level Two");
            LevelMenuEntry levelThreeMenuEntry = new LevelMenuEntry("Level Three");
            LevelMenuEntry levelFourMenuEntry = new LevelMenuEntry("Level Four");
            LevelMenuEntry levelFiveMenuEntry = new LevelMenuEntry("Level Five");
            LevelMenuEntry mainMenuMenuEntry = new LevelMenuEntry("Main Menu");
            LevelMenuEntry exitMenuEntry = new LevelMenuEntry("Exit");

            levelOneMenuEntry.Selected += LevelOneMenuEntrySelected;
            levelTwoMenuEntry.Selected += LevelTwoMenuEntrySelected;
            levelThreeMenuEntry.Selected += LevelThreeMenuEntrySelected;
            levelFourMenuEntry.Selected += LevelFourMenuEntrySelected;
            levelFiveMenuEntry.Selected += LevelFiveMenuEntrySelected;
            mainMenuMenuEntry.Selected += MainMenuMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(levelOneMenuEntry);
            MenuEntries.Add(levelTwoMenuEntry);
            MenuEntries.Add(levelThreeMenuEntry);
            MenuEntries.Add(levelFourMenuEntry);
            MenuEntries.Add(levelFiveMenuEntry);
            MenuEntries.Add(mainMenuMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        void LevelOneMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new LevelOne());
        }

        void LevelTwoMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new LevelTwo());
        }
        void LevelThreeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new LevelThree());
        }
        void LevelFourMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new LevelFour());
        }
        void LevelFiveMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new LevelFive());
        }

        void MainMenuMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }
    }
}
