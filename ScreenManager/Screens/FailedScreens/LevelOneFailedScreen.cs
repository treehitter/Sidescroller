using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SideScroller
{
    class LevelOneFailedScreen : MenuScreen
    {
        public LevelOneFailedScreen()
            : base("")
        {
            MenuEntry restartLevelMenuEntry = new MenuEntry("Restart Level");
            MenuEntry levelSelectMenuEntry = new MenuEntry("Level Select");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            restartLevelMenuEntry.Selected += restartLevelMenuEntrySelected;
            levelSelectMenuEntry.Selected += levelSelectMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(restartLevelMenuEntry);
            MenuEntries.Add(levelSelectMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        void restartLevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new LevelOne());
        }

        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        void levelSelectMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new LevelSelectBackground(), new LevelSelectScreen());
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

    }
}
