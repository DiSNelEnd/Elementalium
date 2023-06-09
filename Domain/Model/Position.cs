using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SQLite;
using static Elementalium.Domain.Model.Enums;

namespace Elementalium.Domain.Model
{
    public class Position
    {
        public Positions State { get; }

        public IEnumerable<Grabs> Grabs { get; }

        public Position(string line)
        {
            State = (Positions) int.Parse(line.Substring(0, 1));
            Grabs = line.Length > 1
                ? line
                    .Substring(2).Split(',')
                    .Distinct()
                    .Select(x => int
                        .TryParse(x, out var number) && number <= 3
                        ? (Grabs) number
                        : 0)
                : null;
        }

        public Position(Positions position, params Grabs[] grabs)
        {
            State = position;
            Grabs = grabs.ToList();
        }

        private bool Equals(Position other)
        {
            if (Grabs == null) return State == other.State;
            return State == other.State && Grabs.Any(grip => other.Grabs.Contains(grip));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            return obj.GetType() == GetType() && Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return State.GetHashCode() + Grabs.GetHashCode();
        }

        public override string ToString()
        {
            return $"{State}:{string.Join(",", Grabs)}";
        }

        public static string GetRegexFormat()
        {
            return @"^[2-5]{1}$|^[0-1]{1}\:[0-3]{1}(\,[0-3])*$";
        }
    }

}   
