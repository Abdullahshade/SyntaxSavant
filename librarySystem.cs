using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml.Linq;
using UML_Project;

namespace UML_Project
{


public class LibrarySystem
    {

            private string libraryname;
            double[] fines;
            string[] catalogs;
            string[] books;
            string email;
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

            public void Register(string name, string username, string password, UserRole role,int id)
        {
            User newUser = new User(name, username, password, role,id);
            users.Add(newUser);
            SaveUsersToFile();
        }

        public User Login(string username, string password)
        {
            foreach (User user in users)
            {
                if (user.Name == username && user.Password == password)
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

            
        User user = new User(name, username, password, UserRole.NormalUser,9999);
                users.Add(user);

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
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading users from file: " + ex.Message);
                    }
                }
            }

            private void SaveUsersToFile()
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
        NormalUser,
        Admin
    }

     
       

   
}
