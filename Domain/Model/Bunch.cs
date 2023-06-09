using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elementalium.Infrastructure;

namespace Elementalium.Domain.Model
{
    public class Bunch
    {
        public string Id { get; }
        public DateTime CreationTime { get; set; }
        public List<int> IdElementList { get; }

        public Bunch(List<int> idElements, DateTime creationTime)
        {
            IdElementList = idElements;
            Id = string.Join(".", IdElementList);
            CreationTime = creationTime;
        }

        public Bunch(string id, DateTime creationTime)
        {
            Id = id;
            IdElementList = id.Split('.').Select(int.Parse).ToList();
            CreationTime = creationTime;
        }

        public static Bunch Parse(string representation,DateTime creationTime)
        {
            return new Bunch(representation.Substring(10,representation.Length-11),creationTime);
        }

        public  override  string ToString()
        {
            return string.Join("->\n", IdElementList.Select(id => App.DatabaseWoElement.GetElementAsync(id).Result.Name));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            return obj.GetType() == GetType() && Equals((Bunch)obj);
        }

        private bool Equals(Bunch other)
        {
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
