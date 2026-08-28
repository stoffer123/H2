using System;
using System.Collections.Generic;
using System.Text;

namespace MenuProject
{
    public class MenuBuilder
    {
        private Menu _menu;

        public MenuBuilder(string title)
        {
            _menu = new Menu(title);
        }

        public MenuBuilder AddOption(string description, Action action)
        {
            _menu.AddOption(description, action);
            return this;
        }

        public IMenu Build()
        {
            return _menu;
        }
    }
}
