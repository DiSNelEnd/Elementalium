using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Elementalium.Infrastructure
{
    public class ValueType<T>
    {
        private IEnumerable<PropertyInfo> Properties =>
            GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            var otherPropertyValues = other.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.GetValue(other));
            return Properties.Select(p => p.GetValue(this)).SequenceEqual(otherPropertyValues);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((T)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Properties.Select(p => p.GetValue(this))
                    .Select(p => p
                        .GetHashCode())
                    .Aggregate((res, next) => (res * 379) ^ next);
            }
        }

        public override string ToString()
        {
            return string
                .Join(",", Properties
                    .OrderBy(p => p.Name)
                    .Select(p => p.GetValue(this)?.ToString()));
        }
    }
}
