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
        // Arguments:
        //         keyword and date in format xx/xx/xxxx

        // Return: Data (structure)
        public Data Read(string keyword, string date)
        {
            keyword = TrimTitle(keyword);
            date = TrimDate(date);


            // if directories doesn't exist yet -> return empty Data sctructure
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"/.data/"))
            {
                return new Data();
            }

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/" + date + "/";
            if (!Directory.Exists(filepathDirectory))
            {
                return new Data();
            }

            string jsonFile = "";
            try
            {
                // reading .json
                using (StreamReader sr = new StreamReader(filepathDirectory + keyword + ".json"))
                {
                    jsonFile = sr.ReadToEnd();
                }
            }
            catch
            {
                return new Data();
            }

            // converting .json data into Data sctructure
            Data? data = JsonSerializer.Deserialize<Data>(jsonFile);

            return data;
        }

        // getting rid off forbidden characters
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
