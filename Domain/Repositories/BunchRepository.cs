using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Elementalium.Domain.Model;

namespace Elementalium.Domain.Repositories
{
    public class BunchRepository:IBunchRepository
    {
        public  string FolderPath { get; }

        public BunchRepository(string folderPath)
        {
            FolderPath = folderPath;
        }
        public List<Bunch> GetBunches()
        {
            var bunches = new List<Bunch>();
            var files = Directory.EnumerateFiles(FolderPath, "*.bunch.txt");
            foreach (var filename in files)
                bunches.Add(new Bunch(File.ReadAllText(filename),File.GetCreationTime(filename)));
            return bunches;
        }

        public Bunch FindBunchById(string id)
        {
            var filename = GetFilename(id);
            if (!File.Exists(filename)) return null;
            var bunch=new Bunch(File.ReadAllText(filename),File.GetCreationTime(filename));
            return bunch;
        }

        public void Save(Bunch bunch)
        {
            var filename = GetFilename(bunch.Id);
            if (!File.Exists(filename))
                File.WriteAllText(filename, bunch.Id);
        }

        public void Delete(Bunch bunch)
        {
            var filename = GetFilename(bunch.Id);
            if (File.Exists(filename))
                File.Delete(filename);
        }

        private string GetFilename(string bunchId)
        {
            return Path.Combine(FolderPath, bunchId + ".bunch.txt");
        }
    }
}
