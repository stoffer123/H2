using System;
using System.Collections.Generic;
using System.Text;

namespace MenuProject
{
    public class MenuOption
    {
        public string Description { get; set; }
        public Action Action { get; set; }

        public MenuOption(string description, Action action)
        {
            Description = description;
            Action = action;
        }
    }
}
