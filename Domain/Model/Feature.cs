using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Elementalium.Infrastructure;
using static Elementalium.Domain.Model.Enums;

namespace Elementalium.Domain.Model
{
    public class Feature
    {
        public IEnumerable<Features> Features { get; }
        public Feature(string line)
        {
            Features = line!=""
                ? line.Split(',')
                .Distinct()
                .Select(x => int
                    .TryParse(x, out var number) && number <= 6 ? (Features)number : 0)
                :null;
        }

        public Feature(params Features[] arg)
        {
            Features = arg.ToList();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            return obj.GetType() == GetType() && Equals((Feature) obj);
        }

        private bool Equals(Feature other)
        {
            return Features.Count()==other.Features.Count() && Features.All(f => other.Features.Contains(f));
        }

        public override int GetHashCode()
        {
            return Features.GetHashCode();
        }

        public override string ToString()
        {
            return string.Join(",", Features.Select(f=>Translator.Dictionary[f.ToString()]));
        }

        public static string GetRegexFormat()
        {
            return @"^[0-6](\,[0-6])*$";
        }
    }
}
