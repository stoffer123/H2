using System;
using System.Collections.Generic;
using System.Text;

namespace MenuProject
{
    public interface IMenu
    {
        void AddOption(string description, Action action);
        void Display();
    }
}
