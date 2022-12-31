using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TB_CameraTweaks.KsHelperLib.Localization
{
    internal class TocLangFileHandler
    {
        internal void WriteUpdatedContent(FileInfo langFile, List<TocEntryModel> currentEntries)
        {
            if (langFile.Exists) { langFile.Delete(); }

            using (FileStream fs = new FileStream(langFile.FullName, FileMode.Append))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine("ID,Text,Comment");

                    foreach (var headerLine in TocConfig.Header)
                    {
                        tw.WriteLine(headerLine);
                    }

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
        }

        internal List<TocEntryModel> GetCurrentContent(FileInfo langFile)
        {
            List<TocEntryModel> currentEntries = new();
            if (!langFile.Exists) return currentEntries;

            var lines = File.ReadAllLines(langFile.FullName);

            for (var i = 0; i < lines.Length; i += 1)
            {
                string line = lines[i];

                if (i <= TocConfig.Header.Count) continue;

                if (!string.IsNullOrEmpty(line) && line.Contains(","))
                {
                    string[] parts = line.Split(",");
                    switch (parts.Length)
                    {
                        case 2:
                            currentEntries.Add(new TocEntryModel(parts[0], parts[1].Trim(), string.Empty));
                            break;

                        case 3:
                            currentEntries.Add(new TocEntryModel(parts[0], parts[1].Trim(), parts[2].Trim()));
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