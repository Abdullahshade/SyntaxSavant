using System;
using System.Collections.Generic;
using System.IO;


namespace UML_CSharp
{
    class Program
    {

public class LibrarySystem
    {
        private List<User> users;
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

            public void Register(string name, string username, string password, string email, UserRole role)
        {
            User newUser = new User(name, username, password, email, role);
            users.Add(newUser);
            SaveUsersToFile();
        }

        public User Login(string username, string password)
        {
            foreach (User user in users)
            {
                if (user.GetName() == username && user.GetPassword() == password)
                {
                    return user;
                }
            }

            return null; // User not found
        }


            public void Register()
            {
                Console.WriteLine("Enter your name:");
                string name = Console.ReadLine();

                // Check if the name is already taken
                if (users.Exists(u => u.GetName() == name))
                {
                    Console.WriteLine("Error: Name is already taken.");
                    return;
                }

                Console.WriteLine("Enter your username:");
                string username = Console.ReadLine();

                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();

                Console.WriteLine("Enter your email:");
                string email = Console.ReadLine();

                // Check if the email is already taken
                if (users.Exists(u => u.GetEmail() == email))
                {
                    Console.WriteLine("Error: Email is already taken.");
                    return;
                }

                User user = new User(name, username, password, email, UserRole.NormalUser);
                users.Add(user);

                Console.WriteLine("User registered successfully.");
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
                            string[] parts = line.Split(',');
                            string name = parts[0];
                            string username = parts[1];
                            string password = parts[2];
                            string email = parts[3];
                            UserRole role = (UserRole)Enum.Parse(typeof(UserRole), parts[4]);

                            if (role == UserRole.Admin)
                            {
                                // Create an Admin object if the user is an admin
                                User.Admin admin = new User.Admin(name, username, password, email);
                                users.Add(admin);
                            }
                            else
                            {
                                // Create a regular User object if the user is not an admin
                                User user = new User(name, username, password, email, role);
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
            try
            {
                using (StreamWriter writer = new StreamWriter(usersFilePath))
                {
                    foreach (User user in users)
                    {
                        string line = string.Join(",", user.GetName(), user.GetUsername(), user.GetPassword(), user.GetEmail(), user.GetRole().ToString());
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving users to file: " + e.Message);
            }
        }
    }

        public class User
        {
            private string name;
            private string username;
            private string password;
            private string email;
            private UserRole role;

            public User(string name, string username, string password, string email, UserRole role)
            {
                this.name = name;
                this.username = username;
                this.password = password;
                this.email = email;
                this.role = role;
            }

            public string GetName()
            {
                return name;
            }

            public string GetUsername()
            {
                return username;
            }

            public string GetPassword()
            {
                return password;
            }

            public string GetEmail()
            {
                return email;
            }

            public UserRole GetRole()
            {
                return role;
            }

            // Admin subclass with additional methods
            public class Admin : User
            {
                public Admin(string name, string username, string password, string email) : base(name, username, password, email, UserRole.Admin)
                {
                }

                public void ViewAllUsers(List<User> users)
                {
                    Console.WriteLine("All users:");
                    foreach (User user in users)
                    {
                        Console.WriteLine(user.GetName());
                    }
                }

                public void SetRole(User userToChange, UserRole newRole)
                {
                    userToChange.role = newRole;
                    Console.WriteLine(userToChange.GetName() + "'s role has been changed to " + newRole);

                    // Update the user list and save it to file
                }
            }
        }

        public enum UserRole
    {
        NormalUser,
        Admin
    }
        static void Main(string[] args)
        {
            LibrarySystem librarySystem = new LibrarySystem("users.txt");
            User currentUser = null;

            while (true)
            {
                if (currentUser == null)
                {
                    // Prompt the user to choose between registering a new user or logging in with an existing user
                    Console.WriteLine("Welcome to the library system!");
                    Console.WriteLine("Do you want to register a new user (1) or log in with an existing user (2)?");
                    int choice = int.Parse(Console.ReadLine());

                    if (choice == 1)
                    {
                        // Register a new user
                        librarySystem.Register();
                    }
                    else if (choice == 2)
                    {
                        // Log in with an existing user
                        currentUser = librarySystem.Login();

                        if (currentUser != null)
                        {
                            Console.WriteLine("Logged in as " + currentUser.GetName());
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                else
                {
                    // User is logged in
                    if (currentUser is User.Admin)
                    {
                        // If user is admin, show additional options
                        Console.WriteLine("Do you want to view all users (1), set a user's role (2), or log out (3)?");
                        int adminChoice = int.Parse(Console.ReadLine());

                        if (adminChoice == 1)
                        {
                            List<User> users = librarySystem.GetUsers();
                            ((User.Admin)currentUser).ViewAllUsers(users);
                        }
                        else if (adminChoice == 2)
                        {
                            Console.WriteLine("Enter the username of the user whose role you want to change:");
                            string username = Console.ReadLine();
                            User userToChange = librarySystem.GetUsers().Find(u => u.GetUsername() == username);

                            if (userToChange != null)
                            {
                                Console.WriteLine("Enter the new role for the user:");
                                UserRole newRole = (UserRole)Enum.Parse(typeof(UserRole), Console.ReadLine());
                                ((User.Admin)currentUser).SetRole(userToChange, newRole);
                            }
                            else
                            {
                                Console.WriteLine("User not found.");
                            }
                        }
                        else if (adminChoice == 3)
                        {
                            currentUser = null;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Do you want to log out (1) or do something else (2)?");
                        int userChoice = int.Parse(Console.ReadLine());

                        if (userChoice == 1)
                        {
                            currentUser = null;
                        }
                        else if (userChoice == 2)
                        {
                            // Add additional options for normal users here
                            Console.WriteLine("Nothing else to do for normal users.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                    }
                }
            }
        }

    }
}
