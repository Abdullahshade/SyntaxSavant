using System;
using System.Collections.Generic;


namespace UML_Project
{
    public class Librarian : User
    {


        private string[] workSchedule;
        private string emailAddress;

        public Librarian(string name, string username, string password, UserRole role, int id, string emailAddress = "", string[] workSchedule = null) : base(name, username, password, role, id)
        {
            
            this.workSchedule = workSchedule ?? new string[0];
            this.emailAddress = emailAddress;
        }

        public string[] WorkSchedule { get => workSchedule; set => workSchedule = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }

        public static Book FindBookByTitle(LibrarySystem lb,string title)
        {
            return lb.FindBookByTitle(title);
        }

        public void AddNewBook(LibrarySystem lb, string title, string author, int publicationYear, string genre, int isbn, int copies)
        {




            for (int j = 1; j <= copies; j++)
            {

                Book book = new Book(title, author, publicationYear, genre, isbn, 1);
                lb.AddBookToBooks(book);

            }

            Console.WriteLine("New book added: " + title);
        }
        public void Numberofbooks(LibrarySystem lb)
        {
            Console.WriteLine("The number of books in the library: " + lb.NumberOfBooks());
        }


        public void RemoveBook(LibrarySystem lb,string title)
        {



            string searchTitle = title;
            Book foundBook = lb.FindBookByTitle(title);

            if (foundBook != null)
            {
                Console.WriteLine("Book found:");
                // foundBook.ShowBookInfo();
                lb.DeleteBook(foundBook);
                Console.WriteLine("Book removed successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }


        }
    }
}
