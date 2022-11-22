// Author: Nikola Machálková
// Date: 20/11/2022

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
        // TODO: in format "Title"
        // Return: "Title_xx/xx/xxxx_xx:xx:xx_xM"
        public List<string> FindKeyword(string keyword)
        {
            List<string> result = new List<string>();

            string filepathDirectory = Directory.GetCurrentDirectory() + @"/.data/";

            string[] files = Directory.GetFiles(filepathDirectory, "*" + keyword + "*");

            for(int i = 0; i < files.Length - 1; i++)
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
            string trimmedWhitespace = date.Replace('#', ':');

            return trimmedWhitespace.Replace(@"-", "/");
        }
    }
}
