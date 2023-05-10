using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Project;

namespace UML_Project
{
    internal class main
    {

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
                            Console.WriteLine("Logged in as " + currentUser.Name);
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
                    if (currentUser is Admin)
                    {
                        // If user is admin, show additional options
                        Console.WriteLine("Do you want to view all users (1), set a user's role (2), or log out (3)?");
                        int adminChoice = int.Parse(Console.ReadLine());

                        if (adminChoice == 1)
                        {
                            List<User> users = librarySystem.GetUsers();
                            ((Admin)currentUser).ManageUserAccounts(users);
                        }
                        else if (adminChoice == 2)
                        {
                            Console.WriteLine("Enter the username of the user whose role you want to change:");
                            string username = Console.ReadLine();
                            User userToChange = librarySystem.GetUsers().Find(u => u.Username == username);

                            if (userToChange != null)
                            {
                                Console.WriteLine("Enter the new role for the user:");
                                UserRole newRole = (UserRole)Enum.Parse(typeof(UserRole), Console.ReadLine());
                                ((Admin)currentUser).SetRole(userToChange, newRole);
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
