using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static Elementalium.Domain.Model.Enums;

namespace Elementalium.Domain.Model
{
    public static class Ligamentous
    {
        private static IEnumerable<WoElement> Elements => App.DatabaseWoElement.GetElementAsync().Result;

        public static Bunch GenerateBunch(int countElements, int complexity)
        {
            if (countElements > 12 || complexity > 9) return null;
            var idElements = new List<int>();
            WoElement previousElement = null;
            while (idElements.Count<countElements)
            {
                var position = new Position(Positions.Vis, Grabs.UpperGrip);
                for (var i = 0; i < countElements; i++)
                {
                    var element = i == countElements - 1 && countElements > 2 && complexity > 1
                        ? SearchElement(position, complexity, p => p.Type == Families.Dismount,previousElement)
                        : countElements > 2 && complexity > 1
                            ? SearchElement(position, complexity, p => p.Type != Families.Dismount,previousElement)
                            : SearchElement(position, complexity, p => true,previousElement);
                    if (element == null)
                    {
                        idElements.Clear();
                        break;
                    }
                    idElements.Add(element.Id);
                    previousElement = element;
                    position = element.EndPosition;
                }
            }
            return new Bunch(idElements,DateTime.Now);
        }

        private static WoElement SearchElement(Position position,int complexity,Func<WoElement,bool> condition,WoElement previousElement)
        {
            var rnd = new Random();
            var requiredItems = Elements
                .Where(e =>e.Can &&
                e.Complexity <= complexity && 
                Equals(e.StartPosition, position) && 
                condition(e)).ToArray();
            var count = requiredItems.Length;
            if (count == 0) return null;
            var index = rnd.Next(count-1);
            var newElement= requiredItems.ElementAt(index);
            if (newElement.Equals(previousElement)) return null;
            return newElement;
        }
    }
}
