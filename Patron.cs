using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;


namespace UML_Project
{
    public class Patron : User
    {

        string contactInformation;
        string message = null;
        List<Book> borrowedItems;
        List<Book> reserveditems;

        public Patron(string name, string username, string password, string contactInformation, UserRole role, int id) : base(name, username, password, role, id)
        {
            ContactInformation = contactInformation;
            BorrowedItems = new List<Book>();
            Reserveditems = new List<Book>();
            LoadDataFromFile();
            updateFines();
        }

        public string ContactInformation { get => contactInformation; set => contactInformation = value; }
        public string Message { get => message; set => message = value; }
        public List<Book> BorrowedItems { get => borrowedItems; set => borrowedItems = value; }
        public List<Book> Reserveditems { get => reserveditems; set => reserveditems = value; }

        public void DeleteBookFromBorrowedItems(Book book)
        {
            Book bo = BorrowedItems.Find(b => book.Title == b.Title);
            BorrowedItems.Remove(bo);
            SaveDataToFile();
        }
        public void SaveDataToFile()
        {
            FileStream fileStream = new FileStream(Name + "Borrowed.dat", FileMode.OpenOrCreate);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, borrowedItems);
            fileStream.Close();

            FileStream fileStream2 = new FileStream(Name + "reserved.dat", FileMode.OpenOrCreate);
            BinaryFormatter binaryFormatter2 = new BinaryFormatter();
            binaryFormatter2.Serialize(fileStream2, reserveditems);
            fileStream2.Close();
        }
        public void LoadDataFromFile()
        {
            FileStream fileStream = null;
            FileStream fileStream2 = null;
            try
            {
                fileStream = new FileStream(Name + "Borrowed.dat", FileMode.Open);

                BinaryFormatter binaryFormatter = new BinaryFormatter();
                borrowedItems = (List<Book>)binaryFormatter.Deserialize(fileStream);
            }
            catch (FileNotFoundException)
            {
                // File not found, initialize empty list
                borrowedItems = new List<Book>();
            }
            finally
            {
                fileStream?.Close();
            }

            try
            {
                fileStream2 = new FileStream(Name + "Reserved.dat", FileMode.Open);

                BinaryFormatter binaryFormatter2 = new BinaryFormatter();
                Reserveditems = (List<Book>)binaryFormatter2.Deserialize(fileStream2);
            }
            catch (FileNotFoundException)
            {
                // File not found, initialize empty list
                Reserveditems = new List<Book>();
            }
            finally
            {
                fileStream2?.Close();
            }
        }
        public bool RequestBorrowing(LibrarySystem lb)
        {
            LoadDataFromFile();
            Console.Write("Enter the title of the book you want to borrow: ");

            try
            {
                string title = Console.ReadLine();
                Book book = Librarian.FindBookByTitle(lb, title);

                if (book != null)
                {

                    if (!book.GetAvailabilityStatus())
                    {
                        Console.WriteLine("Book already Borrowed ");
                        return false;
                    }

                    BorrowedItems.Add(book);

                    book.updateBorrowedDate();
                    book.EditAvabliabiltiyStatus();

                    lb.SaveBooksToFile();
                    Console.WriteLine("Book borrowed successfully.");
                    SaveDataToFile();

                    return true;

                }

                else
                {
                    Console.WriteLine("Book not found in the library.");
                    return false;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void RequestReservation(LibrarySystem lb)
        {
            Console.Write("Enter the title of the book you want to reserve: ");
            string title = Console.ReadLine();
            Book borrowedBook = BorrowedItems.Find(b => b.Title == title);

            if(borrowedBook != null)// if book already borrowed don't make reservation
            {
                Console.WriteLine("Can't reserve a book you already have");
                return;
            }

            // Check if a book is available in the library by title
            Book book = Librarian.FindBookByTitle(lb, title);
            if (book != null)
            {
                if (!book.GetAvailabilityStatus())             // Check availability reservation
                {
                    Reserveditems.Add(book);

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
            SaveDataToFile();
        }
        public void ShowReservedBooks()
        {
            Console.WriteLine(" Reserved books for the user : ");

            foreach (Book book in Reserveditems)
            {
                Console.WriteLine("|" + book.Title);
            }
        }
        public void ShowBorrowedBooks()
        {
            Console.WriteLine(" borrowed books for the user : ");

            foreach (Book book in borrowedItems)
            {
                Console.WriteLine("|" + book.Title);
            }
        }
        public void updateFines()
        {
            double totalFees = 0;

            foreach (Book bo in borrowedItems)
            {
                TimeSpan dif = DateTime.Now - bo.BorrowedDate;
                if (dif.TotalHours > 72)
                    totalFees += dif.TotalHours / 5;
            }

            double OutstandingFees = totalFees * 0.5;
            Fees += OutstandingFees;
            
        }

    }
}