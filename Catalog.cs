using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
}
