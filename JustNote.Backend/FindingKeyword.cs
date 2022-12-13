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
        // parameters in format keyword as "Title" and date "xx/xx/xxxx"
        // Return: "Title"
        public List<string> FindKeyword(string keyword, string date)
        {
            List<string> result = new List<string>();

            date = TrimText(date);

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/";

            string[] dateDirs = Directory.GetDirectories(filepathDirectory);
            List<string> files = new();
            try
            {
                foreach(string dateDir in dateDirs)
                {
                    files.AddRange( Directory.GetFiles(dateDir + "/", "*" + keyword + "*"));
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
            }
            result = files.ToList();
            return result;
        }

        private string TrimText(string date)
        {
            return date.Replace(@"/", "_");
        }
    }
}
