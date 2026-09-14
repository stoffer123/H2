using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace H2_LINQ1
{
    internal struct Price : IComparable<Price>
    {
        public decimal Value { get; }

        public Price(decimal value)
        {
            if(value < 0)
            {
                throw new ArgumentException("Price cant be negative");
            }
            Value = value;
        }



        public override string ToString() => Value.ToString();
        public override bool Equals(object? obj) => obj is Price other && this == other;
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo(Price other) => Value.CompareTo(other.Value);

        //Implicit operator så price kan sammenlignes med decimaler
        public static implicit operator decimal(Price p) => p.Value;

        //Sammenligningsoperator med anden Price
        public static bool operator ==(Price left, Price right) => left.Value == right.Value;
        public static bool operator !=(Price left, Price right) => left.Value != right.Value;
        public static bool operator >=(Price left, Price right) => left.Value >= right.Value;
        public static bool operator <=(Price left, Price right) => left.Value <= right.Value;
        public static bool operator >(Price left, Price right) => left.Value > right.Value;
        public static bool operator <(Price left, Price right) => left.Value < right.Value;
    }
}
