using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote.Backend.Data
{
    public class SearchingData
    {
        // method for searching up data to show in a calendar
        public List<string> SearchData(string date)
        {
            List<string> results = new List<string>();

            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"/.data/"))
            {
                return null;
            }

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/" + date + "/";
            if (!Directory.Exists(filepathDirectory))
            {
                return null;
            }

            string[] files = Directory.GetFiles(filepathDirectory);

            for (int i = 0; i < files.Length - 1; i++)
            {
                files[i] = Path.GetFileName(files[i]);
                files[i] = files[i].Replace(".json", "");
            }
            results = files.ToList();


            return results;
        }

        private string TrimDate(string date)
        {
            return date.Replace(@"/", "_");
        }
    }
}
