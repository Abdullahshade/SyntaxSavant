using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UML
{ class Book
    {
        string title;
        string author;
        int publicationyear;
        string genre;
        int ISBN;
        bool availabilitystatus;
        int copies;


        public Book ShowBookInfo()
        {
            Book tempbook = new Book();
            return tempbook;
        }
        public void  EditCopiesNumber()
        {

        }
        public void EditAvabliabiltiyStatus()
        {

        }
        public void SendNewBookInfo()
        {

        }
        public void ShowISBN()
        {

        }


    }
  
    class Catalog
    {
        List<Book> booksgenre = new List<Book>();
        public void  AddBookToGenre()
        {

        }
        public void RemoveBookFromGenre()
        {

        }
        public void ShowGenre()
        {

        }

    }
    class Program
    {

        static void Main(string[] args)
        {

            
            
        }
    }
}
