using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using static Elementalium.Domain.Model.Enums;

namespace Elementalium.Domain.Model
{
    public class Training
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime TrainingDateTime { get; set; }
        public string RepetitionsLine { get; set; }
        public string LearningNewLine { get; set; }
        public string LigamentsLine { get; set; }
        public string LearnedElements { get; set; }
        public List<WoElement> LearningNewWoElements { get; private set; }

        private IEnumerable<WoElement> Elements => App
            .DatabaseWoElement.GetElementAsync().Result;
        public void StartWorkout(DateTime workoutDateTime, List<WoElement> learningList)
        {
            TrainingDateTime = workoutDateTime;
            LearningNewWoElements=learningList.Count==0? GetRandomElements():learningList;
            LearningNewLine = string.Join("\n", LearningNewWoElements.Select(e => e.Name));
        }

        public void FinishWorkout(HashSet<string> repetitionsHash, HashSet<string> learnedHash, HashSet<Bunch> bunches)
        {
            RepetitionsLine = string.Join(" ! ", repetitionsHash);
            LearnedElements = string.Join("\n", learnedHash);
            LigamentsLine = string.Join("\n\n", bunches);
        }

        private List<WoElement> GetRandomElements()
        {
            var rnd = new Random();
            var result = new List<WoElement>();
            var type = Families.None;
            var complexity = 1;
            while (result.Count < 3)
            {
                if (complexity == 10) break;
                var elements = Elements.Where(e =>!e.Can && e.Complexity == complexity && e.Type!=type).ToArray();
                var count = elements.Length;
                if (count <= 5) complexity++;
                if (count == 0) continue;
                var index = rnd.Next(count - 1);
                var element = elements[index];
                type = element.Type;
                result.Add(element);
            }
            return result;
        }

        public override string ToString()
        {
            return $"Тренировка № {Id}";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Training)obj);
        }
        protected bool Equals(Training other)
        {
            return Id.Equals(other.Id);
        }
    }
}
