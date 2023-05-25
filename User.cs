using System;


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

        public User(string name, string username, string password, UserRole role, int id, float fees = 0)
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

        

        public void ViewFines()
        {
            Console.WriteLine("Outstanding Fines: {0}", Fees);
        }
 
    }

}