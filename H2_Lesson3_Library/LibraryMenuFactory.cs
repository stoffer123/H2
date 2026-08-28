using H2_Lesson2_BooksNBorrowers;
using MenuProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson3_Library
{
    public class LibraryMenuFactory : IMenuFactory
    {
        private readonly Library _library;

        public LibraryMenuFactory(Library library)
        {
            _library = library;
        }

        public IMenu CreateMenu()
        {
            return new MenuBuilder("BibliotekSystem")
                .AddOption("Vis alle bøger", ShowAllBooks)
                .AddOption("Søg efter bog", SearchBook)
                .AddOption("Lån bog", BorrowBook)
                .AddOption("Aflever bog", ReturnBook)
                .AddOption("Afslut", () => Environment.Exit(0))
                .Build();
        }

        private void ShowAllBooks()
        {
            foreach (var book in _library.Books)
            {
                Console.WriteLine($"{book.Title} af {book.Author} - ISBN: {book.Isbn}");
            }
        }

        private void SearchBook()
        {
            Console.Write("Søg efter titel: ");
            string title = Console.ReadLine();
            var results = _library.FindBook(title);

            if (results.Count == 0)
            {
                Console.WriteLine("Ingen bøger fundet.");
            }
            else
            {
                foreach (var book in results)
                {
                    Console.WriteLine($"{book.Title} af {book.Author} - ISBN: {book.Isbn}");
                }
            }
        }

        private void BorrowBook()
        {
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();
            _library.BorrowBook(new Isbn(isbn));
            Console.WriteLine("Bog udlånt!");
        }

        private void ReturnBook()
        {
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();
            _library.ReturnBook(new Isbn(isbn));
            Console.WriteLine("Bog returneret!");
        }
    }
}
