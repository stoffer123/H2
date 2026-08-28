using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson2_BooksNBorrowers
{
    internal struct Isbn
    {
        private readonly string _value;

        public Isbn(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("ISBN må ikke være null eller tom");
            }

            if(value.Length != 13)
            {
                throw new ArgumentException("ISBN skal være præcis 13 cifre");
            }

            if(!value.All(char.IsDigit))
            {
                throw new ArgumentException("ISBN Må kun indeholde cifre");
            }

            _value = value;
        }

        public override string ToString() => _value;
        public static implicit operator string(Isbn isbn) => isbn._value;
        public static implicit operator Isbn(string value) => new Isbn(value);
    }
}
