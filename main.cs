﻿using System;
using System.Collections.Generic;
using System.Threading;


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
                    Console.WriteLine("[1]Login\n[2]Register");

                    try
                    {
                        int choice = int.Parse(Console.ReadLine());

                        if (choice == 2)
                        {
                            // Register a new user
                            librarySystem.Register();
                            Thread.Sleep(1000);

                        }
                        else if (choice == 1)
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
                    catch (FormatException e)
                    {
                        Console.WriteLine("choice cannot be null! ");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    // User is logged in
                    if (currentUser is Admin)
                    {
                        // If user is admin, show additional options
                        Console.WriteLine("[0]Generate library Report\n[1]View all users\n[2]Remove Users\n[3]Modify Users\n[4]log out");
                        try
                        {
                            int adminChoice = int.Parse(Console.ReadLine());

                            if (adminChoice == 0)
                            {
                                ((Admin)currentUser).generateReports(librarySystem);

                            }
                            else if (adminChoice == 1)
                            {
                                List<User> users = librarySystem.GetUsers();
                                ((Admin)currentUser).ManageUserAccounts(users);

                            }
                            else if (adminChoice == 2)
                            {
                                List<User> users = librarySystem.GetUsers();

                                ((Admin)currentUser).ManageUserAccounts(users);

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
                                librarySystem.LoadUsersFromFile();
                            }
                            else if (adminChoice == 3)
                            {
                                List<User> users = librarySystem.GetUsers();

                                ((Admin)currentUser).ManageUserAccounts(users);

                                Console.WriteLine("Enter the username of the user to be modified:");
                                string username = Console.ReadLine();
                                User user2Modify = librarySystem.GetUsers().Find(u => u.Username == username);

                                if (user2Modify != null)
                                {
                                    ((Admin)currentUser).modifyingUsers(librarySystem, user2Modify);
                                }
                                else
                                {
                                    Console.WriteLine("User not found.");
                                }
                                librarySystem.LoadUsersFromFile();
                            }
                            else if (adminChoice == 4)
                            {
                                currentUser = null;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                        }
                        catch (FormatException e)
                        {

                        }
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    // user is a Librarian
                    else if (currentUser is Librarian)
                    {


                        Console.WriteLine("[0]List Books\n[1]Add new Book\n[2]View number of books \n[3]search book\n[4]remove book by name\n[5]checkin book\n[6]logout");
                        try
                        {
                            int LibrarinChoice = int.Parse(Console.ReadLine());

                            if (LibrarinChoice == 0)
                            {
                                librarySystem.ListBooks();
                            }
                            else if (LibrarinChoice == 1)
                            {
                                Console.Write("Enter book Title:");
                                string title = Console.ReadLine();

                                Console.Write("Enter book author:");
                                string author = Console.ReadLine();

                                Console.Write("Enter year of publication:");
                                int publicationYear = Convert.ToInt32(Console.ReadLine());

                                Console.Write("Enter genre:");
                                string genre = Console.ReadLine();

                            

                                ((Librarian)currentUser).AddNewBook(librarySystem, title, author, publicationYear, genre);
                            }
                            else if (LibrarinChoice == 2)
                            {

                                ((Librarian)currentUser).Numberofbooks(librarySystem);
                            }
                            else if (LibrarinChoice == 3)
                            {
                                Console.Write("Search book by:\n[0]Title\n[1]author\n[2]ISBN\n[3]genre\n");
                                int searchBy = int.Parse(Console.ReadLine());
                                if (searchBy == 0)
                                {
                                    Console.Write("Enter book Title:");
                                    string title = Console.ReadLine();
                                    Book FindBook = Librarian.FindBookByTitle(librarySystem, title);
                                    if (FindBook != null)
                                    {
                                        FindBook.ShowBookInfo();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Book not found! ");
                                    }
                                }
                                else if (searchBy == 1)
                                {
                                    Console.Write("Enter book author:");
                                    string author = Console.ReadLine();
                                    Book FindBook = Librarian.FindBookByAuthor(librarySystem, author);
                                    if (FindBook != null)
                                    {
                                        Console.WriteLine("-------------------");

                                    }
                                    else
                                    {
                                        Console.WriteLine("Book not found! ");
                                    }

                                }
                                else if (searchBy == 2)
                                {
                                    Console.Write("Enter book ISBN:");
                                    string ISBN = Console.ReadLine();
                                    Book FindBook = Librarian.FindBookByISBN(librarySystem, ISBN);
                                    if (FindBook != null)
                                    {
                                        FindBook.ShowBookInfo();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Book not found! ");
                                    }

                                }
                                else if (searchBy == 3)
                                {
                                    Console.Write("Enter book genre:");
                                    string genre = Console.ReadLine();
                                    Book FindBook = Librarian.FindBookByGenre(librarySystem, genre);
                                    if (FindBook != null)
                                    {
                                        Console.WriteLine("-------------------");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Book not found! ");
                                    }

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
                                Console.WriteLine("Enter the title of the book: ");
                                string title = Console.ReadLine();
                                librarySystem.ReturnBook(title);
                                
                                Console.WriteLine("Enter The username of the patron");
                                string username = Console.ReadLine();
                                librarySystem.GetNameOfPatronToCheckInBook(username, title);
                                Console.WriteLine("Book checked in Successfully");
                            }
                            else if (LibrarinChoice == 6)
                            {
                                currentUser = null;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();


                    }
                    // user is a patron
                    else
                    {
                        Console.WriteLine("[1]My Books\n[2]Request borrwoing \n[3]Request Reservation\n[4]View Fines\n[5]List Books\n[6]Notifications and Reminders\n[7]Messages\n[8]return book\n[9]log out");
                        try
                        {
                            int userChoice = int.Parse(Console.ReadLine());

                            if (userChoice == 1)
                            {
                                // List books in the system
                                ((Patron)currentUser).ShowBorrowedBooks();

                                ((Patron)currentUser).ShowReservedBooks();
                            }
                            else if (userChoice == 2)
                            {
                                // Add additional options for normal users here
                                ((Patron)currentUser).RequestBorrowing(librarySystem);
                                librarySystem.SaveBooksToFile();

                            }
                            else if (userChoice == 3)
                            {
                                // Add additional options for normal users here
                                ((Patron)currentUser).RequestReservation(librarySystem);
                                librarySystem.SaveBooksToFile();

                            }
                            else if (userChoice == 4)
                            {

                                ((Patron)currentUser).updateFines();

                                // Add additional options for normal users here
                                currentUser.ViewFines();


                            }
                            else if (userChoice == 5)
                            {
                                // List books in the system
                                librarySystem.ListBooks();
                            }
                            else if (userChoice == 6)
                            {

                            }
                            else if (userChoice == 7)
                            {

                            }
                            else if (userChoice == 8)
                            {

                                Console.WriteLine("Enter the title of the book: ");
                                string title = Console.ReadLine();

                                Book borrowedBook = ((Patron)currentUser).BorrowedItems.Find(b => b.Title == title);// make sure the patron borrwed the book
                                try {
                                    if (title == borrowedBook.Title && borrowedBook != null)
                                    {
                                        Book book = librarySystem.GetBooks().Find(b => b.Title == title);
                                        ((Patron)currentUser).updateFines();
                                        ((Patron)currentUser).DeleteBookFromBorrowedItems(book);
                                        ((Patron)currentUser).LoadDataFromFile();
                                        librarySystem.ReturnBook(title);
                                        librarySystem.LoadBooksFromFile();

                                        Console.WriteLine("Book Returned Successfully");

                                        Console.WriteLine("Outstanding Fees: {0}", currentUser.Fees);
                                        if (currentUser.Fees != 0)
                                        {
                                            Console.WriteLine("Do you wish to pay? y for yes, n for no");
                                            string c = Console.ReadLine();
                                            c.ToCharArray();
                                            if (c[0] == 'y')
                                            {
                                                Console.WriteLine("Press Enter to pay");
                                                Console.ReadKey();
                                                Console.WriteLine("Paid Successfully");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Book isn't borrowed");
                                    }
                                }
                                catch (NullReferenceException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                }
                            else if (userChoice == 9)
                            {
                                currentUser = null;
                            }

                            else
                            {
                                Console.WriteLine("Invalid choice.");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
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