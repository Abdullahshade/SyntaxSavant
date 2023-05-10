using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
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

        public Book(string title = "No Title", string author = "No Author", int publicationyear = 0000, string genre = "No Genre", int ISBN = 00, bool availabilitystatus = true, int copies = 1)
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
        public void SendNewBookInfo()
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
                    Console.WriteLine("Book gener " + Books[key].Genre);
                found = true;
            }
            if (found == false)
                Console.WriteLine("The book not found!");
        }


        public void print()
        {
            foreach (var key in Books.Keys)
                Console.WriteLine(Books[key].Title);

        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Book b1 = new Book();
            b1.SendNewBookInfo();
            b1.ShowBookInfo();

            Catalog c = new Catalog();
            c.print();

            c.ShowGenre();

        }
    }

}
