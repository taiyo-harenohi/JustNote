// Author: Nikola Machálková
// Date: 19/11/2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
// todo: delete later
using System.Diagnostics;

namespace JustNote.Backend.Data
{
    public class ReadingData
    {
        // TODO: keyword must be in format "Title xx\xx\xxxx xx:xx:xx xM"
        public Data Read(string keyword)
        {
            keyword = TrimText(keyword);

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/";

            string jsonFile = "";

            try
            {
                using (StreamReader sr = new StreamReader(filepathDirectory + "/" + keyword + ".json"))
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
