//Lav 3 book instanser, med forskellige ctor
using H2_Lesson2_BooksNBorrowers;
try
{
    var book1 = new Book("BookNumber1", "SomeAuthor", "0000000000001", 2010);
    var book2 = new Book("BookNumber2", "SomeAuthor", "0000000000002", 2010);
    var book3 = new Book("BookNumber3", "SomeAuthor");

    //Lav 2 Borrowers
    var borrower1 = new Borrower("Borrower1", "1");
    var borrower2 = new Borrower("Borrower2", "2");

    //En borrower låner en bog
    borrower1.BorrowBook();
    book1.Checkout();

    //Kald checkout på en bog der allerede er udlånt
    book1.Checkout();

}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

/*
 * Begrundelse for valg af access modifiers:
 * 
 * 1. private string _name (Borrower-klasse backing field):
 *    Private fordi direkte adgang ville springe Name-setterens validering over.
 *    Kun gennem Name-property kan vi garantere at navn ikke er null/tomt.
 * 
 * 2. public string BorrowerNumber { get; init; }:
 *    Getter er public så låner nummer kan læses. Setter er init (ikke set) fordi
 *    låner nummer er immutable efter konstruktion.
 * 
 * 3. private set { } på NumberOfBooksLoaned:
 *    Antallet skal KUN ændres via BorrowBook() og ReturnBook(), som har forretningslogik.
 *    Direkte adgang ville kunne bryde reglerne.
 * 
 */