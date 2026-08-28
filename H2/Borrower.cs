using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson2_BooksNBorrowers
{
    internal class Borrower
    {
		private string _name;
		private string _borrowerNumber;
		private int _numberOfBooksLoaned;

		public Borrower(string name, string borrowerNumber)
		{
			Name = name;
			BorrowerNumber = borrowerNumber;
			NumberOfBooksLoaned = 0;
		}

		public int NumberOfBooksLoaned
		{
			get { return _numberOfBooksLoaned; }
			private set 
			{
				if(value < 0)
				{
					return;
				}
				_numberOfBooksLoaned = value; 
			}
		}

        public string BorrowerNumber
        {
            get
            {
                return _borrowerNumber;
            }
            init;
        }


        public string Name
		{
			get { return _name; }
			set 
			{
				//Guards
				if(string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Name må ikke være null eller empty");
				}
				_name = value; 
			}
		}

		public void BorrowBook()
		{
			if(NumberOfBooksLoaned > 5)
			{
				throw new InvalidOperationException("Du må maks låne 5 bøger ad gangen");
			}

			NumberOfBooksLoaned++;
		}
		
		public void ReturnBook()
		{
			NumberOfBooksLoaned--;
		}

	}
}
