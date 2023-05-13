using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml.Linq;
using UML_Project;
using static System.Reflection.Metadata.BlobBuilder;

namespace UML_Project
{


public class LibrarySystem
    {

            private string libraryname;
            double[] fines;
            Catalog[] catalogs;
            //Book[] books;
        static List<Book> books = new List<Book>();
        

        string email;
        int usersNumber=0;
        public List<User> users;
        private string usersFilePath="users.txt";

        public LibrarySystem(string usersFilePath)
        {
            this.usersFilePath = usersFilePath;
            users = new List<User>();
            LoadUsersFromFile();
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
            string name, username, password;

            // Get name
            do
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
            } while (!User.IsValidName(name));

            // Get username
            do
            {
                Console.Write("Enter your username: ");
                username = Console.ReadLine();
            } while (!User.IsValidUsername(username));

            // Get password
            do
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine();
            } while (!User.IsValidPassword(password));

            
        User user = new User(name, username, password, UserRole.Patron,usersNumber);
                users.Add(user);
            usersNumber++;
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
        }
        public int NumberOfBooks()
        {
            return books.Count();
        }
        public void DeleteBook(Book book)
        {
            books.Remove(book);

        }


        private void LoadUsersFromFile()
            {
                if (File.Exists(usersFilePath))
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(usersFilePath);
                        foreach (string line in lines)
                        {
                        Console.Write("Done");
                            string[] parts = line.Split(',');
                            string name = parts[0];
                            string username = parts[1];
                            string password = parts[2];

                        UserRole role = (UserRole)Enum.Parse(typeof(UserRole), parts[3]);
                        string id = parts[4];

                        if (role == UserRole.Admin)
                            {
                                // Create an Admin object if the user is an admin
                                Admin admin = new Admin(name, username, password, int.Parse(id));
                                users.Add(admin);
                            }
                            else
                            {
                                // Create a regular User object if the user is not an admin
                                User user = new User(name, username, password, role, int.Parse(id));
                                users.Add(user);
                            }
                        usersNumber = int.Parse(id) + 1;
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
            int counter = 0;
            try
            {
                using (StreamWriter writer = new StreamWriter(usersFilePath))
                {
                    foreach (User user in users)
                    {
                        string line = string.Join(",", user.Name, user.Username, user.Password, user.Role.ToString(),counter);
                        writer.WriteLine(line);
                        counter++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving users to file: " + e.Message);
            }
        }
    }

  
    public enum UserRole
    {
        Patron,
        Admin,
        Librarian
    }

     
       

   
}
