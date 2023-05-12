
using static System.Reflection.Metadata.BlobBuilder;

namespace UML_Project
{
    class Catalog
    {
        Dictionary<string, Book> Books = new Dictionary<string, Book>();

        public void AddBookToGenre(Book book)
        {


            Books.Add(book.ISBN1, book);

        }
        public void RemoveBookFromGenre(Book book)
        {
            Books.Remove(book.ISBN1);
        }
        public void ShowGenre()
        {
            Console.Write("Enter the name of the book: ");
            string bookName = Console.ReadLine();
            bool found = false;
            foreach (var key in Books.Keys)
            {
                if (Books[key].Title == bookName)
                    Console.WriteLine("Book gener is : " + Books[key].Genre);
                found = true;
            }
            if (found == false)
                Console.WriteLine("The book not found!");
        }



    }
    class Book
    {
        string title;
        string author;
        int publicationyear;
        string genre;
        string ISBN;
        bool availabilitystatus;
        int copies;
        List<string> previousISBNs = new List<string>();

        public string ISBN1 { get => ISBN; set => ISBN = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Title { get => title; set => title = value; }

        public Book(string title = "No Title", string author = "No Author", int publicationyear = 0000, string genre = "No Genre", int ISBN = 00, int copies = 1, bool availabilitystatus = true)
        {
            this.title = title;
            this.author = author;
            this.publicationyear = publicationyear;
            this.genre = genre;
            this.ISBN1 = GenerateISBN();
            this.availabilitystatus = availabilitystatus;
            this.copies = copies;
        }

        public string GenerateISBN()
        {
            Random random = new Random();
            string isbn = "978";
            bool isUnique = false;

            while (!isUnique)
            {
                for (int i = 0; i < 7; i++)
                {
                    isbn += random.Next(0, 10);
                }

                // Calculate the check digit
                int sum = 0;
                for (int i = 0; i < isbn.Length; i++)
                {
                    int digit = int.Parse(isbn[i].ToString());
                    sum += (i % 2 == 0) ? digit : digit * 3;
                }
                int checkDigit = (10 - sum % 10) % 10;

                isbn += checkDigit.ToString();

                // Check if the generated ISBN is unique
                if (!previousISBNs.Contains(isbn))
                {
                    isUnique = true;
                    previousISBNs.Add(isbn);
                }
                else
                {
                    isbn = "978"; // Reset the ISBN to generate a new one
                }
            }

            return isbn;
        }
        public void ShowBookInfo()
        {


            Console.WriteLine("Title: " + this.title);
            Console.WriteLine("Author: " + this.author);
            Console.WriteLine("Publication Year: " + this.publicationyear);
            Console.WriteLine("Genre: " + this.genre);
            Console.WriteLine("ISBN: " + this.ISBN1);
            Console.Write("Availability Status: ");
            if (this.availabilitystatus)
                Console.WriteLine("The book is available.");
            else
                Console.WriteLine("The book is  not available.");
            Console.WriteLine("Number of Copies: " + this.copies);



        }
        public void EditCopiesNumber()
        {
            bool validinput = false;
            int newcopies = 0;

            while (!validinput)
            {
                try
                {
                    Console.Write("Enter the new number of copies: ");
                    string s = Console.ReadLine();
                    newcopies = Convert.ToInt32(s);
                    validinput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid iuput ,Please enter valid input .");
                }
            }
            this.copies = newcopies;
            Console.WriteLine("Number of copies updated successfully. ");




        }
        public void EditAvabliabiltiyStatus()
        {
            bool validinput = false;
            bool temp_status = true;
            while (!validinput)
            {
                try
                {
                    Console.Write("Enter the new availability status (true or false): ");
                    string s = Console.ReadLine();
                    temp_status = Convert.ToBoolean(s);
                    validinput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input, please enter either 'true' or 'false'.");
                }
            }
            this.availabilitystatus = temp_status;
            Console.WriteLine("Availability status updated successfully.");
        }
        public void SendNewBookInfo(Catalog c)
        {
            //set title 
            Console.Write("Enter the new  title of the book : ");
            this.title = Console.ReadLine();
            //set author
            Console.Write("Enter the author of the book : ");
            this.author = Console.ReadLine();
            Console.Write("Enter the genre of the book : ");
            this.genre = Console.ReadLine();
            this.SetPublicationYear();
            this.SetCopies();
            this.availabilitystatus = true;
            c.AddBookToGenre(this);
        }

        void SetPublicationYear()
        {//set publicationyear 

            // start
            bool validinput = false;
            int temp_publicationyear = 0000;

            while (!validinput)
            {
                try
                {
                    Console.Write("Enter the publicationyear of the book : ");
                    string s = Console.ReadLine();
                    temp_publicationyear = Convert.ToInt32(s);
                    validinput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid iuput ,Please enter valid year  .");
                }
            }
            this.publicationyear = temp_publicationyear;
            //end of publictonyear

        }
        void SetCopies()
        {
            bool validin = false;
            int newcopies = 0;

            while (!validin)
            {
                try
                {
                    Console.Write("Enter the  number of copies: ");
                    string s = Console.ReadLine();
                    newcopies = Convert.ToInt32(s);
                    validin = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid iuput ,Please enter valid input .");
                }
            }
            this.copies = newcopies;
        }
        public void ShowISBN()
        {
            Console.WriteLine("ISBN: " + this.ISBN1);
        }


    }

        public class Librarian
        {

            static  List<Book> books = new List<Book>();

            private string[] workSchedule;
            private string emailAddress;

            public Librarian(string[] workSchedule = null, string emailAddress = "")
            {

                this.workSchedule = workSchedule ?? new string[0];
                this.emailAddress = emailAddress;
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
      

    class Program
            {
                static void Main(string[] args)
                {
                    Librarian L3 = new Librarian();
                    Librarian L2 = new Librarian();
                    Librarian L1 = new Librarian();

                    L1.AddNewBook("Book 1", "Author 1", 2021, "Genre 1", 145237863, 5);
                    L3.Numberofbooks();

                    L1.AddNewBook("Book 2", "Author 2", 2022, "Genre 2", 932587416, 3);
                   
                    L1.AddNewBook("Book 3", "Author 3", 2000, "Genre 2", 932587416, 9);
                L3.Numberofbooks();
                L1.RemoveBook("Book 1");
                    L1.RemoveBook("C++");
                    L3.Numberofbooks();


                }
            }
        

    
}
