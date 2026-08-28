using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson2_BooksNBorrowers
{
    public struct Isbn : IEquatable<Isbn>
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

        public override bool Equals(object obj)
        {
            return obj is Isbn isbn && Equals(isbn);
        }

        public bool Equals(Isbn other)
        {
            return _value == other._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString() => _value;

        public static implicit operator string(Isbn isbn) => isbn._value;
        public static implicit operator Isbn(string value) => new Isbn(value);
        public static bool operator ==(Isbn left, Isbn right) => left.Equals(right);
        public static bool operator !=(Isbn left, Isbn right) => !left.Equals(right);
    }
}
