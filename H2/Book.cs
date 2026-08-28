using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace H2_Lesson2_BooksNBorrowers
{
    public class Book
    {
        private string _title;
        private string _author;
        private Isbn _isbn;
        private int _publicationYear;
        private bool _isOnLoan;


        public Book(string title, string author, string isbn, int publicationYear)
        {
            //Brug properties til at sætte for valideringen i Setteren
			Title = title;
			Author = author;
            Isbn = new Isbn(isbn);
			PublicationYear = publicationYear;
			IsOnLoan = false;
        }

		public Book(string title, string author) : this(title, author, "0000000000000", 0) { }

        public string Title
        { 
            get { return _title; }
            private set
            {
                if(String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title må ikke være null eller empty");
                }
                _title = value;
            }
        }

        public string Author
        {
            get { return _author; }
            private set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title må ikke være null eller empty");
                }
                _author = value;
            }
        }
        public Isbn Isbn
        {
            get { return _isbn; }
            private set { _isbn = value; }
        }
        public int PublicationYear
        {
            get { return _publicationYear; }
            private set { _publicationYear = value; }
        }
        public bool IsOnLoan
        {
            get { return _isOnLoan; }
            private set { _isOnLoan = value; }
        }

        public void Checkout()
        {
            if(IsOnLoan)
            {
                throw new InvalidOperationException("Bogen er allerede udlånt");
            }

            IsOnLoan = true;
        }

        public void Return()
        {
            IsOnLoan = false;
        }

	}
}
