using System;
using System.Collections.Generic;
using System.Text;

namespace Elementalium.Infrastructure
{
    public class Entity<T>
    {
        protected bool Equals(T other)
        {
            return GetType().GetProperty("Id").GetValue(this).Equals(other.GetType().GetProperty("Id").GetValue(other));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((T)obj);
        }

        public override int GetHashCode()
        {
            return GetType().GetProperty("Id").GetValue(this).GetHashCode();
        }

        public override string ToString()
        {
            var id = GetType().GetProperty("Id");
            return $"{GetType().Name}({nameof(id)}: {id.GetValue(this)})";
        }
    }
}
