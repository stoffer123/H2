using H2_Lesson3_Library;
using MenuProject;

var library = Library.CreateWithTestData();
IMenuFactory menuFactory = new LibraryMenuFactory(library);
IMenu menu = menuFactory.CreateMenu();

menu.Display();
