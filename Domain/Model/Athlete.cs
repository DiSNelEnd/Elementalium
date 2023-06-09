using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elementalium.Domain.Repositories;


namespace Elementalium.Domain.Model
{
    public static class Athlete
    {
        private static string FileName=>Path.Combine(App.BunchRepository.FolderPath, "username.txt");
        public static string Niсkname
        {
            get => File.Exists(FileName) ? File.ReadAllText(FileName) : null;

            set => File.WriteAllText(FileName,value);
        }

        public static int Level
        {
            get
            {
                return App.DatabaseWoElement
                    .GetElementAsync().Result
                    .Where(e => e.Can)
                    .Select(e => e.Complexity)
                    .Sum();
            }
        }

        public static Tuple<int, int> GetCountAndComplexity()
        {
            if (Level>0 && Level<=10) return new Tuple<int, int>(2,2);
            if (Level>10 && Level<=20) return  new Tuple<int, int>(3,2);
            if (Level>20 && Level<=30) return new Tuple<int, int>(4, 2);
            if (Level>30 && Level <= 40) return new Tuple<int, int>(4, 3);
            if (Level>40 && Level<=50) return new Tuple<int, int>(5, 3);
            if (Level > 50 && Level <= 60) return new Tuple<int, int>(5, 4);
            if (Level > 60 && Level <= 70) return new Tuple<int, int>(6, 5);
            return new Tuple<int, int>(8, 5);
        }
    }
}
