using System;
using System.Linq;

namespace UML_Project
{
    public class User
    {
        string name;
        string password;
        string username;
        int id;
        float fees;
        private UserRole role;

        public User(string name, string username, string password, UserRole role,int id, float fees = 0)
        {
            Name = name;
            Username = username;
            Password = password;
            Role = role;
            Fees = fees;
            Id = id;
        }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public UserRole Role { get => role; set => role = value; }
        public string Username { get => username; set => username = value; }
        public int Id { get => id; set => id = value; }
        public float Fees { get => fees; set => fees = value; }


      

        
        


        public static bool IsValidName(string name)
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

        public static bool IsValidUsername(string username)
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

        public static bool IsValidPassword(string password)
        {
            // Password should not be empty and should be at least 8 characters long
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                Console.WriteLine("Invalid password. Please enter a password that is at least 8 characters long.");
                return false;
            }

            return true;
        }

        public static bool IsNameUnique(string name)
        {
            // This is a placeholder method that always returns true
            // You should replace this with your own code that checks if the name already exists in a database or some other data source
            return true;
        }

        public static bool IsUsernameUnique(string username)
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


        // Admin subclass with additional methods
    }

}
