using Newtonsoft.Json;

namespace ConfigBasedApplication
{
    class AppSettings
    {
        public string ApplicationName { get; set; }

        public string Version { get; set; }

        public string Environment { get; set; }
    }

    
    class DatabaseConfig
    {
        public string Provider { get; set; }

        public string ConnectionString { get; set; }

        public int MaxConnections { get; set; }
    }

    
    class FeatureFlags
    {
        public bool EnableLogging { get; set; }

        public bool EnableNotifications { get; set; }

        public bool EnableAutoSave { get; set; }
    }

    class UserPreferences
    {
        public string Theme { get; set; }

        public string Language { get; set; }

        public string DateFormat { get; set; }
    }


    class AppConfig
    {
        public AppSettings AppSettings { get; set; }

        public DatabaseConfig DatabaseConfig { get; set; }

        public FeatureFlags FeatureFlags { get; set; }

        public UserPreferences UserPreferences { get; set; }
    }

    
    class ConfigService
    {
        public string filePath =
            @"C:\Users\Blobs\source\repos\WpfInternTraining\Phase3_CSharp_POCs\POC06_ConfigBasedApp\config.json";

        public AppConfig LoadConfig()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    AppConfig defaultConfig =
                        CreateDefaultConfig();

                    SaveConfig(defaultConfig);

                    return defaultConfig;
                }

