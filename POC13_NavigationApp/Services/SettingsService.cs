using POC13_NavigationApp.Enums;

namespace POC13_NavigationApp.Services
{
    public class SettingsService
    {
        public ThemeType GetTheme()
        {
            string theme =
                Properties.Settings.Default.Theme;

            if (theme == "Dark")
            {
                return ThemeType.Dark;
            }

            return ThemeType.Light;
        }

        public void SaveTheme(ThemeType theme)
        {
            Properties.Settings.Default.Theme =
                theme.ToString();

            Properties.Settings.Default.Save();
        }
    }
}