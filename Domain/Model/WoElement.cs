using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elementalium.Infrastructure;
using SQLite;
using static Elementalium.Domain.Model.Enums;

namespace Elementalium.Domain.Model
{
    [Table("WoElements")]
    public class WoElement
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Complexity { get; set; }
        public Families Type { get; set; }
        public string StartPositionLine { get; set; }
        public string EndPositionLine { get; set; }
        public string FeatureLine { get; set; }
        public bool Can { get; set; }

        public string RussianNameType => Translator.Dictionary.ContainsKey(Type.ToString())
            ? Translator.Dictionary[Type.ToString()]
            : "";
        public Position StartPosition => StartPositionLine != null ? new Position(StartPositionLine) : null; 
        public Position EndPosition => EndPositionLine != null ? new Position(EndPositionLine) : null;
        public Feature Features => FeatureLine != null ? new Feature(FeatureLine) : null;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WoElement)obj);
        }
        protected bool Equals(WoElement other)
        {
            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name}({nameof(Id)}: {Id})";
        }
    }
}