                string jsonData =
                    File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(jsonData))
                {
                    return CreateDefaultConfig();
                }

                AppConfig config =
                    JsonConvert.DeserializeObject<AppConfig>(jsonData);

                return config ?? CreateDefaultConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return CreateDefaultConfig();
            }
        }

        public void SaveConfig(AppConfig config)
        {
            string jsonData =
                JsonConvert.SerializeObject
                (
                    config,
                    Formatting.Indented
                );

            File.WriteAllText(filePath, jsonData);
        }

      
        public AppConfig CreateDefaultConfig()
        {
            return new AppConfig
            {
                AppSettings = new AppSettings
                {
                    ApplicationName = "Task Manager Pro",
                    Version = "1.0.0",
                    Environment = "Development"
                },

                DatabaseConfig = new DatabaseConfig
                {
                    Provider = "SQLite",
                    ConnectionString = "Data Source=tasks.db",
                    MaxConnections = 10
                },

                FeatureFlags = new FeatureFlags
                {
                    EnableLogging = true,
                    EnableNotifications = true,
                    EnableAutoSave = false
                },

                UserPreferences = new UserPreferences
                {
                    Theme = "Dark",
                    Language = "English",
                    DateFormat = "yyyy-MM-dd"
                }
            };
        }

        
        public void DisplayConfig()
        {
            AppConfig config = LoadConfig();

            Console.WriteLine("\n===== APP SETTINGS =====");

            Console.WriteLine
            (
                $"Application Name: " +
                $"{config.AppSettings.ApplicationName}"
            );

            Console.WriteLine
            (
                $"Version: " +
                $"{config.AppSettings.Version}"
            );

            Console.WriteLine
            (
                $"Environment: " +
                $"{config.AppSettings.Environment}"
            );

            Console.WriteLine("\n===== DATABASE CONFIG =====");

            Console.WriteLine
            (
                $"Provider: " +
                $"{config.DatabaseConfig.Provider}"
            );

            Console.WriteLine
            (
                $"Connection String: " +
                $"{config.DatabaseConfig.ConnectionString}"
            );

            Console.WriteLine
            (
                $"Max Connections: " +
                $"{config.DatabaseConfig.MaxConnections}"
            );

            Console.WriteLine("\n===== FEATURE FLAGS =====");

            Console.WriteLine
            (
                $"Enable Logging: " +
                $"{config.FeatureFlags.EnableLogging}"
            );

            Console.WriteLine
            (
                $"Enable Notifications: " +
                $"{config.FeatureFlags.EnableNotifications}"
            );

            Console.WriteLine
            (
                $"Enable Auto Save: " +
                $"{config.FeatureFlags.EnableAutoSave}"
            );

            Console.WriteLine("\n===== USER PREFERENCES =====");

            Console.WriteLine
            (
                $"Theme: " +
                $"{config.UserPreferences.Theme}"
            );

            Console.WriteLine
            (
                $"Language: " +
                $"{config.UserPreferences.Language}"
            );

            Console.WriteLine
            (
                $"Date Format: " +
                $"{config.UserPreferences.DateFormat}"
            );
        }

      
        public void UpdateUserPreferences()
        {
            AppConfig config = LoadConfig();

            Console.WriteLine("\n===== UPDATE USER PREFERENCES =====");

           
            Console.Write
            (
                $"Theme ({config.UserPreferences.Theme}) " +
                $"Dark/Light: "
            );

            string theme = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(theme))
            {
                if (theme == "Dark" || theme == "Light")
                {
                    config.UserPreferences.Theme = theme;
                }
                else
                {
                    Console.WriteLine("Invalid Theme");
                }
            }

           
            Console.Write
            (
                $"Language ({config.UserPreferences.Language}): "
            );

            string language = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(language))
            {
                config.UserPreferences.Language = language;
            }

            Console.Write
            (
                $"Date Format ({config.UserPreferences.DateFormat}): "
            );

            string dateFormat = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(dateFormat))
            {
                config.UserPreferences.DateFormat = dateFormat;
            }

            SaveConfig(config);

            Console.WriteLine("User Preferences Updated.");
        }

        public void ToggleFeatureFlags()
        {
            AppConfig config = LoadConfig();

            Console.WriteLine("\n===== FEATURE FLAGS =====");

            Console.WriteLine
            (
                $"1. Enable Logging " +
                $"({config.FeatureFlags.EnableLogging})"
            );

            Console.WriteLine
            (
                $"2. Enable Notifications " +
                $"({config.FeatureFlags.EnableNotifications})"
            );

            Console.WriteLine
            (
                $"3. Enable Auto Save " +
                $"({config.FeatureFlags.EnableAutoSave})"
            );

            Console.Write("Choose Option: ");

            int choice =
               int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:

                    config.FeatureFlags.EnableLogging =
                        !config.FeatureFlags.EnableLogging;

                    Console.WriteLine
                    (
                        $"Logging Changed To: " +
                        $"{config.FeatureFlags.EnableLogging}"
                    );

                    break;

                case 2:

                    config.FeatureFlags.EnableNotifications =
                        !config.FeatureFlags.EnableNotifications;

                    Console.WriteLine
                    (
                        $"Notifications Changed To: " +
                        $"{config.FeatureFlags.EnableNotifications}"
                    );

                    break;

                case 3:

                    config.FeatureFlags.EnableAutoSave =
                        !config.FeatureFlags.EnableAutoSave;

                    Console.WriteLine
                    (
                        $"Auto Save Changed To: " +
                        $"{config.FeatureFlags.EnableAutoSave}"
                    );

                    break;

                default:

                    Console.WriteLine("Invalid Choice");

                    return;
            }

            SaveConfig(config);
        }

        public void TestBehavior()
        {
            AppConfig config = LoadConfig();

            Console.WriteLine
            (
                "\n=== Testing Config-Driven Behavior ==="
            );

            if (config.FeatureFlags.EnableLogging)
            {
                Console.WriteLine
                (
                    $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] " +
                    $"Log: Application started"
                );
            }

            if (config.FeatureFlags.EnableNotifications)
            {
                Console.WriteLine
                (
                    "Notification: You have 3 pending tasks"
                );
            }

            if (!config.FeatureFlags.EnableAutoSave)
            {
                Console.WriteLine
                (
                    "Auto-save is disabled"
                );
            }
            else
            {
                Console.WriteLine
                (
                    "Auto-save is enabled"
                );
            }

            Console.WriteLine
            (
                $"Current theme: " +
                $"{config.UserPreferences.Theme}"
            );

            if (config.UserPreferences.Theme == "Dark")
            {
                Console.BackgroundColor =
                    ConsoleColor.Black;

                Console.ForegroundColor =
                    ConsoleColor.Green;


                Console.WriteLine("Dark theme applied!");
            }
            else
            {
                Console.BackgroundColor =
                    ConsoleColor.White;
                
                Console.ForegroundColor =
                    ConsoleColor.Black;
                
                //Console.Clear();

                Console.WriteLine("Light theme applied!");
            } 
        }

        public void ReloadConfiguration()
        {
            LoadConfig();

            Console.WriteLine
            (
                "Configuration Reloaded Successfully."
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConfigService service =
                new ConfigService();

            AppConfig config =
                service.LoadConfig();

            Console.WriteLine
            (
                $"\nCurrent App: " +
                $"{config.AppSettings.ApplicationName} " +
                $"v{config.AppSettings.Version}"
            );
            int choice;

            do
            {
                Console.WriteLine
                (
                    "\n=== Configuration Manager ==="
                );

                Console.WriteLine("1. View Configuration");

                Console.WriteLine("2. Update User Preferences");

                Console.WriteLine("3. Toggle Feature Flags");

                Console.WriteLine("4. Test Config-Driven Behavior");

                Console.WriteLine("5. Reload Configuration");

                Console.WriteLine("6. Exit");

                Console.Write("\nChoose Option: ");

                choice =
                    int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        service.DisplayConfig();

                        break;

                    case 2:

                        service.UpdateUserPreferences();

                        break;

                    case 3:

                        service.ToggleFeatureFlags();

                        break;

                    case 4:

                        service.TestBehavior();

                        break;

                    case 5:

                        service.ReloadConfiguration();

                        break;

                    case 6:

                        return;

                    default:

                        Console.WriteLine("Invalid Choice");

                        break;
                }
            }
            while (choice != 6);
        }
    }
}
