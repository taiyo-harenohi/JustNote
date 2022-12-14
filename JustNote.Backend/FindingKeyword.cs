// Author: Nikola Machálková

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote.Backend
{
    public class FindingKeyword
    {
        // Arguments:
        //         keyword as "Title" and date "xx/xx/xxxx"

        // Return: list of titles with corresponding keyword
        public List<string[]> FindKeyword(string keyword, string date)
        {
            List<string[]> result = new List<string[]>();
            if (keyword == null)
            {
                return result;
            }

            date = TrimText(date);

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/";
            if (!Directory.Exists(filepathDirectory))
            {
                return result;
            }

            // looking for files in directories
            string[] dateDirs = Directory.GetDirectories(filepathDirectory);
            List<string> files = new();
            List<string> dates = new();
            int j = 0;
            try
            {
                foreach(string dateDir in dateDirs)
                {
                    files.AddRange( Directory.GetFiles(dateDir + "/", "*" + keyword + "*"));
                    while (j < files.Count)
                    {
                        dates.Add(dateDir.Split("/").Last());
                        j++;
                    }
                }
                
            }
            catch (Exception ex)
            {
                return result;
            }

            for(int i = 0; i < files.Count; i++)
            {
                files[i] = Path.GetFileName(files[i]);
                files[i] = TrimText(files[i]);
                files[i] = files[i].Replace(".json", "");
                string[] title_date = { files[i], dates[i] };
                result.Add(title_date);
            }
            
            return result;
        }

        // replaces fobidden character '/ to '_'
        private string TrimText(string date)
        {
            return date.Replace(@"/", "_");
        }
    }
}
