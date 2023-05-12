using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Project
{
    class Patron
    {
        string[] ContactInformation;
        List<Book> BorrowedItems;

        public Patron()
        {
            ContactInformation = new string[0];
            BorrowedItems = new List<Book>();
        }

        public bool RequestBorrowing()
        {
            Console.Write("Enter the title of the book you want to borrow: ");
            string title = Console.ReadLine();


            Book book = Librarian.FindBookByTitle(title);
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


        public void RequestReservation()
        {
            Console.Write("Enter the title of the book you want to reserve: ");
            string title = Console.ReadLine();

            // Check if a book is available in the library by title
            Book book = Librarian.FindBookByTitle(title);
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
