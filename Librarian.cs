using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML_Project
{
    public class Librarian
    {

        static List<Book> books = new List<Book>();

        private string[] workSchedule;
        private string emailAddress;

        public Librarian(string[] workSchedule = null, string emailAddress = "")
        {

            this.workSchedule = workSchedule ?? new string[0];
            this.emailAddress = emailAddress;
        }

        public static Book FindBookByTitle(string title)
        {
            foreach (Book book in books)
            {
                if (book.Title == title)
                {
                    return book;
                }

            }
            return null;
        }

        public void AddNewBook(string title, string author, int publicationYear, string genre, int isbn, int copies)
        {




            for (int j = 1; j <= copies; j++)
            {

                Book book = new Book(title, author, publicationYear, genre, isbn, 1);
                books.Add(book);
            }

            Console.WriteLine("New book added: " + title);
        }
        public void Numberofbooks()
        {
            Console.WriteLine("The number of books in the library: " + books.Count);
        }


        public void RemoveBook(string title)
        {



            string searchTitle = title;
            Book foundBook = books.Find(book => book.Title == searchTitle);

            if (foundBook != null)
            {
                Console.WriteLine("Book found:");
                // foundBook.ShowBookInfo();
                books.Remove(foundBook);
                Console.WriteLine("Book removed successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }


        }
    }
}
