using System;
using System.Linq;

namespace UML_Project
{
    class User
    {
        string name;
        string password;
        string role;
        string username;
        int id;
        float fees;
        static int id_counter = 111111; // to set values for users ids so that each one is unique

        public User(string name, string password, string role, string username, float fees = 0)
        {
            this.name = name;
            this.password = password;
            this.role = role;
            this.username = username;
            this.fees = fees;
        }

        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }
        public string Username { get => username; set => username = value; }
        public int Id { get => id; set => id = value; }
        public float Fees { get => fees; set => fees = value; }

        public void Register()
        {
            string name, username, password;

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

            // Set user details
            Name = name;
            Username = username;
            Password = password;
            Role = "User";
            Id = id_counter++;
            Fees = 0;

            Console.WriteLine("Registration successful!");
            string input = Console.ReadLine();
        }

        static bool IsValidName(string name)
        {
            // Name should not be empty and should contain only alphabets
            if (string.IsNullOrEmpty(name) || !name.All(char.IsLetter))
            {
                Console.WriteLine("Invalid name. Please enter a valid name containing only alphabets.");
                return false;
            }

            // Check if name is unique
            // This code assumes that you have a method called IsNameUnique that checks if the name already exists in a database or some other data source
            if (!IsNameUnique(name))
            {
                Console.WriteLine("Name already exists. Please enter a unique name.");
                return false;
            }

            return true;
        }

        static bool IsValidUsername(string username)
        {
            // Username should not be empty and should contain only alphabets and digits
            if (string.IsNullOrEmpty(username) || !username.All(char.IsLetterOrDigit))
            {
                Console.WriteLine("Invalid username. Please enter a valid username containing only alphabets and digits.");
                return false;
            }

            // Check if username is unique
            // This code assumes that you have a method called IsUsernameUnique that checks if the username already exists in a database or some other data source
            if (!IsUsernameUnique(username))
            {
                Console.WriteLine("Username already exists. Please enter a unique username.");
                return false;
            }

            return true;
        }

        static bool IsValidPassword(string password)
        {
            // Password should not be empty and should be at least 8 characters long
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                Console.WriteLine("Invalid password. Please enter a password that is at least 8 characters long.");
                return false;
            }

            return true;
        }

        static bool IsNameUnique(string name)
        {
            // This is a placeholder method that always returns true
            // You should replace this with your own code that checks if the name already exists in a database or some other data source
            return true;
        }

        static bool IsUsernameUnique(string username)
        {
            // This is a placeholder method that always returns true
            // You should replace this with your own code that checks if the username already exists in a database or some other data source
            return true;
        }

        public void ViewFines(User user)
        {
            Console.WriteLine("Outstanding Fines: {0}", user.Fees);
        }
        public void Search() 
        {
            
        }


    
    }
}
