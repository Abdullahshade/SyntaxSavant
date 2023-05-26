using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace UML_Project
{

    [Serializable]

    public class LibrarySystem
    {

        private string libraryname;
        double[] fines;
        Catalog[] catalogs;
        //Book[] books;
        static List<Book> books = new List<Book>();
        Catalog catalog = new Catalog();


        int usersNumber = 1;
        public List<User> users;
        private string usersFilePath = "users.txt";

        public int UsersNumber { get => usersNumber; set => usersNumber = value; }

        public LibrarySystem(string usersFilePath)
        {
            this.usersFilePath = usersFilePath;
            users = new List<User>();
            LoadUsersFromFile();
            LoadBooksFromFile();
        }
        public List<User> GetUsers()
        {
            return users;
        }
        public User Login(string username, string password)
        {
            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return user;
                }
            }

            return null; // User not found
        }


        public void Register()
        {
            string name, username, password, contactInformation;

            // Get name
            do
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
            } while (!IsValidName(name));

            // Get username
            do
            {
                Console.Write("Enter your username: ");
                username = Console.ReadLine();
            } while (!IsValidUsername(username));

            // Get password
            do
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine();
            } while (!IsValidPassword(password));

            // Get contact Information
            do
            {
                Console.Write("Enter your phone number: ");
                contactInformation = Console.ReadLine();    
            } while (!IsValidContactInformation(contactInformation));


            User user = new Patron(name, username, password, contactInformation, UserRole.Patron, UsersNumber);
            users.Add(user);
            UsersNumber++;
            Console.WriteLine("User registered successfully.");
            SaveUsersToFile();
        }

        public User Login()
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            return Login(username, password);
        }

        public Book FindBookByTitle(string title)
        {

            foreach (Book book in books)
            {
                if (book.Title == title)
                {
                    return book;
                }

            }
            return null;
        }
        public void AddBookToBooks(Book newBook)
        {
            books.Add(newBook);
            newBook.SendNewBookInfo(catalog);
            SaveBooksToFile();
        }
        public int NumberOfBooks()
        {
            return books.Count();
        }
        public void DeleteBook(Book book)
        {

            books.Remove(book);
            catalog.RemoveBookFromGenre(book);
            SaveBooksToFile();
        }

        public void LoadUsersFromFile()
        {
            users.Clear();
            if (File.Exists(usersFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(usersFilePath);
                    int counter = 1;

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        UserRole role = (UserRole)Enum.Parse(typeof(UserRole), parts[0]);
                        

                        if (role == UserRole.Admin)
                        {
                            // Create an Admin object if the user is an admin
                            string name = parts[1];
                            string username = parts[2];
                            string password = parts[3];
                            string id = parts[4];
                            Admin admin = new Admin(name, username, password, int.Parse(id));
                            users.Add(admin);
                            counter++;
                        }
                        else if (role == UserRole.Librarian)
                        {
                            // Create a Librarian object if the user is a Librarian
                            string name = parts[1];
                            string username = parts[2];
                            string password = parts[3];
                            string id = parts[4];
                            Librarian librarian = new Librarian(name, username, password, int.Parse(id));
                            users.Add(librarian);
                            counter++;
                        }
                        else if (role == UserRole.Patron) 
                        {
                            // Create a regular User object if the user is not an admin or librarin 
                            string name = parts[1];
                            string username = parts[2];
                            string password = parts[3];
                            string id = parts[4];
                            string contactInformation = parts[5];
        
                            Patron patron = new Patron(name, username, password, contactInformation, role, int.Parse(id));
                            users.Add(patron);
                            counter++;
                        }
                        UsersNumber = counter;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading users from file: " + ex.Message);
                }
            }
        }
        public void SaveUsersToFile()
        {
            int counter = 1;
            try
            {
                using (StreamWriter writer = new StreamWriter(usersFilePath))
                {
                    foreach (User user in users)
                    {
                        if (user.Role == UserRole.Admin || user.Role == UserRole.Librarian)
                        {
                            string line = string.Join(",", user.Role.ToString(), user.Name, user.Username, user.Password, counter);
                            writer.WriteLine(line);
                            counter++;
                        }
                        else if (user.Role == UserRole.Patron)
                        {
                            string contactInformation = ((Patron)user).ContactInformation;
                            string line = string.Join(",", user.Role.ToString(), user.Name, user.Username, user.Password, counter, contactInformation);
                            writer.WriteLine(line);
                            counter++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving users to file: " + e.Message);
            }
        }

        public void SaveBooksToFile()
        {
            FileStream fileStream = new FileStream("books.dat", FileMode.OpenOrCreate);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, books);
            fileStream.Close();
        }
        public void ListBooks()
        {

            foreach (Book book in books)
            {
                Console.WriteLine("--------------------------------");
                book.ShowBookInfo();
                Console.WriteLine("--------------------------------\n");
            }
        }
        public void LoadBooksFromFile()
        {
            FileStream fileStream = new FileStream("books.dat", FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            books = (List<Book>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }

            
        public void DeleteUser(User user)
        {
            int id = user.Id;
            users.RemoveAt(id);
        }

        public static bool IsValidName(string name)
        {
            // Name should not be empty and should contain only alphabets
            if (string.IsNullOrEmpty(name) || !name.All(char.IsLetter))
            {
                Console.WriteLine("Invalid name. Please enter a valid name containing only alphabets.");
                return false;
            }

            return true;
        }

        public bool IsValidUsername(string username)
        {
            // Username should not be empty and should contain only alphabets and digits
            if (string.IsNullOrEmpty(username) || !username.All(char.IsLetterOrDigit))
            {
                Console.WriteLine("Invalid username. Please enter a valid username containing only alphabets and digits.");
                return false;
            }

            // Check if username is unique

            if (!IsUsernameUnique(username))
            {
                Console.WriteLine("Username already exists. Please enter a unique username.");
                return false;
            }

            return true;
        }

        public bool IsValidPassword(string password)
        {
            // Password should not be empty and should be at least 8 characters long
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                Console.WriteLine("Invalid password. Please enter a password that is at least 8 characters long.");
                return false;
            }
            if (!IsPasswordUnique(password))
            {
                Console.WriteLine("Password isn't unique. Please enter a unique password.");
                return false;
            }
            
            return true;
        }
        public static bool IsValidContactInformation(string contactInformation)
        {
            // To check if phone number is valid and correct
            if (string.IsNullOrEmpty(contactInformation) || contactInformation.Length != 10 || !contactInformation.All(char.IsDigit))
            {
                Console.WriteLine($"{contactInformation} is not a valid phone number.");
                return false;
            }
            return true;
        }

        public bool IsPasswordUnique(string password)
        {
            // To check if password is unique
            User user = users.Find(x => x.Password == password);
            if(user == null)
            {
                return true;
            }

            return false;
        }

        public bool IsUsernameUnique(string username)
        {
            // To check if Username is taken
            User user = users.Find(u => u.Username == username);
            if (user == null)
            {
                return true;
            }
            
            return false;
        }
    }


        




    public enum UserRole
    {
        Patron,
        Admin,
        Librarian
    }





}