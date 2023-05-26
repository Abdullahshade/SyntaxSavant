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


        public static void GenerateFinancialReport(List<Patron> patrons)
{
    double totalCollectedFines = 0;
    double totalOutstandingPayments = 0;

    Console.WriteLine("Financial Report");
    Console.WriteLine("----------------");

    foreach (Patron patron in patrons)
    {
        patron.updateFines();

        if (patron.Fees > 0)
        {
            totalCollectedFines += patron.Fees;
            Console.WriteLine($"Patron: {patron.Name}");
            Console.WriteLine($"Collected Fines: ${patron.Fees}");
            Console.WriteLine();
        }
        else
        {
            totalOutstandingPayments += Math.Abs(patron.Fees);
        }
    }

    Console.WriteLine("Summary");
    Console.WriteLine($"Total Collected Fines: ${totalCollectedFines}");
    Console.WriteLine($"Total Outstanding Payments: ${totalOutstandingPayments}");
}

        public void ViewFines()
        {
            Console.Write("*Note*\nfree borrowe time is 72 hours, after that you will be charged 0.5 JOD for every 5 hours late\n******\n");
            Console.WriteLine("| Outstanding Fines: {0}", Fees);
        }

    }

}
