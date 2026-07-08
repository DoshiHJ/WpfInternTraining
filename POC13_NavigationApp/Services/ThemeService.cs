using System;
using System.Windows;

using POC13_NavigationApp.Enums;

namespace POC13_NavigationApp.Services
{
    public class ThemeService
    {
        public void ApplyTheme(ThemeType theme)
        {
            string themePath = "";

            if (theme == ThemeType.Light)
            {
                themePath = "Themes/LightTheme.xaml";
            }
            else
            {
                themePath = "Themes/DarkTheme.xaml";
            }

            ResourceDictionary dictionary =
                new ResourceDictionary();

            dictionary.Source =
                new Uri(themePath, UriKind.Relative);

            Application.Current.Resources
                .MergedDictionaries.Clear();

            Application.Current.Resources
                .MergedDictionaries.Add(dictionary);
        }
    }
}