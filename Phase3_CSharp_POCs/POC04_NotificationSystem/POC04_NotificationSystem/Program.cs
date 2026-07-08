using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Runtime.CompilerServices;

namespace POC04_NotificationSystem
{
    interface INotificationService
    {
        void SendNotification(string message, string recipient);

        string GetServiceName();
    }

    class notification
    {
        public string ServiceName;
        public string Message;
        public string recipient;
        public DateTime date;
    }


    class ConsoleNotificationService : INotificationService
    {
        public void SendNotification(string message, string recipient)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("CONSOLE NOTIFICATION");
            Console.WriteLine("====================================================");

            Console.WriteLine($"\nTo: {recipient}\nTime:{DateTime.Now}  \nmessage:{message}");

            Console.WriteLine("\n====================================================");

            Console.WriteLine("Notification sent successfully");

        }
        public string GetServiceName()
        {
            return "Console Notification";

        }
    }

    class FileNotificationService : INotificationService
    {

        public void SendNotification(string message, string recipient)
        {
            string path = @"C:\Users\Blobs\source\repos\WpfInternTraining\Phase3_CSharp_POCs\POC04_NotificationSystem\notifications.txt";
            string data = $"\nTo:{recipient} \nTime:{DateTime.Now} \nMessage:{message}";
            File.AppendAllText(path, data);

            Console.WriteLine("File Notification sent successfully");

        }

        public string GetServiceName()
        {
            return "File Notification";

        }
    }

    class EmailNotificationService : INotificationService
    {
        public void SendNotification(string message, string recipient)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("EMAIL NOTIFICATION");
            Console.WriteLine("====================================================");
            Console.WriteLine($"\nTo: {recipient}\nSubject:Email Notification \nTime:{DateTime.Now}  \nmessage:{message}");

            Console.WriteLine("\n====================================================");

            Console.WriteLine("Email Notification sent successfully");
        }

        public string GetServiceName()
        {
            return "Email Notification";

        }
    }

    class notificatiomanager
    {
        private List<notification> history = new List<notification>();
        private Dictionary<String,int> stat=new Dictionary<String,int>();

       
        public void send(INotificationService service, string message, string recipient)
        {
            service.SendNotification(message, recipient);

            notification notifications = new notification();
            notifications.recipient = recipient;
            notifications.Message = message;
            notifications.ServiceName = service.GetServiceName();
            notifications.date = DateTime.Now;

            history.Add(notifications);

            string servicename = service.GetServiceName();

            if (stat.ContainsKey(servicename)) { 
                stat[servicename]++;
            }

            else { stat[servicename] = 1; }

           
        }

        public void showhistory()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("\nNo any Notifications history");
            }

            foreach (notification notification in history)
            {
                Console.WriteLine("\n");
                Console.Write($"\nservice name:{notification.ServiceName} \nrecipient:{notification.recipient} \nmessage:{notification.Message} \ntime:{notification.date}");
            }
        }

        public void statistics()
        {
            foreach(var item in stat)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }
        }

       
    }
        internal class Program
        {
            static void Main(string[] args)
            {

                notificatiomanager notification= new notificatiomanager();
                INotificationService current = new ConsoleNotificationService();

            int choice;

            do
            {
                Console.WriteLine("\n=== Notification System === ");
                Console.WriteLine($"\nCurrent Service: {current.GetServiceName()}");
                Console.WriteLine("\n1.Send Notification \n2.view History \n3.Change Service \n4.Service Statistics \n5.Exit");
                Console.WriteLine("\nEnter Choice:");
                choice=int.Parse(Console.ReadLine());

                switch (choice) {
                    case 1:
                        Console.WriteLine("\nEnter Recipient:");
                        string recipient = Console.ReadLine();
                        Console.WriteLine("\nEnter Message:");
                        string message = Console.ReadLine();

                        notification.send(current, message,recipient);

                        break; 

                    case 2:
                       
                        notification.showhistory();
                        break;

                    case 3:
                        Console.WriteLine("\nSelect notification service:");
                        Console.WriteLine("\n1.console \n2.file \n3.email");
                        Console.WriteLine("\nEnter choice");
                        int ch = int.Parse(Console.ReadLine());

                        switch (ch)
                        {
                            case 1:
                                current = new ConsoleNotificationService();
                                Console.WriteLine("\nswithced to console");
                                break;

                            case 2:
                                current = new FileNotificationService();
                                Console.WriteLine("\nswitched to file");
                                break;

                            case 3:
                                current = new EmailNotificationService();
                                Console.WriteLine("\nswithced to email");
                                break;

                            default:
                                Console.WriteLine("\ninvalid choice");
                                break;

                        }
                        break;

                    case 4:
                        notification.statistics();
                        break;

                    case 5:
                        break;
                }


            }
            while (choice!=5);

            }
        }
    }

