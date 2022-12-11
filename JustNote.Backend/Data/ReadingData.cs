// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace JustNote.Backend.Data
{
    public class ReadingData
    {
        // keyword must be in format "Title" and date in format "dd/MM/yyyy"
        public Data Read(string keyword, string date)
        {
            keyword = TrimTitle(keyword);
            date = TrimDate(date);


            // if directories doesn't exist yet -> null
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"/.data/"))
            {
                return null;
            }

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/" + date + "/";
            if (!Directory.Exists(filepathDirectory))
            {
                return null;
            }

            string jsonFile = "";
            try
            {
                using (StreamReader sr = new StreamReader(filepathDirectory + keyword + ".json"))
                {
                    jsonFile = sr.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }

            Data? data = JsonSerializer.Deserialize<Data>(jsonFile);

            return data;
        }

        private string TrimDate(string date)
        {
            return date.Replace(@"/", "_");
        }

        private string TrimTitle(string title)
        {
            return Path.GetInvalidFileNameChars().Aggregate(title, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
