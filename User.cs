using System;


namespace UML_Project
{
    public class User
    {
        string name;
        string password;
        string username;
        int id;
        double fees;
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
        public double Fees { get => fees; set => fees = value; }

        

        public void ViewFines()
        {
            Console.Write("*Note*\nfree borrowe time is 72 hours, after that you will be charged 0.5 JOD for every 5 hours late\n******\n");
            Console.WriteLine("| Outstanding Fines: {0}", Fees);
        }
 
    }

}