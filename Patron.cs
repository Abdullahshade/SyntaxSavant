using System;
using System.Collections.Generic;


namespace UML_Project
{
    public class Patron : User
    {
        string[] contactInformation;
        List<Book> borrowedItems;

        public Patron(string name, string username, string password, UserRole role, int id):base(name, username,password,role, id)        
        {
            ContactInformation = new string[0];
            BorrowedItems = new List<Book>();
        }

        public string[] ContactInformation { get => contactInformation; set => contactInformation = value; }
        public List<Book> BorrowedItems { get => borrowedItems; set => borrowedItems = value; }

        public bool RequestBorrowing(LibrarySystem lb)
        {
            Console.Write("Enter the title of the book you want to borrow: ");
            string title = Console.ReadLine();


            Book book = Librarian.FindBookByTitle(lb,title);
            if (book != null)
            {

                if (book.GetAvailabilityStatus() && book.GetCopies() > 0)
                {
                    int cout = book.GetCopies();
                    BorrowedItems.Add(book);
                    book.SetCopies(cout--);

                    Console.WriteLine("Book borrowed successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("The book is not available for borrowing.");
                }
            }
            else
            {
                Console.WriteLine("Book not found in the library.");
            }

            return false;
        }


        public void RequestReservation(LibrarySystem lb)
        {
            Console.Write("Enter the title of the book you want to reserve: ");
            string title = Console.ReadLine();

            // Check if a book is available in the library by title
            Book book = Librarian.FindBookByTitle(lb,title);
            if (book != null)
            {
                if (book.GetAvailabilityStatus() && book.GetCopies() > 0)             // Check availability of copies available for reservation
                {
                    BorrowedItems.Add(book);

                    Console.WriteLine("Book reserved successfully.");
                }
                else
                {
                    Console.WriteLine("The book is not available for reservation.");
                }
            }
            else
            {
                Console.WriteLine("Book not found in the library.");
            }
        }

    }
}
