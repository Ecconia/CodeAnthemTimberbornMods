using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TB_CameraTweaks.Lang
{
    internal class TocManager
    {
        private readonly IEnumerable<string> _languages;
        private readonly IEnumerable<string> _entries;
        private string TocTag => Plugin._tocTag;

        private string langFolder;
        private const string _headerline = "ID,Text,Comment";

        public TocManager(IEnumerable<string> languages, IEnumerable<string> entries)
        {
            this._languages = languages;
            this._entries = entries;
        }

        public void CheckFiles()
        {
            langFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\lang\\";
            if (!Directory.Exists(langFolder)) Directory.CreateDirectory(langFolder);

            foreach (var lang in _languages)
            {
                var langFile = new FileInfo(langFolder + lang + ".txt");

                VerifyLangFile(langFile);
            }
        }

        private void VerifyLangFile(FileInfo langFile)
        {
            var currentEntries = GetCurrentContent(langFile);

            bool inconsistent = false;
            foreach (var configuredEntries in _entries)
            {
                string fullEntry = TocTag + "." + configuredEntries;
                TocEntryModel entry = currentEntries.FirstOrDefault(x => x.Key == fullEntry);
                if (entry == default)
                {
                    currentEntries.Add(new TocEntryModel(fullEntry, "not~translated~yet", string.Empty));
                    inconsistent = true;
                }
            }
            if (!inconsistent) return;

            UpdateLangFile(langFile, currentEntries);
        }

        private void UpdateLangFile(FileInfo langFile, List<TocEntryModel> currentEntries)
        {
            using (StreamWriter outputFile = new StreamWriter(langFile.FullName))
            {
                outputFile.WriteLine(_headerline);
            }

            using (FileStream fs = new FileStream(langFile.FullName, FileMode.Append))
            {
                using (TextWriter tw = new StreamWriter(fs))

                    foreach (var entry in currentEntries)
                    {
                        if (entry.Comment == string.Empty)
                        {
                            tw.WriteLine(string.Format($"{entry.Key}, {entry.Text}"));
                            continue;
                        }
                        tw.WriteLine(string.Format($"{entry.Key}, {entry.Text}, {entry.Comment}"));
                    }
            }
        }

        private List<TocEntryModel> GetCurrentContent(FileInfo langFile)
        {
            List<TocEntryModel> currentEntries = new();
            if (!langFile.Exists) return currentEntries;

            var lines = File.ReadAllLines(langFile.FullName);
            //Plugin.Log.LogInfo($"Language file: {langFile.Name} - Lines: {lines.Length}");

            for (var i = 0; i < lines.Length; i += 1)
            {
                if (i == 0 && _headerline == lines[0]) continue;

                var line = lines[i];
                if (!string.IsNullOrEmpty(line) && line.Contains(","))
                {
                    string[] parts = line.Split(",");
                    switch (parts.Length)
                    {
                        case 2:
                            currentEntries.Add(new TocEntryModel(parts[0], parts[1], string.Empty));
                            break;

                        case 3:
                            currentEntries.Add(new TocEntryModel(parts[0], parts[1], parts[2]));
                            break;

                        default:
                            throw new InvalidDataException($"Language file {langFile.Name} corrupted data");
                    };
                }
            }
            return currentEntries;
        }
    }
}