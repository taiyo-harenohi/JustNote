// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote.Backend.Data
{
    public class SearchingData
    {
        // Arguments:
        //         date "xx/xx/xxxx"

        // Return: list of titles under corresponding date
        public List<string> SearchData(string date)
        {
            List<string> results = new List<string>();

            // checking, if all directories exists -> if not, returns empty list
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"/.data/"))
            {
                return results;
            }

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/" + TrimDate(date) + "/";
            if (!Directory.Exists(filepathDirectory))
            {
                return results;
            }

            string[] files = Directory.GetFiles(filepathDirectory);

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
                files[i] = files[i].Replace(".json", "");
            }
            results = files.ToList();

            return results;
        }

        // changing forbidden character '/' to '_'
        private string TrimDate(string date)
        {
            return date.Replace(@"/", "_");
        }
    }
}
