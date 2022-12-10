// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace JustNote.Backend.Data
{
    public class SavingData
    {
        // method for saving data (as in: whole page)
        public void Save(Data data)
        {
            string trimmedDate = TrimDate(data.Date);

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

            string filename = data.Title + ".json";

            using (StreamWriter sw = new StreamWriter(filepathDirectory + "/" + filename))
            {
                sw.WriteLine(JsonSerializer.Serialize(data));
            }
        }

        // method for getting rid off forbidden characters in a file name
        private string TrimDate(DateTime date)
        {
            string trimmedWhitespace = date.ToString().Replace(' ', '_');
            trimmedWhitespace = trimmedWhitespace.Replace(':', '#');

            return trimmedWhitespace.Replace(@"/", "-");
        }
    }
}
