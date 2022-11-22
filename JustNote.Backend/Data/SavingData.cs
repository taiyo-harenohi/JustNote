// Author: Nikola Machálková
// Date: 19/11/2022

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
        public void Save(Data data)
        {
            string trimmedDate = TrimDate(data.Date);

            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"/.data/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"/.data/");
            }

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/";
            string filename = data.Title + "_" + trimmedDate + ".json";

            using (StreamWriter sw = new StreamWriter(filepathDirectory + "/" + filename))
            {
                sw.WriteLine(JsonSerializer.Serialize(data));
            }
        }

        // method for getting rid off forbidden characters
        private string TrimDate(DateTime date)
        {
            string trimmedWhitespace = date.ToString().Replace(' ', '_');
            trimmedWhitespace = trimmedWhitespace.Replace(':', '#');

            return trimmedWhitespace.Replace(@"/", "-");
        }
    }
}
