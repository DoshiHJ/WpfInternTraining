using Newtonsoft.Json;

using System.Text.RegularExpressions;


namespace JSONContactBook
{
   
    class Contact
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Category { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsFavorite { get; set; }
    }

    
    class ContactBook
    {
        public List<Contact> Contacts { get; set; } = new List<Contact>();

        public DateTime LastModified { get; set; }

        public int TotalContacts
        {
            get
            {
                return Contacts.Count;
            }
        }
    }

    class ContactService
    {
        private readonly string filePath =
            @"C:\Users\Blobs\source\repos\WpfInternTraining\Phase3_CSharp_POCs\POC05_JsonContactBook\contacts.json";

        private readonly string backupFolder =
            @"C:\Users\Blobs\source\repos\WpfInternTraining\Phase3_CSharp_POCs\POC05_JsonContactBook\Backups";

        
        public ContactBook LoadContacts()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new ContactBook();
                }

                string jsonData = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(jsonData))
                {             
                    return new ContactBook();
                }

                ContactBook contactBook =
                    JsonConvert.DeserializeObject<ContactBook>(jsonData);

                return contactBook ?? new ContactBook();
            }
            catch (JsonException)
            {
                Console.WriteLine("Corrupted JSON detected.");

                BackupCorruptedFile();

                return new ContactBook();
            }
        }

       
        public void SaveContacts(ContactBook contactBook)
        {
            contactBook.LastModified = DateTime.Now;

            string jsonData =
                JsonConvert.SerializeObject
                (
                    contactBook,
                    Formatting.Indented
                );

            File.WriteAllText(filePath, jsonData);
        }

       
        public void AddContact()
        {
            ContactBook contactBook = LoadContacts();

            Contact contact = new Contact();

            Console.Write("Enter Name: ");
            contact.Name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(contact.Name))
            {
                Console.WriteLine("Name is required.");
                return;
            }

            Console.Write("Enter Phone Number: ");
            contact.PhoneNumber = Console.ReadLine();

            

            if (contact.PhoneNumber.Length != 10)
            {
                Console.WriteLine("Invalid phone number. Number must be 10 digits");
                return;
            }

            if (contactBook.Contacts.Any
                (c => c.PhoneNumber == contact.PhoneNumber))
            {
                Console.WriteLine("Phone number already exists.");
                return;
            }

            Console.Write("Enter Email: ");
            contact.Email = Console.ReadLine();

          
            if (!contact.Email.Contains("@"))
            {
                Console.WriteLine("Invalid Email Format");
                return;
            }

            if (contactBook.Contacts.Any
                (c => c.Email == contact.Email))
            {
                Console.WriteLine("Email already exists.");
                return;
            }       
            Console.Write("Enter Address: ");
            contact.Address = Console.ReadLine();
  
            Console.WriteLine("Select Category: \n1.Family \n2.Friends \n3.Work \n4.Other");
            int ch=int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 1:
                    contact.Category = "Family";
                    break;
                case 2:
                    contact.Category = "Friends";
                    break;
                case 3:
                    contact.Category = "Work";
                    break;
                case 4:
                    contact.Category = "Other";
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }

            contact.Id = Guid.NewGuid();

            contact.DateAdded = DateTime.Now;

            
            Console.WriteLine("Mark as favorite? (y/n):");
            string favorite = Console.ReadLine();

            if (favorite == "y")
            {
                contact.IsFavorite = true;
            }
            else
            {
                contact.IsFavorite= false;
            }

            CreateBackup();

            contactBook.Contacts.Add(contact);

            SaveContacts(contactBook);

            Console.WriteLine("Contact Added Successfully.");
        }

        
        public void ViewContacts()
        {
            ContactBook contactBook = LoadContacts();

            if (contactBook.Contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            foreach (Contact contact in contactBook.Contacts)
            {
                DisplayContact(contact);
            }
        }

       
        public void DisplayContact(Contact contact)
        {
            Console.WriteLine("-----------------------------------");

            Console.WriteLine($"Id: {contact.Id}");

            Console.WriteLine($"Name: {contact.Name}");

            Console.WriteLine($"Phone: {contact.PhoneNumber}");

            Console.WriteLine($"Email: {contact.Email}");

            Console.WriteLine($"Address: {contact.Address}");

            Console.WriteLine($"Category: {contact.Category}");

            Console.WriteLine($"Favorite: {contact.IsFavorite}");

            Console.WriteLine($"Date Added: {contact.DateAdded}");
        }

        
        public void SearchByName()
        {
            ContactBook contactBook = LoadContacts();

            Console.Write("Enter Name: ");

            string search = Console.ReadLine();

            var results =
                contactBook.Contacts.Where
                (
                    c => c.Name.Contains
                    (
                        search,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            foreach (var contact in results)
            {
                DisplayContact(contact);
            }
        }

        
        public void SearchByPhone()
        {
            ContactBook contactBook = LoadContacts();

            Console.Write("Enter Phone Number: ");

            string phone = Console.ReadLine();

            Contact contact =
                contactBook.Contacts.FirstOrDefault
                (
                    c => c.PhoneNumber == phone
                );

            if (contact != null)
            {
                DisplayContact(contact);
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

       
        public void SearchByEmail()
        {
            ContactBook contactBook = LoadContacts();

            Console.Write("Enter Email: ");

            string email = Console.ReadLine();

            Contact contact =
                contactBook.Contacts.FirstOrDefault
                (
                    c => c.Email == email
                );

            if (contact != null)
            {
                DisplayContact(contact);
            }
            else
            {
                Console.WriteLine("Contact not found.");
            } 
        }

       
        public void EditContact()
        {
            ContactBook contactBook = LoadContacts();

            Console.Write("Enter Phone Number to Edit: ");

            string phone = Console.ReadLine();

            Contact contact =
                contactBook.Contacts.FirstOrDefault
                (
                    c => c.PhoneNumber == phone
                );

            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.WriteLine("\nLeave field empty to keep old value.");

            
            Console.Write($"Name ({contact.Name}): ");

            string newName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName))
            {
                contact.Name = newName;
            }

            Console.Write($"Phone ({contact.PhoneNumber}): ");

            string newPhone = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newPhone))
            {
               
                if (newPhone.Length != 10)
                {
                    Console.WriteLine("Invalid phone number Number must be 10 digits");
                    return;
                }

                bool phoneExists =
                    contactBook.Contacts.Any
                    (
                        c => c.PhoneNumber == newPhone
                        && c.Id != contact.Id
                    );

                if (phoneExists)
                {  
                    Console.WriteLine("Phone number already exists.");
                    return;
                }

                contact.PhoneNumber = newPhone;
            }

           
            Console.Write($"Email ({contact.Email}): ");

            string newEmail = Console.ReadLine();
             
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                if (!newEmail.Contains("@")) {
                    Console.WriteLine("Invalid email format");
                    return;
                }

                bool emailExists =
                    contactBook.Contacts.Any
                    (
                        c => c.Email == newEmail
                        && c.Id != contact.Id
                    );

                if (emailExists)
                {
                    Console.WriteLine("Email already exists.");
                    return;
                }

                contact.Email = newEmail;
            }

           
            Console.Write($"Address ({contact.Address}): ");

            string newAddress = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newAddress))
            {
                contact.Address = newAddress;
            }

            
            Console.Write($"Category ({contact.Category}): ");

            Console.WriteLine("Select New  Category: \n1.Family \n2.Friends \n3.Work \n4.Other");
            string input= Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                int ch=int.Parse(input);
                switch (ch)
                {
                    case 1:
                        contact.Category = "Family";
                        break;
                    case 2:
                        contact.Category = "Friends";
                        break;
                    case 3:
                        contact.Category = "Work";
                        break;
                    case 4:
                        contact.Category = "Other";
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }

            }

           
            CreateBackup();

            SaveContacts(contactBook);

            Console.WriteLine("Contact updated successfully.");
        }

       
        public void DeleteContact()
        {
            ContactBook contactBook = LoadContacts();

            Console.Write("Enter Phone Number: ");

            string phone = Console.ReadLine();

            Contact contact =
                contactBook.Contacts.FirstOrDefault
                (
                    c => c.PhoneNumber == phone
                );

            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.Write("Are you sure? (yes/no): ");                                                                       

            string confirm = Console.ReadLine();

            if (confirm.ToLower() == "yes")
            {
                CreateBackup();

                contactBook.Contacts.Remove(contact);

                SaveContacts(contactBook);

                Console.WriteLine("Contact deleted.");                                             
            }
        }

        
        public void ToggleFavorite()
        {
            ContactBook contactBook = LoadContacts();

            Console.Write("Enter Phone Number: ");
                                                                                                         
            string phone = Console.ReadLine();

            Contact contact =
                contactBook.Contacts.FirstOrDefault
                (
                    c => c.PhoneNumber == phone
                );

            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            contact.IsFavorite = !contact.IsFavorite;

            SaveContacts(contactBook);

            Console.WriteLine("Favorite updated.");
        }

       
        public void FilterByCategory()
        {
            ContactBook contactBook = LoadContacts();

            Console.Write("Enter Category: ");

            string category = Console.ReadLine();

            var contacts =
                contactBook.Contacts.Where
                (
                    c => c.Category.Equals
                    (
                        category,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            foreach (var contact in contacts)
            {
                DisplayContact(contact);
            }
        }

        
        public void ViewFavorites()
        {
            ContactBook contactBook = LoadContacts();

            var favorites =
                contactBook.Contacts.Where
                (
                    c => c.IsFavorite
                );

            foreach (var contact in favorites)
            {
                DisplayContact(contact);
            }
        }

      
        public void CategoryStatistics()
        {
            ContactBook contactBook = LoadContacts();

            var groups =
                contactBook.Contacts.GroupBy(c => c.Category);

            foreach (var group in groups)
            {
                Console.WriteLine
                (
                    $"{group.Key} : {group.Count()}"
                );
            }
        }

      
        public void CreateBackup()
        {
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }

            if (!File.Exists(filePath))
            {
                return;
            }

            string backupFile =
                Path.Combine
                (
                    backupFolder,
                    $"contacts_{DateTime.Now:yyyyMMddHHmmss}.json"
                );

            File.Copy(filePath, backupFile, true);

            KeepLastFiveBackups();
        }

        public void KeepLastFiveBackups()
        {
            DirectoryInfo directory =
                new DirectoryInfo(backupFolder);

            FileInfo[] files =
                directory.GetFiles()
                .OrderByDescending(f => f.CreationTime)
                .ToArray();

            if (files.Length > 5)
            {
                foreach (FileInfo file in files.Skip(5))
                {
                    file.Delete();
                }
            }
        }

        public void ListBackups()
        {
            if (!Directory.Exists(backupFolder))
            {
                Console.WriteLine("No backups found.");
                return;
            }

            string[] files =
                Directory.GetFiles(backupFolder);

            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
              
            }
        }

        public void BackupCorruptedFile()
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string corruptedFile =
                Path.Combine
                (
                    backupFolder,
                    $"corrupted_{DateTime.Now:yyyyMMddHHmmss}.json"
                );

            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }

            File.Copy(filePath, corruptedFile, true);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ContactService service = new ContactService();
            int choice;

            do
            {
                Console.WriteLine("\n===== CONTACT BOOK =====");

                Console.WriteLine("1. Add Contact");

                Console.WriteLine("2. View Contacts");

                Console.WriteLine("3. Search By Name");

                Console.WriteLine("4. Search By Phone");

                Console.WriteLine("5. Search By Email");
                Console.WriteLine("6. Edit Contact");

                Console.WriteLine("7. Delete Contact");

                Console.WriteLine("8. Favorite/Unfavorite");

                Console.WriteLine("9. Filter By Category");

                Console.WriteLine("10. View Favorites");

                Console.WriteLine("11. Category Statistics");

                Console.WriteLine("12. List Backups");

                Console.WriteLine("13. Exit");

                Console.Write("Enter Choice: ");

                choice =
                     int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        service.AddContact();
                        break;

                    case 2:
                        service.ViewContacts();
                        break;

                    case 3:
                        service.SearchByName();
                        break;

                    case 4:
                        service.SearchByPhone();
                        break;

                    case 5:
                        service.SearchByEmail();
                        break;

                    case 6:
                        service.EditContact();
                        break;

                    case 7:
                        service.DeleteContact();
                        break;

                    case 8:
                        service.ToggleFavorite();
                        break;

                    case 9:
                        service.FilterByCategory();
                        break;

                    case 10:
                        service.ViewFavorites();
                        break;

                    case 11:
                        service.CategoryStatistics();
                        break;

                    case 12:
                        service.ListBackups();
                        break;

                    case 13:
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
            while (choice != 13);
        }
    }
}