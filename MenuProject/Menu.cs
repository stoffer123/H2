using System;
using System.Collections.Generic;
using System.Text;

namespace MenuProject
{
    public class Menu : IMenu
    {
        private List<MenuOption> _options = new();
        private string _title;

        public Menu(string title)
        {
            _title = title;
        }

        public void AddOption(string description, Action action)
        {
            _options.Add(new MenuOption(description, action));
        }

        public void Display()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine($"=== {_title} ===\n");

                for(int i = 0; i < _options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_options[i].Description}");
                }

                Console.Write($"\nVælg (1-{_options.Count}): ");
                string input = Console.ReadLine();

                if(int.TryParse(input, out int choice) && choice > 0 && choice <= _options.Count)
                {
                    Console.Clear();
                    try
                    {
                        _options[choice - 1].Action?.Invoke();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Fejl: {ex.Message}");
                    }
                    Console.WriteLine("\nTryk en tast for at fortsætte...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg! Prøv igen.");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
