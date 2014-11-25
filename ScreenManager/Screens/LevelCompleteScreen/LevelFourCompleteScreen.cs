using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SideScroller
{
    class LevelFourCompleteScreen : LevelCompMenuScreen
    {
        public LevelFourCompleteScreen()
            : base("")
        {
            LevelCompMenuEntry levelSelectMenuEntry = new LevelCompMenuEntry("Level Select");
            LevelCompMenuEntry optionsMenuEntry = new LevelCompMenuEntry("Options");
            LevelCompMenuEntry exitMenuEntry = new LevelCompMenuEntry("Exit");

            levelSelectMenuEntry.Selected += LevelSelectMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(levelSelectMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        void LevelSelectMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new LevelSelectBackground(), new LevelSelectScreen());
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }
    }
}
