// Author: Nikola Machálková 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote.Backend.Data
{
    public class DeletingData
    {
        // Arguments:
        //         data of type Data (structure)

        // Return: -
        public void Delete(Data data)
        {
            string filename = TrimTitle(data.Title);
            string date = TrimDate(data.Date.ToString("dd/MM/yyyy"));

            string filepath = Directory.GetCurrentDirectory() + @"/.data/" + date + "/" + filename + ".json";

            File.Delete(filepath);
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
