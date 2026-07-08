using Microsoft.Win32;
using System.Reflection;

namespace POC13_NavigationApp.Services
{
    public class StartupService
    {
        private const string AppName = "POC13_NavigationApp";

        public void SetStartup(bool enable)
        {
            RegistryKey key =
                Registry.CurrentUser.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
                    true);

            if (enable)
            {
                string exePath =
                    Assembly.GetExecutingAssembly().Location;

                key.SetValue(AppName, exePath);
            }
            else
            {
                key.DeleteValue(AppName, false);
            }
        }

        public bool IsStartupEnabled()
        {
            RegistryKey key =
                Registry.CurrentUser.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

            return key.GetValue(AppName) != null;
        }
    }
}