using BaseCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BaseCore.Enumerations
{
    public abstract class Enumeration : IComparable
    {
        public string Id { get; set; }
        public string Name { get; set; }

        protected Enumeration(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static bool Contains<T>(string Id) where T : Enumeration
        {
            var fields = GetAll<T>().ToList();
            return fields.Select(field => field.Id).Contains(Id);
        }

        public static string GetName<T>(string Id) where T : Enumeration
        {
            var fields = GetAll<T>().ToList();
            return fields.FirstOrDefault(field => Id == field.Id)?.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var otherValue = obj as Enumeration;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object obj)
        {
            return this.Id.CompareTo(((Enumeration)obj).Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public static bool operator ==(Enumeration primeiro, Enumeration segundo) => primeiro?.Id == segundo?.Id && primeiro?.Name == segundo?.Name;
        public static bool operator !=(Enumeration primeiro, Enumeration segundo) => !(primeiro == segundo);
        public static bool operator >(Enumeration primeiro, Enumeration segundo) => throw new EnumerationIvalidOperatorException(primeiro, segundo, ">");
        public static bool operator <(Enumeration primeiro, Enumeration segundo) => throw new EnumerationIvalidOperatorException(primeiro, segundo, "<");
        public static bool operator >=(Enumeration primeiro, Enumeration segundo) => throw new EnumerationIvalidOperatorException(primeiro, segundo, ">=");
        public static bool operator <=(Enumeration primeiro, Enumeration segundo) => throw new EnumerationIvalidOperatorException(primeiro, segundo, "<=");

    }
}
