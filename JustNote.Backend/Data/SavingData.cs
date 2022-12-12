// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Diagnostics;

namespace JustNote.Backend.Data
{
    public class SavingData
    {
        // method for saving data (as in: whole page)
        public void Save(Data data)
        {
            string trimmedDate = TrimDate(data.Date.ToString("dd/MM/yyyy"));
            
            // if directories doesn't exist yet -> create them
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"/.data/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"/.data/");
            }

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/" + trimmedDate + "/";
            if (!Directory.Exists(filepathDirectory))
            {
                Directory.CreateDirectory(filepathDirectory);
            }


            string filename = TrimTitle(data.Title + ".json");


            using (StreamWriter sw = new StreamWriter(filepathDirectory + filename))
            {
                sw.WriteLine(JsonSerializer.Serialize(data));
            }
        }

        // methods for getting rid off forbidden characters
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
