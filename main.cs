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
                    Console.Clear();
                    // Prompt the user to choose between registering a new user or logging in with an existing user
                    Console.WriteLine("Welcome to the library system!");
                    Console.WriteLine("[1]register a new user \n[2]log in with an existing user");
                    int choice = int.Parse(Console.ReadLine());

                    if (choice == 1)
                    {
                        // Register a new user
                        librarySystem.Register();
                        Thread.Sleep(1000);

                    }
                    else if (choice == 2)
                    {
                        // Log in with an existing user
                        currentUser = librarySystem.Login();

                        if (currentUser != null)
                        {
                            Console.Clear();

                            Console.WriteLine("Logged in as " + currentUser.Name);
                        }
                        else
                        {

                            Console.WriteLine("Invalid username or password.");
                            Thread.Sleep(1000);

                        }
                    }
                    else
                    {

                        Console.WriteLine("Invalid choice.");
                        Thread.Sleep(1000);

                    }
                }
                else
                {
                    // User is logged in
                    if (currentUser is Admin)
                    {
                        // If user is admin, show additional options
                        Console.WriteLine("[1]Do you want to view all users\n[2]set a user's role \n[3]To remove user\n[4]log out");
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
                                librarySystem.SaveUsersToFile();

                            }
                            else
                            {
                                Console.WriteLine("User not found.");
                            }

                        }
                        else if (adminChoice == 3)
                        {
                            Console.WriteLine("Enter the username of the user to be deleted:");
                            string username = Console.ReadLine();
                            User userToDelete = librarySystem.GetUsers().Find(u => u.Username == username);

                            if (userToDelete != null)
                            {
                                ((Admin)currentUser).DeleteUser(librarySystem, userToDelete);
                                librarySystem.SaveUsersToFile();

                            }
                            else
                            {
                                Console.WriteLine("User not found.");
                            }

                        }
                        else if (adminChoice == 4)
                        {
                            currentUser = null;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if(currentUser is Librarian)
                    {


                        Console.WriteLine("[1]Add new Book\n[2]View number of books \n[3]search book by title\n[4]remove book by name\n[5]logout");
                        int LibrarinChoice = int.Parse(Console.ReadLine());

                        if (LibrarinChoice == 1)
                        {
                            Console.Write("Enter book Title:");
                            string title = Console.ReadLine();
                            Console.Write("Enter book author:");
                            string author= Console.ReadLine();
                            Console.Write("Enter year of publication:");
                            int publicationYear= Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter genre:");

                            string genre = Console.ReadLine();
                            Console.Write("Enter isbn:");

                            int isbn = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter number of copies :");

                            int copies = Convert.ToInt32(Console.ReadLine());
                            ((Librarian)currentUser).AddNewBook(librarySystem, title,  author,  publicationYear,  genre,  isbn,  copies);
                        }
                        else if (LibrarinChoice == 2)
                        {
                        
                           ((Librarian)currentUser).Numberofbooks(librarySystem);
                        }
                        else if(LibrarinChoice == 3)
                        {
                            Console.Write("Enter book Title:");
                            string title = Console.ReadLine();
                            Book FindBook = Librarian.FindBookByTitle(librarySystem,title);
                            if (FindBook != null)
                            {
                                Console.WriteLine("Book Found");
                            }
                            else
                            {
                                Console.WriteLine("Book not found! ");
                            }
                        }
                        else if (LibrarinChoice == 4)
                        {
                            Console.Write("Enter book Title:");
                            string title = Console.ReadLine();
                            ((Librarian)currentUser).RemoveBook(librarySystem, title);

                        }
                        else if (LibrarinChoice == 5)
                        {
                            currentUser = null;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();


                    }
                    else
                    {
                        Console.WriteLine("[1]log out\n[2]to request borrwoing \n[3]Request Reservation\n[4]View Fines ");
                        int userChoice = int.Parse(Console.ReadLine());

                        if (userChoice == 1)
                        {
                            currentUser = null;
                        }
                        else if (userChoice == 2)
                        {
                            // Add additional options for normal users here
                            ((Patron)currentUser).RequestBorrowing(librarySystem);

                        }
                        else if (userChoice == 3)
                        {
                            // Add additional options for normal users here
                            ((Patron)currentUser).RequestReservation(librarySystem);

                        }
                        else if (userChoice == 4)
                        {
                            // Add additional options for normal users here
                            currentUser.ViewFines();

                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }
    }
}
