using System;
using System.IO;
using System.Reflection;
using Elementalium.Data;
using Elementalium.Domain.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elementalium
{
    public partial class App : Application
    {
        public const string DataBaseName = "WoElementsBeta.db3";
        private static WoElementDatabase _databaseWoElement;
        private static TrainingDatabase _databaseTraining;
        private static BunchRepository _bunchRepository;

        public static TrainingDatabase DatabaseTraining =>
            _databaseTraining ??
            new TrainingDatabase(Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "TrainingBeta1db.db3"));

        public static BunchRepository BunchRepository
        {
            get
            {
                var folderPath= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                _bunchRepository=new BunchRepository(folderPath);
                return _bunchRepository;
            }
        }

        public static WoElementDatabase DatabaseWoElement
        {
            get
            {
                if (_databaseWoElement != null) return _databaseWoElement;
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DataBaseName);
                if (!File.Exists(dbPath))
                {
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    using (var stream = assembly.GetManifestResourceStream("Elementalium.Data.MyWoElements.db3"))
                    {
                        using (var fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                        {
                            stream.CopyTo(fs);  
                            fs.Flush();
                        }
                    }
                }
                _databaseWoElement = new WoElementDatabase(dbPath);
                return _databaseWoElement;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.DarkBlue,
                BarTextColor = Color.Red,
                BackgroundColor= Color.GhostWhite
            };
            CopyDBToDownloadsDirectory(DataBaseName);
            CopyDBToDownloadsDirectory("TrainingBeta1db.db3");
        }

        public static void CopyDBToDownloadsDirectory(string nameBD)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),nameBD);
            string DocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var safePath = Path.Combine(DocumentPath, nameBD);
            File.Copy(safePath, path, true);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
