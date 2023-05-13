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
                    Console.WriteLine("Do you want to register a new user (1) or log in with an existing user (2)?");
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
                        Console.WriteLine("Do you want to view all users (1), set a user's role (2), or log out (3)?");
                        int adminChoice = int.Parse(Console.ReadLine());

                        if (adminChoice == 1)
                        {
                            List<User> users = librarySystem.GetUsers();
                            ((Admin)currentUser).ManageUserAccounts(users);
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
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
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                Console.Clear();

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
                    else if(currentUser is Librarian)
                    {


                        Console.WriteLine("Add new Book (1),View number of books (2), search book by title(3),remove book by name(4), logout(5)");
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
                            Console.WriteLine("New book added ! Press any key to continue.");
                            Console.ReadKey();
                            Console.Clear();
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
