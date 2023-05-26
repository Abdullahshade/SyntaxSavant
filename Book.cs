using System;
using System.Collections.Generic;


namespace UML_Project
{
    [Serializable]

    public class Book
    {
        string title;
        string author;
        int publicationyear;
        string genre;
        string ISBN;
        bool availabilitystatus;
        int copies;
        DateTime borrowedDate;
        DateTime duetime;
        
        public void SetCopies(int newCopies)
        {
            Copies = newCopies;
        }
        public int GetCopies()
        {
            return Copies;
        }
        public bool GetAvailabilityStatus()
        {
            return availabilitystatus;
        }
        public void updateBorrowedDate()
        {
            borrowedDate = DateTime.Now;
        }

        List<string> previousISBNs = new List<string>();

        public string ISBN1 { get => ISBN; set => ISBN = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public DateTime BorrowedDate { get => borrowedDate; set => borrowedDate = value; }
        public DateTime Duetime { get => duetime; set => duetime = value; }
        public bool Availabilitystatus { get => availabilitystatus; set => availabilitystatus = value; }
        public int Copies { get => copies; set => copies = value; }

        public Book(string title = "No Title", string author = "No Author", int publicationyear = 0000, string genre = "No Genre", int ISBN = 00, int copies = 1, bool availabilitystatus = true)
        {
            this.title = title;
            this.author = author;
            this.publicationyear = publicationyear;
            this.genre = genre;
            this.ISBN1 = GenerateISBN();
            this.availabilitystatus = availabilitystatus;
            this.Copies = copies;
        }

        string GenerateISBN()
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
            Console.WriteLine("Number of Copies: " + this.Copies);



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
            this.Copies = newcopies;
            Console.WriteLine("Number of copies updated successfully. ");
        }
        public void EditAvabliabiltiyStatus()
        {
            if (Copies <= 0)
                availabilitystatus = !GetAvailabilityStatus();
            else availabilitystatus = true;

            // Console.WriteLine("Availability status updated successfully.");
        }
        public void SendNewBookInfo(Catalog c)
        {
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
            this.Copies = newcopies;
        }
        public void ShowISBN()
        {
            Console.WriteLine("ISBN: " + this.ISBN1);
        }

    }
}
