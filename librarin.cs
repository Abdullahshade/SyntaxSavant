using System;



namespace UML_Project
{
    public class Librarian : User
    {


        private string[] workSchedule;
        private int duedate;
        // private string emailAddress;

        public Librarian(string name, string username, string password, int id, string[] workSchedule = null) : base(name, username, password, UserRole.Librarian, id)
        {

            this.workSchedule = workSchedule ?? new string[0];
            //this.emailAddress = emailAddress;
        }

        public string[] WorkSchedule { get => workSchedule; set => workSchedule = value; }
        //public string EmailAddress { get => emailAddress; set => emailAddress = value; }

        public static Book FindBookByTitle(LibrarySystem lb, string title)
        {
            return lb.FindBookByTitle(title);
        }
        public static Book FindBookByAuthor(LibrarySystem lb, string author)
        {
            return lb.FindBookByAuthor(author);
        }
        public static Book FindBookByISBN(LibrarySystem lb, string ISBN)
        {
            return lb.FindBookByISBN(ISBN);
        }
        public static Book FindBookByGenre(LibrarySystem lb, string genre)
        {
            return lb.FindBookByGenre(genre);
        }

        public void AddNewBook(LibrarySystem lb, string title, string author, int publicationYear, string genre, int isbn, int copies)
        {





            Book book = new Book(title, author, publicationYear, genre, isbn, copies);
            lb.AddBookToBooks(book);



            Console.WriteLine("New book added: " + title);
        }
        public void Numberofbooks(LibrarySystem lb)
        {
            Console.WriteLine("The number of books in the library: " + lb.NumberOfBooks());
        }


        public void RemoveBook(LibrarySystem lb, string title)
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
        public bool CheckOut(Book book, int borrowDuration) {
            DateTime dueDate = DateTime.Now.AddDays(borrowDuration);
            if (book.GetAvailabilityStatus())
            {
                book.Availabilitystatus=false ;
                book.Duetime = dueDate;
               

                return true;
            }
            else
            {
                Console.WriteLine("The book '{0}' is not available for checkout.", book.Title);
                return false;
            }

        }
         

    }
}