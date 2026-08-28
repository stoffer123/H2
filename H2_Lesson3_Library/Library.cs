using H2_Lesson2_BooksNBorrowers;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson3_Library
{
    public class Library
    {
        public List<Book> Books { get; private set; } = new();


        /// <summary>
        /// Set borrowed status true on a book
        /// </summary>
        /// <param name="isbn"></param>
        public void BorrowBook(Isbn isbn)
        {
            Book? book = Books.FirstOrDefault(b => b.Isbn == isbn);

            if(book is null)
            {
                throw new InvalidOperationException($"Bogen med ISBN: {book.Isbn} findes ikke");
            }

            book.Checkout();
        }

        public void ReturnBook(Isbn isbn)
        {
            Book? book = Books.FirstOrDefault(b => b.Isbn == isbn);

            if(book is null)
            {
                throw new InvalidOperationException($"Bogen med ISBN: {book.Isbn} findes ikke");
            }

            book.Return();
        }

        /// <summary>
        /// Finder bøger hvor Titlen indeholder det søgte
        /// </summary>
        /// <param name="title"></param>
        /// <returns><see cref="IReadOnlyList{T}"/> af <see cref="Book"/></returns>
        public IReadOnlyList<Book> FindBook(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Søgning må ikke være tom", nameof(title));
            }

            return Books.Where(x => x.Title.Contains(title)).ToList();
        }

        /// <summary>
        /// Laver et <see cref="Library"/> seeded med test-data
        /// </summary>
        /// <returns><see cref="Library"/></returns>
        public static Library CreateWithTestData()
        {
            Library library = new();

            library.Books.Add(new Book("BookNumber1", "SomeAuthor1", "0000000000001", 2010));
            library.Books.Add(new Book("BookNumber2", "SomeAuthor2", "0000000000002", 2010));
            library.Books.Add(new Book("BookNumber3", "SomeAuthor3", "0000000000003", 2010));
            library.Books.Add(new Book("BookNumber4", "SomeAuthor4", "0000000000004", 2010));
            library.Books.Add(new Book("BookNumber5", "SomeAuthor5", "0000000000005", 2010));
            library.Books.Add(new Book("BookNumber6", "SomeAuthor6", "0000000000006", 2010));

            return library;
        }
    }
}
