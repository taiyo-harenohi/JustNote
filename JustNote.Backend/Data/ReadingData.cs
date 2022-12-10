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
        // keyword must be in format "Title" and date in format "xx\xx\xxxx xx:xx:xx xM"
        public Data Read(string keyword, string date)
        {
            keyword = TrimText(keyword);
            date = TrimText(date);

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/" + date + "/";

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

        private string TrimText(string date)
        {
            string trimmedWhitespace = date.Replace(' ', '_');
            trimmedWhitespace = trimmedWhitespace.Replace(':', '#');

            return trimmedWhitespace.Replace(@"/", "-");
        }
    }
}
