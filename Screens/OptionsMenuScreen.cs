using Microsoft.Xna.Framework;

namespace SideScroller
{
    class OptionsMenuScreen : MenuScreen
    {
        MenuEntry ungulateMenuEntry;
        MenuEntry languageMenuEntry;
        MenuEntry frobnicateMenuEntry;
        MenuEntry elfMenuEntry;

        public OptionsMenuScreen()
            : base("Options")
        {
            ungulateMenuEntry = new MenuEntry(string.Empty);
            languageMenuEntry = new MenuEntry(string.Empty);
            frobnicateMenuEntry = new MenuEntry(string.Empty);
            elfMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry backMenuEntry = new MenuEntry("Back");

            backMenuEntry.Selected += OnCancel;
            
            MenuEntries.Add(ungulateMenuEntry);
            MenuEntries.Add(languageMenuEntry);
            MenuEntries.Add(frobnicateMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }

        void SetMenuEntryText()
        {
            ungulateMenuEntry.Text = "Jacob Glawe - Programmer ";
            languageMenuEntry.Text = "Mike Huffington - Artist ";
            frobnicateMenuEntry.Text = "Westin Brummel - Desgin/Media ";
        }

        void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            SetMenuEntryText();
        }

        void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            SetMenuEntryText();
        }

        void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            SetMenuEntryText();
        }
    }
}
